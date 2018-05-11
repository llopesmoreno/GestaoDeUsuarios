using DomainNotificationHelper;
using DomainNotificationHelper.Events;

namespace GestaoDeUsuarios.ApplicationService.Services
{
    public class ApplicationService 
    {
        private readonly IHandler<DomainNotification> _notifications;

        public ApplicationService()
        {
            _notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
        }

        public void Notify(string key, string message) => DomainEvent.Raise(new DomainNotification(key, message));

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
