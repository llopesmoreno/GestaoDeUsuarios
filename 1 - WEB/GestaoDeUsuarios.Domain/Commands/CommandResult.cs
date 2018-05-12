using Flunt.Notifications;
using GestaoDeUsuarios.Domain.Base.Commands;
using GestaoDeUsuarios.Shared.Base;

namespace GestaoDeUsuarios.Domain.Commands
{
    public class CommandResult<T> : Notifiable, ICommandResult where T : DTOBase
    {
        public CommandResult() { }

        public CommandResult(bool success, string message, T dto = null)
        {
            Success = success;
            Message = message;
            Dto = dto;
        }

        public T Dto { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}