using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.DataAccess;
using Igor.Library.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Igor.Library.Abstract
{
    public interface IUserServiceBase
    {
        /// <summary>
        /// Get instance of DB context.
        /// </summary>
        /// <returns></returns>
        LarpContext GetContext();

        LarpContext SetContext(LarpContext ctx);
        /// <summary>
        /// Check whether user exists in DB.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool UserExists(string userName);
        /// <summary>
        /// Get number of all users in the system.
        /// </summary>
        /// <returns></returns>
        int GetAllUsersCount();
        /// <summary>
        /// Get all users in the system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IgorUser> GetAllUsers();
        /// <summary>
        /// Get user by their ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IgorUser GetUser(string id);
        /// <summary>
        /// Get user by their ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IgorUser> GetUserAsync(string id);
        /// <summary>
        /// Get user by their username (login name).
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        IgorUser GetUserByUsername(string username);
        /// <summary>
        /// Get user by their e-mail address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        IgorUser GetUserByEmail(string email);
        /// <summary>
        /// Get user by their username (login name) or if not successful - by full name.
        /// </summary>
        /// <param name="usernameOrFullName"></param>
        /// <returns></returns>
        IgorUser GetUserByUsernameOrFullName(string usernameOrFullName);
        /// <summary>
        /// Get user by their username (login name) or if not successful - by Id.
        /// </summary>
        /// <param name="usernameOrId"></param>
        /// <returns></returns>
        IgorUser GetUserByUsernameOrId(string usernameOrId);
        /// <summary>
        /// Get username (login name) of a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetUserName(string id);
        ///// <summary>
        ///// Get full name of a user.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //string GetUserFullName(string id);
        ///// <summary>
        ///// Get full name of a user.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<string> GetUserFullNameAsync(string id);
        /// <summary>
        /// Get first name of a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetUserFirstName(string id);
        /// <summary>
        /// Get user e-mail address.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetUserEmail(string id);
        /// <summary>
        /// Get user e-mail address.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetUserEmailAsync(string id);
        /// <summary>
        /// Get ID of a user by their username (login name).
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetUserId(string userName);
        /// <summary>
        /// Get ID of a user by their username (login name).
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<string> GetUserIdAsync(string userName);
        /// <summary>
        /// Get user by their full name.
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        string GetUserIdByFullName(string fullname);
        /// <summary>
        /// Get a user by its e-mail and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>False if user cannot be logged in (wrong password or e-mail)</returns>
        IgorUser FindUser(string username, string password);
        /// <summary>
        /// Get role with a given name.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        IdentityRole GetRole(string roleName);
        /// <summary>
        /// Add new role in the DB.
        /// </summary>
        /// <param name="roleName"></param>
        void AddRole(string roleName);
        ///// <summary>
        ///// Update existing user with data from input user and its roles.
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="roles"></param>
        //void UpdateUser(IgorUser user, IEnumerable<string> roles);
        /// <summary>
        /// Get all roles in the system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IdentityRole> GetAllRoles();
        /// <summary>
        /// Get all roles in the system without admin and key user types of roles.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IdentityRole> GetAllNonAdminRoles();
        /// <summary>
        /// Get all users that belong to a given role.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        IEnumerable<IgorUser> GetUsersForRole(string roleName);
        /// <summary>
        /// Remove role from user roles.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        void RemoveUserFromRole(string userId, string roleName);
        /// <summary>
        /// Remove role with given name from the system.
        /// </summary>
        /// <param name="roleName"></param>
        void RemoveRole(string roleName);
        /// <summary>
        /// Change password for a user.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="oldp"></param>
        /// <param name="newp"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(string userid, string oldp, string newp);
        /// <summary>
        /// Reset password for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        void ResetPassword(string userId, string newPassword);


        #region JWT tokens

        ///// <summary>
        ///// Get token used to authorize and get further user data in Igor corporate network.
        ///// </summary>
        ///// <param name="userName">Igor username.</param>
        ///// <param name="password">Igor password.</param>
        ///// <returns></returns>
        //Task<IgorToken> GetIgorInitialTokenAsync(string userName, string password);
        ///// <summary>
        ///// Get user data from Igor token by asking CORD for userInfo.
        ///// User MUST exist in Igor.
        ///// </summary>
        ///// <param name="tokenResponse">Response from initial CORD request.</param>
        ///// <returns></returns>
        //Task<IgorLoginResult> GetIgorUserFromJwtTokenAsync(IgorToken tokenResponse);
        ///// <summary>
        ///// Get new  user model from Igor token by asking CORD for userInfo.
        ///// User may not exist in Igor.
        ///// </summary>
        ///// <param name="tokenResponse">Response from initial CORD request.</param>
        ///// <returns></returns>
        //Task<NewIgorUser> GetIgorNewUserFromJwtTokenAsync(IgorToken tokenResponse);
        ///// <summary>
        ///// Generate secure Igor token for user.
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //string GenerateIgorJwtToken(IgorUser user);

        #endregion
    }
}
