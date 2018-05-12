using GestaoDeUsuarios.Shared.Base;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Base.ValueObjects;

namespace GestaoDeUsuarios.Domain.Services
{
    public interface IUserApplicationService<D> where D : DTOBase
    {
        CommandResult<D> Register(CreateUserCommand command);
        CommandResult<D> Update(UpdateUserCommand command);
        CommandResult<D> Delete(DeleteUserCommand command);
        CommandResult<D> GetAll();
        CommandResult<D> GetByCPF(CPF cpf);
        CommandResult<D> GetByName(Name name);
    }
}