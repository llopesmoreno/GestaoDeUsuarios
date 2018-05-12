using GestaoDeUsuarios.Domain.Base.Commands;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Shared.Base;

namespace GestaoDeUsuarios.Domain.Base
{
    public interface IHandler<T, D> where T : ICommand where D : DTOBase
    {
        CommandResult<D> Handle(T command);
    }
}