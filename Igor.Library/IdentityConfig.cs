using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.DataAccess;
using Igor.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(Igor.Library.IdentityConfig))]
namespace Igor.Library
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            IKernel ninject = new StandardKernel();
            app.CreatePerOwinContext(() => ninject.Get<LarpContext>());
            app.CreatePerOwinContext<IgorUserManager>(IgorUserManager.Create);
            /*app.CreatePerOwinContext<RoleManager<IgorRole>>((options, context) =>
                new RoleManager<IgorRole>(
                    new RoleStore<IgorRole>(context.Get<UserDataContext>())));*/

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
            //ConfigureAuth(app);
            ConfigureOAuthTokenConsumption(app);
        }
        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            string issuer = ConfigurationManager.AppSettings["JwtIssuer"];
            string audienceId = ConfigurationManager.AppSettings["JwtAudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["JwtSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { audienceId },
                IssuerSecurityKeyProviders = new List<IIssuerSecurityKeyProvider>()
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider(issuer, audienceSecret)
                    }
            });
        }
    }
}
