using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Igor.Library.Abstract;
using Igor.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Igor.Library.Services
{
    public class UserService : UserServiceBase, IUserService
    {
        private readonly IAuthenticationManager _authManager;
        private readonly HttpContextBase _httpcontext;

        public UserService(HttpContextBase httpcontext, ILarpUnitOfWork uow)
        {
            this._context = uow.GetContext();
            if (httpcontext != null)
            {
                _userManager = httpcontext.GetOwinContext().GetUserManager<IgorUserManager>();
                _authManager = httpcontext.GetOwinContext().Authentication;
                _roleManager = new IgorRoleManager(new RoleStore<IgorRole>(this._context));
                _httpcontext = httpcontext;
            }
        }


        /// <inheritdoc />
        public IgorUser GetCurrentUser()
        {
            try
            {
                return _userManager.FindById(_httpcontext.User.Identity.GetUserId());
            }
            catch
            {
                return null;
            }
        }
        /// <inheritdoc />
        public string GetCurrentUserId()
        {
            try
            {
                return _httpcontext.User.Identity.GetUserId();
            }
            catch
            {
                return null;
            }

        }
        /// <inheritdoc />
        public string GetCurrentUserName()
        {
            return _httpcontext.User.Identity.GetUserName();
        }
        /// <inheritdoc />
        public IEnumerable<string> GetCurrentUserRoles()
        {
            SetUserRoleManager();
            return _userManager?.GetRoles(_httpcontext?.User?.Identity?.GetUserId()) ?? new List<string>();
        }
        /// <inheritdoc />
        public bool IsLogged()
        {
            return HttpContext.Current?.User?.Identity?.IsAuthenticated ?? false;
        }
        /// <inheritdoc />
        public bool IsInRole(string roleName)
        {
            return HttpContext.Current?.User?.IsInRole(roleName) ?? false;
        }
        /// <summary>
        /// Generate claim (in cookies) for a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected ClaimsIdentity CreateIdentity(IgorUser user)
        {
            return user.GenerateUserIdentity(_userManager);
        }
        /// <inheritdoc />
        public void SignIn(IgorUser user)
        {
            ClaimsIdentity ident = CreateIdentity(user);
            ClaimsIdentity browserRememebered = _authManager.CreateTwoFactorRememberBrowserIdentity(user.Id);
            _authManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false,
                AllowRefresh = true
            },
            new ClaimsIdentity[] { ident, browserRememebered });
        }
        /// <inheritdoc />
        public void SignOut()
        {
            _authManager.SignOut();
        }
    }
}
