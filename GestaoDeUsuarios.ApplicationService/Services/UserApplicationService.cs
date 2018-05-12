using System.Linq;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Handlers;
using GestaoDeUsuarios.Domain.Services;
using GestaoDeUsuarios.Domain.Repositories;

namespace GestaoDeUsuarios.ApplicationService.Services
{
    public class UserApplicationService : ApplicationService, IUserApplicationService<UserDTO>
    {
        private readonly IUserRepository _repository;

        public UserApplicationService(IUserRepository repository) => _repository = repository;
        
        public CommandResult<UserDTO> Register(CreateUserCommand command)
        {
            var handler = new CreateUserHandler(_repository);

            var retornoHandler = handler.Handle(command);

            if (!retornoHandler.Success)
                retornoHandler.Notifications.ToList().ForEach(n => Notify(n.Property, n.Message));

            if (Commit())            
                _repository.Save();

            return retornoHandler;
        }

        public CommandResult<UserDTO> Update(UpdateUserCommand command)
        {
            var handler = new UpdateUserHandler(_repository);

            var retornoHandler = handler.Handle(command);

            if (!retornoHandler.Success)
                retornoHandler.Notifications.ToList().ForEach(n => Notify(n.Property, n.Message));

            if (Commit())
                _repository.Save();

            return retornoHandler;
        }

        public CommandResult<UserDTO> Delete(DeleteUserCommand command)
        {
            var handler = new DeleteUserHandler(_repository);

            var retornoHandler = handler.Handle(command);

            if (!retornoHandler.Success)
                retornoHandler.Notifications.ToList().ForEach(n => Notify(n.Property, n.Message));

            if (Commit())
                _repository.Save();

            return retornoHandler;
        }
    }
}