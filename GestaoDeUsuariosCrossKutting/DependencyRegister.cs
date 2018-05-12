using Unity;
using Unity.Lifetime;
using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using GestaoDeUsuarios.Domain.Services;
using DomainNotificationHelper.Handlers;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuariosFakeInfra.Repositories;
using GestaoDeUsuarios.ApplicationService.Services;
using GestaoDeUsuarios.Shared;

namespace GestaoDeUsuariosCrossKutting
{
    public static class DependencyRegister
    {
        public static void Register(UnityContainer container)
        {            
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserApplicationService<UserDTO>, UserApplicationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHandler<DomainNotification>, DomainNotificationHandler>(new HierarchicalLifetimeManager());
        }
    }
}