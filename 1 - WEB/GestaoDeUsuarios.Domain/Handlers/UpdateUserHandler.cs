using Flunt.Notifications;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Shared.Resources;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuarios.Domain.Base.ValueObjects;

namespace GestaoDeUsuarios.Domain.Handlers
{
    public class UpdateUserHandler : Notifiable, 
        IHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IUserRepository userRepository;

        public UpdateUserHandler(IUserRepository repository) => userRepository = repository; 

        public CommandResult<UserDTO> Handle(UpdateUserCommand command)
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

            var usuarioAtualizar = userRepository.GetById(command.Id);

            usuarioAtualizar.Update(nome, cpf, command.Telefone);

            if (usuarioAtualizar.Invalid)
            {
                commandResult.AddNotifications(usuarioAtualizar);
                return commandResult;
            }

            userRepository.Update(usuarioAtualizar);

            var dto = new UserDTO(usuarioAtualizar.Name.FirstName,
                usuarioAtualizar.Name.LastName,
                usuarioAtualizar.CPF.Value,
                usuarioAtualizar.Telefone,
                usuarioAtualizar.Id.ToString());

            commandResult.Success = true;
            commandResult.Message = Message.CadastroRealizadoComSucesso;
            commandResult.Dto = dto;

            return commandResult;
        }
       
    }
}