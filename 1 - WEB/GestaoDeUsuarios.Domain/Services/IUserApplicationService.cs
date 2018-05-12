using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Shared.Base;

namespace GestaoDeUsuarios.Domain.Services
{
    public interface IUserApplicationService<D> where D : DTOBase
    {
        CommandResult<D> Register(CreateUserCommand command);
        CommandResult<D> Update(UpdateUserCommand command);
        CommandResult<D> Delete(DeleteUserCommand command);
    }
}