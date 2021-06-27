using Igor.Library.Abstract;
using Igor.Library.DataAccess;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Igor.Library.Services;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Igor.Core.Infrastructure
{
    public class NinjectIgorDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectIgorDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void AddBindings()
        {
            _kernel.Bind<ILarpUnitOfWork>().To<LarpUnitOfWork>().InRequestScope();
            _kernel.Bind<IIgorDataService>().To<IgorDataService>().InRequestScope();
            _kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            _kernel.Bind<IUserServiceBase>().To<UserServiceBase>().InRequestScope();
            _kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InRequestScope();
            
            _kernel.Bind<LarpContext>().ToSelf().InRequestScope();

        }
    }
}