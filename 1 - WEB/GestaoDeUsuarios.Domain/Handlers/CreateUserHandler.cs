using Flunt.Notifications;
using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Shared.Resources;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuarios.Domain.Base.ValueObjects;
using GestaoDeUsuarios.Shared;

namespace GestaoDeUsuarios.Domain.Handlers
{
    public class CreateUserHandler : Notifiable, 
        IHandler<CreateUserCommand, UserDTO>
    {
        public CreateUserHandler(IUserRepository repository) => userRepository = repository;        

        private readonly IUserRepository userRepository;

        public CommandResult<UserDTO> Handle(CreateUserCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                //todo criar resource
                return new CommandResult<UserDTO>(false, "Dados de cadastro inválidos");
            }

            if (userRepository.CPFExists(new CPF(command.CPF)))
                //todo criar resource
                AddNotification("CPF", "Este CPF já está cadastrado!");

            var nome = new Name(command.Nome, command.Sobrenome);
            var cpf = new CPF(command.CPF);

            var usuario = new User(nome, cpf, command.Telefone);

            if (usuario.Invalid)
            {
                AddNotifications(usuario);
                //todo criar resource
                return new CommandResult<UserDTO>(false, "Dados de cadastro inválidos");
            }

            userRepository.Create(usuario);

            var dto = new UserDTO(usuario.Name.FirstName,
                usuario.Name.LastName,
                usuario.CPF.Value,
                usuario.Telefone,
                usuario.Id.ToString());

            return new CommandResult<UserDTO>(true, Message.CadastroRealizadoComSucesso, dto);
        }
       
    }
}