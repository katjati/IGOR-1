using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Igor.Library.Models
{
    public class IgorUserManager : UserManager<IgorUser>
    {
        public IgorUserManager(IUserStore<IgorUser> store)
            : base(store)
        {
            PasswordValidator = new PasswordValidator
            {
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
        }

        // this method is called by Owin therefore best place to configure your User Manager
        public static IgorUserManager Create(
            IdentityFactoryOptions<IgorUserManager> options, IOwinContext context)
        {
            var manager = new IgorUserManager(
                new UserStore<IgorUser>(context.Get<LarpContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}
