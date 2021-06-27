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
    public class IgorRoleManager : RoleManager<IgorRole>
    {
        public IgorRoleManager(IRoleStore<IgorRole, string> store) : base(store)
        {
        }

        public static IgorRoleManager Create(IdentityFactoryOptions<IgorRoleManager> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IgorRole>(context.Get<LarpContext>());
            return new IgorRoleManager(roleStore);
        }
    }
}
