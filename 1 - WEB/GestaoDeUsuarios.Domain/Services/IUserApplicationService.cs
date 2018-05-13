using GestaoDeUsuarios.Shared.Base;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Queries.Params;

namespace GestaoDeUsuarios.Domain.Services
{
    public interface IUserApplicationService<D> where D : DTOBase
    {
        CommandResult<D> Register(CreateUserCommand command);
        CommandResult<D> Update(UpdateUserCommand command);
        CommandResult<D> Delete(DeleteUserCommand command);
        CommandResult<D> GetAll();
        CommandResult<D> GetByParams(QueryUserParams queryUserParams);
    }
}