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
    public class UpdateUserHandler : Notifiable, 
        IHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IUserRepository userRepository;

        public UpdateUserHandler(IUserRepository repository) => userRepository = repository; 

        public CommandResult<UserDTO> Handle(UpdateUserCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult<UserDTO>(false, "Dados de cadastro inválidos");
            }

            var usuarioAtualizar = userRepository.GetById(command.Id);

            var nome = new Name(command.Nome, command.Sobrenome);
            var cpf = new CPF(command.CPF);

            usuarioAtualizar.Update(nome, cpf, command.Telefone);

            if (usuarioAtualizar.Invalid)
            {
                AddNotifications(usuarioAtualizar);
                return new CommandResult<UserDTO>(false, "Dados de cadastro inválidos");
            }

            userRepository.Update(usuarioAtualizar);

            var dto = new UserDTO(usuarioAtualizar.Name.FirstName, 
                usuarioAtualizar.Name.LastName, 
                usuarioAtualizar.CPF.Value, 
                usuarioAtualizar.Telefone, 
                usuarioAtualizar.Id.ToString());

            //Todo Criar resource.
            return new CommandResult<UserDTO>(true, "Registro Atualizado Com Sucesso", dto);
        }
       
    }
}