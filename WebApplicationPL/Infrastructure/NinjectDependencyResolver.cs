using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Ninject;

namespace WebApplicationPL.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;
        public NinjectDependencyResolver(IKernel _kernel)
        {
            if (_kernel == null)
                throw new ArgumentNullException(nameof(_kernel));

            kernel = _kernel;
        }
        public object GetService(Type serviceType) => kernel.TryGet(serviceType);
        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);
    }
}