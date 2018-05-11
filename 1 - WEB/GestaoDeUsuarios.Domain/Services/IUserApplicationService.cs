using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Entities;

namespace GestaoDeUsuarios.Domain.Services
{
    public interface IUserApplicationService
    {
        CommandResult Register(CreateUserCommand command);
    }
}