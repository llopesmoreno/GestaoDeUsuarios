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
            var commandResult = new CommandResult<UserDTO>();

            command.Validate();

            if (command.Invalid)
            {
                commandResult.AddNotifications(command);
                return commandResult;
            }             

            if (userRepository.CPFExists(new CPF(command.CPF)))
            {
                commandResult.AddNotification("CPF", "Este CPF já está cadastrado!");
                return commandResult;
            }

            var nome = new Name(command.Nome, command.Sobrenome);
            var cpf = new CPF(command.CPF);

            var usuario = new User(nome, cpf, command.Telefone);

            if (usuario.Invalid)
            {
                commandResult.AddNotifications(usuario);
                return commandResult;
            }

            userRepository.Create(usuario);

            var dto = new UserDTO(usuario.Name.FirstName,
                usuario.Name.LastName,
                usuario.CPF.Value,
                usuario.Telefone,
                usuario.Id.ToString());

            commandResult.Success = true;
            commandResult.Message = Message.CadastroRealizadoComSucesso;
            commandResult.Dto = dto;

            return commandResult;
        }       
    }
}