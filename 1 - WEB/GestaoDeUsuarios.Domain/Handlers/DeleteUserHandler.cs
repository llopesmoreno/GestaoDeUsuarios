using Flunt.Notifications;
using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Shared.Resources;
using GestaoDeUsuarios.Domain.Repositories;

namespace GestaoDeUsuarios.Domain.Handlers
{
    public class DeleteUserHandler : Notifiable, 
        IHandler<DeleteUserCommand>
    {
        public DeleteUserHandler(IUserRepository repository) => userRepository = repository;        

        private readonly IUserRepository userRepository;

        public CommandResult Handle(DeleteUserCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Dados de cadastro inválidos");
            }

            var usuarioDeletar = userRepository.GetById(command.Id);

            if(usuarioDeletar == null)
            {
                //todo  criar resource
                AddNotification("DeleteUserCommand.Id","Usuário inexiste");
            }

            userRepository.Delete(usuarioDeletar);
            
            return new CommandResult(true, Message.CadastroRealizadoComSucesso);
        }
    }
}