using System.Linq;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Handlers;
using GestaoDeUsuarios.Domain.Services;
using GestaoDeUsuarios.Domain.Repositories;

namespace GestaoDeUsuarios.ApplicationService.Services
{
    public class UserApplicationService : ApplicationService, IUserApplicationService
    {
        private readonly IUserRepository _repository;

        public UserApplicationService(IUserRepository repository) => _repository = repository;        

        public CommandResult Register(CreateUserCommand command)
        {
            var handler = new CreateUserHandler(_repository);

            var retornoHandler = handler.Handle(command);

            if (!retornoHandler.Success)
                retornoHandler.Notifications.ToList().ForEach(n => Notify(n.Property, n.Message));

            if (Commit())            
                _repository.Save();

            return retornoHandler;
        }    
    }
}
