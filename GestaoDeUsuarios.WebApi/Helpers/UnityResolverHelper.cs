using Unity;
using System;
using Unity.Exceptions;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using DomainNotificationHelper.Helpers.Mvc.Containers;

namespace GestaoDeUsuarios.WebAPI.Helpers
{
    public class UnityResolverHelper : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolverHelper(IUnityContainer container)
        {
            this.container = container ?? throw new ArgumentNullException("container");
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolverHelper(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}