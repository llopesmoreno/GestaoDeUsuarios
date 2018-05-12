using Flunt.Notifications;
using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Domain.Base.Commands;

namespace GestaoDeUsuarios.Domain.Commands
{
    public class CommandResult : Notifiable, ICommandResult
    {
        public CommandResult() { }

        public CommandResult(bool success, string message, IEntitie entitie = null)
        {
            Success = success;
            Message = message;
            Entitie = entitie;
        }

        public IEntitie Entitie { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}