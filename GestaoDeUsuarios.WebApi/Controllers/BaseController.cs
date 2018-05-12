using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GestaoDeUsuarios.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        public IHandler<DomainNotification> Notifications;
        public HttpResponseMessage ResponseMessage;

        public BaseController()
        {
            Notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
            ResponseMessage = new HttpResponseMessage();
        }

        public Task<HttpResponseMessage> CreateResponse(HttpStatusCode code, object result)
        {
            if (Notifications.HasNotifications())
                ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest,
                    new {
                        success = false,
                        errors = Notifications.Notify()
                    });
            else
                ResponseMessage = Request.CreateResponse(code, result);

            return Task.FromResult(ResponseMessage);
        }

        public Task<HttpResponseMessage> CreateErrorResponse(HttpStatusCode code)
        {
            if (Notifications.HasNotifications())
                ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest,
                    new { errors = Notifications.Notify() });
            else
                ResponseMessage = Request.CreateResponse(code, "");

            return Task.FromResult(ResponseMessage);
        }

        public bool InvalidBody([FromBody] dynamic body) => body == null;

        protected override void Dispose(bool disposing) => Notifications.Dispose();
    }
}