using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Domain.Commands;

namespace GestaoDeUsuarios.Domain.Services
{
    public interface IUserApplicationService
    {
        CommandResult Register(CreateUserCommand command);
        CommandResult Update(UpdateUserCommand command);
        CommandResult Delete(DeleteUserCommand command);
    }
}