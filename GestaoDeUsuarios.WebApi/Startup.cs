using Owin;
using Unity;
using Microsoft.Owin;
using System.Web.Http;
using Newtonsoft.Json;
using Microsoft.Owin.Cors;
using GestaoDeUsuariosCrossKutting;
using Newtonsoft.Json.Serialization;
using DomainNotificationHelper.Events;
using GestaoDeUsuarios.WebAPI.Helpers;
using DomainNotificationHelper.Helpers.Mvc.Containers;

[assembly: OwinStartup(typeof(GestaoDeUsuarios.WebApi.Startup))]

namespace GestaoDeUsuarios.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var container = new UnityContainer();

            ConfigureDependencyInjection(config, container);
            ConfigureWebApi(config);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        public static void ConfigureDependencyInjection(HttpConfiguration config, UnityContainer container)
        {
            DependencyRegister.Register(container);

            config.DependencyResolver = new UnityResolverHelper(container);
            DomainEvent.Container = new DomainEventsContainer(config.DependencyResolver);
        }
    }
}
