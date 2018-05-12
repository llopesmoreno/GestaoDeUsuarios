using Flunt.Notifications;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Shared.Resources;
using GestaoDeUsuarios.Domain.Repositories;

namespace GestaoDeUsuarios.Domain.Handlers
{
    public class DeleteUserHandler : Notifiable, 
        IHandler<DeleteUserCommand, UserDTO>
    {
        public DeleteUserHandler(IUserRepository repository) => userRepository = repository;        

        private readonly IUserRepository userRepository;

        public CommandResult<UserDTO> Handle(DeleteUserCommand command)
        {
            var commandResult = new CommandResult<UserDTO>();

            command.Validate();

            if (command.Invalid)
            {
                commandResult.AddNotifications(command);
                return commandResult;
            }

            var usuarioDeletar = userRepository.GetById(command.Id);

            if(usuarioDeletar == null)
            {
                //todo  criar resource
                commandResult.AddNotification("DeleteUserCommand.Id","Usuário inexiste");
                return commandResult;
            }

            userRepository.Delete(usuarioDeletar);

            commandResult.Success = true;
            //todo criar resource
            commandResult.Message = "Usuario excluido com sucesso";

            return commandResult;
        }
    }
}