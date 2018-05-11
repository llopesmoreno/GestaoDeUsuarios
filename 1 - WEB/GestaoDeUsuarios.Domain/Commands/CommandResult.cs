using Flunt.Notifications;
using GestaoDeUsuarios.Domain.Base.Commands;

namespace GestaoDeUsuarios.Domain.Commands
{
    public class CommandResult : Notifiable, ICommandResult
    {
        public CommandResult() { }

        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}