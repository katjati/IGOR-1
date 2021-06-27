using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Models;
using JetBrains.Annotations;

namespace Igor.Library.Abstract
{
    public interface IUserService : IUserServiceBase
    {
        /// <summary>
        /// Get roles of currently logged in user.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        IEnumerable<string> GetCurrentUserRoles();
        /// <summary>
        /// Get currently logged in user.
        /// </summary>
        /// <returns></returns>
        IgorUser GetCurrentUser();
        /// <summary>
        /// Get ID of currently logged in user.
        /// </summary>
        /// <returns></returns>
        string GetCurrentUserId();
        /// <summary>
        /// Get username of currently logged in user.
        /// </summary>
        /// <returns></returns>
        string GetCurrentUserName();
        /// <summary>
        /// Check whether current use is logged in.
        /// </summary>
        /// <returns></returns>
        bool IsLogged();
        /// <summary>
        /// Check if current user has given role.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool IsInRole(string roleName);
        /// <summary>
        /// Log in user.
        /// </summary>
        /// <param name="user"></param>
        void SignIn(IgorUser user);

        /// <summary>
        /// Log out user.
        /// </summary>
        void SignOut();
    }
}
