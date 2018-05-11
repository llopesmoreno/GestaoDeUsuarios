using GestaoDeUsuarios.Domain.Base.Commands;
using GestaoDeUsuarios.Domain.Commands;

namespace GestaoDeUsuarios.Domain.Base
{
    public interface IHandler<T> where T : ICommand
    {
        CommandResult Handle(T command);
    }
}