using System.Linq;
using GestaoDeUsuarios.Shared;
using DomainNotificationHelper;
using System.Collections.Generic;
using GestaoDeUsuarios.Shared.Base;
using DomainNotificationHelper.Events;
using GestaoDeUsuarios.Domain.Commands;

namespace GestaoDeUsuarios.ApplicationService.Services
{
    public class ApplicationService<T> where T : DTOBase
    {
        private readonly IHandler<DomainNotification> _notifications;

        public ApplicationService()
        {
            _notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
        }

        public void Notify(string key, string message) => DomainEvent.Raise(new DomainNotification(key, message));

        public void Notify(CommandResult<T> command)
        {
            command.Errors = new List<Error>();
            command.Notifications.ToList()
                .ForEach(n => command.Errors.Add(new Error(n.Property, n.Message)));
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications())
                return false;

            return true;
        }

        public void Rollback(string message) => DomainEvent.Raise(new DomainNotification("BusinessError", message));        

        public void Rollback() {  }
    }
}
