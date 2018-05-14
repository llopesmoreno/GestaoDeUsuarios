using System.Linq;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Handlers;
using GestaoDeUsuarios.Domain.Services;
using GestaoDeUsuarios.Domain.Queries;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuarios.Domain.Queries.Params;
using GestaoDeUsuarios.Domain.Base.ValueObjects;

namespace GestaoDeUsuarios.ApplicationService.Services
{
    public class UserApplicationService : ApplicationService<UserDTO>, IUserApplicationService<UserDTO>
    {
        private readonly IUserRepository _repository;

        public UserApplicationService(IUserRepository repository) => _repository = repository;
        
        public CommandResult<UserDTO> Register(CreateUserCommand command)
        {
            var handler = new CreateUserHandler(_repository);

            var retornoHandler = handler.Handle(command);

            if (!retornoHandler.Success)
            {
                Notify(retornoHandler);
                return retornoHandler;
            }

            if (Commit())            
                _repository.Save();

            return retornoHandler;
        }

        public CommandResult<UserDTO> Update(UpdateUserCommand command)
        {
            var handler = new UpdateUserHandler(_repository);

            var retornoHandler = handler.Handle(command);

            if (!retornoHandler.Success)
            {
                Notify(retornoHandler);
                return retornoHandler;
            }

            if (Commit())
                _repository.Save();

            return retornoHandler;
        }

        public CommandResult<UserDTO> Delete(DeleteUserCommand command)
        {
            var handler = new DeleteUserHandler(_repository);

            var retornoHandler = handler.Handle(command);

            if (!retornoHandler.Success)
            {
                Notify(retornoHandler);
                return retornoHandler;
            }

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

        private CommandResult<UserDTO> GetByCPF(CPF cpf)
        {
            var querie = new UserQueries(_repository);

            var retorno = querie.GetByCPF(cpf);

            return retorno;
        }

        private CommandResult<UserDTO> GetByName(Name name)
        {
            var querie = new UserQueries(_repository);

            var retorno = querie.GetByName(name);

            return retorno;
        }

        public CommandResult<UserDTO> GetByParams(QueryUserParams queryUserParams)
        {
            var result = new CommandResult<UserDTO>();

            if (queryUserParams.Nome.Valid)
                result = GetByName(queryUserParams.Nome);
            else
                result = GetByCPF(queryUserParams.Cpf);

            return result;
        }
        
    }
}