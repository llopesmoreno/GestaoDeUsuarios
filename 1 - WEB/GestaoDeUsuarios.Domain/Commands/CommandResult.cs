using Flunt.Notifications;
using GestaoDeUsuarios.Domain.Base.Commands;
using GestaoDeUsuarios.Shared.Base;
using System.Collections.Generic;

namespace GestaoDeUsuarios.Domain.Commands
{
    public class CommandResult<T> : Notifiable, ICommandResult where T : DTOBase
    {
        public CommandResult() { }

        public CommandResult(bool success, string message, T dto = null, List<T> listDto = null)
        {
            Dto = dto;
            ListDto = listDto;
            Success = success;
            Message = message;
        }

        public List<T> ListDto { get; set; }
        public T Dto { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}