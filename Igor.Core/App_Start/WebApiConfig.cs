using Igor.Library.Abstract;
using Igor.Library.DataAccess;
using Ninject;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;
using Igor.Library.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Web.Common;

namespace Igor.Core
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            IKernel kernel = new StandardKernel();
            kernel.Bind<DefaultModelValidatorProviders>()
                .ToConstant(new DefaultModelValidatorProviders(config.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>()));
            kernel.Bind<DefaultFilterProviders>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());

            kernel.Bind<ILarpUnitOfWork>().To<LarpUnitOfWork>().InRequestScope();
            kernel.Bind<IIgorDataService>().To<IgorDataService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IUserServiceBase>().To<UserServiceBase>().InRequestScope();
            kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InRequestScope();
            
            kernel.Bind<LarpContext>().ToSelf().InRequestScope();
            kernel.Bind<LarpUnitOfWork>().ToSelf().InRequestScope();


            config.DependencyResolver = new NinjectDependencyResolver(kernel);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
