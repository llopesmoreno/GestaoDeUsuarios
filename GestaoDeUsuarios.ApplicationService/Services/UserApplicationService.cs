using System.Linq;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Handlers;
using GestaoDeUsuarios.Domain.Services;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuarios.Domain.Base.ValueObjects;

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

        public CommandResult<UserDTO> GetAll()
        {
            var querie = new UserQueries(_repository);

            var retorno = querie.GetAllUsers();

            return retorno;
        }

        public CommandResult<UserDTO> GetByCPF(CPF cpf)
        {
            var querie = new UserQueries(_repository);

            var retorno = querie.GetByCPF(cpf);

            return retorno;
        }

        public CommandResult<UserDTO> GetByName(Name name)
        {
            var querie = new UserQueries(_repository);

            var retorno = querie.GetByName(name);

            return retorno;
        }
    }
}