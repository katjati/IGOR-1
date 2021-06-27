using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Igor.Library.Abstract;
using Igor.Library.DataAccess;
using Igor.Library.Global;
using Igor.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Igor.Library.Services
{
    public class UserServiceBase : IUserServiceBase
    {
        protected IgorUserManager _userManager;
        protected IgorRoleManager _roleManager;
        protected LarpContext _context;

        ///<inheritdoc/>
        public LarpContext GetContext()
        {
            return _context ?? SetContext();
        }
        /// <summary>
        /// Set and return new instance of context.
        /// </summary>
        /// <returns></returns>
        protected LarpContext SetContext()
        {
            _context = new LarpContext();
            return _context;
        }
        /// <summary>
        /// Set and return context provided as input.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public LarpContext SetContext(LarpContext ctx)
        {
            _context = ctx;
            return _context;
        }
        /// <summary>
        /// Create user manager if disposed.
        /// </summary>
        protected void SetUserRoleManager()
        {
            if (_userManager.Users == null)
            {
                _userManager = new IgorUserManager(new UserStore<IgorUser>(GetContext()));
                _roleManager = new IgorRoleManager(new RoleStore<IgorRole>(GetContext()));
            }
        }
        //====================================================

        /// <summary>
        /// Get list of role names from user identity roles.
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        protected IList<string> RolesToList(IEnumerable<IdentityUserRole> userRoles)
        {
            return userRoles.Select(identityUserRole => GetRole(identityUserRole.RoleId)).Select(role => role.Name).ToList();
        }

        //====================================================

        /// <inheritdoc />
        public bool UserExists(string userName)
        {
            SetUserRoleManager();
            return _userManager.Users.Any(a => a.UserName == userName);
        }
        /// <inheritdoc />
        public int GetAllUsersCount()
        {
            SetUserRoleManager();
            return _userManager.Users.Count();
        }
        /// <inheritdoc />
        public IEnumerable<IgorUser> GetAllUsers()
        {
            SetUserRoleManager();
            return _userManager.Users.ToList();
        }
        /// <inheritdoc />
        public IgorUser GetUserByUsername(string username)
        {
            SetUserRoleManager();
            username = username?.ToLower() ?? string.Empty;
            return UserExists(username) ? _userManager.Users.FirstOrDefault(w => w.UserName.ToLower() == username) : null;
        }
        /// <inheritdoc />
        public IgorUser GetUserByEmail(string email)
        {
            SetUserRoleManager();
            email = email?.ToLower() ?? string.Empty;
            return _userManager?.Users?.FirstOrDefault(w => w.Email.ToLower() == email);
        }
        /// <inheritdoc />
        public IgorUser GetUserByUsernameOrFullName(string usernameOrFullName)
        {
            IgorUser user = GetUserByUsername(usernameOrFullName);
            if (user == null)
            {
                string userId = GetUserIdByFullName(usernameOrFullName);
                if (!userId.IsNullOrEmpty()) user = GetUser(userId);
            }

            return user;
        }
        /// <inheritdoc />
        public IgorUser GetUserByUsernameOrId(string usernameOrId)
        {
            IgorUser user = GetUserByUsername(usernameOrId) ?? GetUser(usernameOrId);

            return user;
        }
        /// <inheritdoc />
        public IgorUser GetUser(string id)
        {
            return _userManager.FindById(id);
        }
        /// <inheritdoc />
        public async Task<IgorUser> GetUserAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public IQueryable<IgorUser> GetUsersByUserNameQuery(string userName)
        {
            SetUserRoleManager();
            userName = userName?.ToLower() ?? string.Empty;
            return _userManager.Users?.Where(w => w.UserName.ToLower() == userName);
        }
        /// <inheritdoc />
        public string GetUserId(string userName)
        {
            return GetUsersByUserNameQuery(userName)?.Select(x => x.Id).FirstOrDefault();
        }
        /// <inheritdoc />
        public async Task<string> GetUserIdAsync(string userName)
        {
            IQueryable<IgorUser> query = GetUsersByUserNameQuery(userName);
            return query == null
                ? null
                : await GetUsersByUserNameQuery(userName).Select(x => x.Id).FirstOrDefaultAsync();
        }
        /// <inheritdoc />
        public string GetUserIdByFullName(string fullname)
        {
            SetUserRoleManager();
            return _userManager.Users.Where(w => w.FirstName.ToLower() + " " + w.LastName.ToLower() == fullname.ToLower()).Select(x => x.Id).FirstOrDefault();
        }
        /// <inheritdoc />
        public string GetUserName(string id)
        {
            return GetUser(id)?.UserName;
        }
        /// <inheritdoc />
        public string GetUserEmail(string id)
        {
            return GetUser(id)?.Email ?? string.Empty;
        }
        /// <inheritdoc />
        public async Task<string> GetUserEmailAsync(string id)
        {
            return (await GetUserAsync(id))?.Email ?? string.Empty;
        }
        /// <inheritdoc />
        //public string GetUserFullName(string id)
        //{
        //    return GetUser(id)?.FullName ?? string.Empty;
        //}
        ///// <inheritdoc />
        //public async Task<string> GetUserFullNameAsync(string id)
        //{
        //    IgorUser user = await GetUserAsync(id);
        //    return user?.FullName ?? string.Empty;
        //}
        /// <inheritdoc />
        public string GetUserFirstName(string id)
        {
            return GetUser(id)?.FirstName ?? string.Empty;
        }
        /// <inheritdoc />
        public IgorUser FindUser(string username, string password)
        {
            return _userManager.Find(username, password);
        }
        /// <inheritdoc />
        //public string AddUser(NewIgorUser user)
        //{
        //    if (user.UserRoles == null || !user.UserRoles.Any()) user.UserRoles = new List<string> { RoleConstants.CreatorRole };

        //    IgorUser newUser = new IgorUser
        //    {
        //        UserName = user.UserName,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        PhoneNumber = user.PhoneNumber,
        //    };

        //    if (user.OfficeSiteName.IsNullOrEmpty())
        //    {
        //        //default office site
        //        user.OfficeSiteName = "None";
        //    }

        //    IdentityResult result = user.Password.IsNullOrEmpty() ? _userManager.Create(newUser) : _userManager.Create(newUser, user.Password);
        //    if (result.Succeeded)
        //    {

        //        foreach (string role in user.UserRoles)
        //        {
        //            _userManager.AddToRole(newUser.Id, role);
        //        }
        //    }

        //    return newUser.Id;
        //}
        /// <inheritdoc />
        public IdentityRole GetRole(string roleName)
        {
            return _context.Roles.FirstOrDefault(w => w.Name == roleName);
        }
        /// <inheritdoc />
        public void AddRole(string roleName)
        {
            _roleManager.Create(new IgorRole { Name = roleName });

            GetContext().SaveChanges();
        }

        /// <inheritdoc />
        //public void UpdateUser(IgorUser user, IEnumerable<string> roles)
        //{
        //    IgorUser userToUpdate = GetUser(user.Id);
        //    userToUpdate.FirstName = user.FirstName;
        //    userToUpdate.LastName = user.LastName;
        //    userToUpdate.Email = user.Email;
        //    userToUpdate.PhoneNumber = user.PhoneNumber;

        //    _userManager.Update(userToUpdate);
        //    if (roles != null)
        //    {
        //        var existingRoles = _userManager.GetRoles(user.Id);
        //        //first remove all roles that are not in the input list
        //        foreach (string existingrole in existingRoles)
        //        {
        //            if (existingrole != RoleConstants.AdminRole && !roles.Contains(existingrole)) _userManager.RemoveFromRole(userToUpdate.Id, existingrole);
        //        }

        //        foreach (string role in roles)
        //        {
        //            //Then add those roles to the DB model list
        //            if (!_userManager.IsInRole(userToUpdate.Id, role)) _userManager.AddToRole(userToUpdate.Id, role);
        //        }
        //    }
        //}

        /// <inheritdoc />
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return GetContext().Roles.ToList();
        }
        /// <inheritdoc />
        public IEnumerable<IdentityRole> GetAllNonAdminRoles()
        {
            return GetAllRoles().Where(w =>
                w.Name != RoleConstants.SuperAdminRole && w.Name != RoleConstants.AdminRole);
        }
        /// <inheritdoc />
        public async Task<bool> ChangePasswordAsync(string userid, string oldp, string newp)
        {
            var result = await _userManager.ChangePasswordAsync(userid, oldp, newp);
            if (result.Succeeded) return true;
            else return false;
        }
        /// <inheritdoc />
        public void ResetPassword(string userId, string newPassword)
        {

            IgorUser userToReset = GetUser(userId);

            userToReset.PasswordHash = _userManager.PasswordHasher.HashPassword(newPassword);
            _userManager.Update(userToReset);


        }
        /// <inheritdoc />
        public IEnumerable<IgorUser> GetUsersForRole(string roleName)
        {
            IgorRole role = roleName.IsNullOrEmpty() ? null : _roleManager?.FindByName(roleName);

            if (role != null)
            {
                var users = role.Users;
                var result = new List<IgorUser>();
                foreach (var item in users)
                {
                    result.Add(GetUser(item.UserId));
                }
                return result;
            }
            else return new List<IgorUser>();
        }
        /// <inheritdoc />
        public void RemoveUserFromRole(string userId, string roleName)
        {
            _userManager.RemoveFromRole(userId, roleName);
        }
        /// <inheritdoc />
        public void RemoveRole(string roleName)
        {
            var role = GetContext().Roles.FirstOrDefault(w => w.Name == roleName);
            GetContext().Roles.Remove(role);
            GetContext().SaveChanges();
        }

        #region JWT tokens

        ///<inheritdoc />
        //public async Task<IgorToken> GetIgorInitialTokenAsync(string userName, string password)
        //{
        //    IgorTokenApiRequest request = new IgorTokenApiRequest()
        //    {
        //        UserName = userName,
        //        Password = password
        //    };
        //    IgorTokenApiResponse httpResponse = await request.MakeJsonRestApiCall<IgorTokenApiResponse>();
        //    return httpResponse == null ? null : new IgorToken()
        //    {
        //        Token = httpResponse.Token,
        //        ErrorMessage = httpResponse.ErrorMessage,
        //        StatusCode = httpResponse.Status?.ToInt() ?? 500
        //    };
        //}

        ///<inheritdoc />
        //public async Task<IgorLoginResult> GetIgorUserFromJwtTokenAsync(IgorToken tokenResponse)
        //{
        //    IgorLoginApiResponse userInfo = await GetIgorUserInfoJwtAsync(tokenResponse);
        //    IgorUser user = userInfo == null ? null : GetUserByUsername(userInfo.UserName);
        //    return new IgorLoginResult()
        //    {
        //        Response = userInfo,
        //        User = user
        //    };
        //}
        ///<inheritdoc />
        //public async Task<NewIgorUser> GetIgorNewUserFromJwtTokenAsync(IgorToken tokenResponse)
        //{
        //    string token = tokenResponse?.Token;
        //    if (!token.IsNullOrEmpty())
        //    {
        //        IgorLoginApiResponse response = await GetIgorUserInfoJwtAsync(tokenResponse);
        //        if (response != null)
        //        {
        //            ICollection<IdentityRole> roles = GetAllNonAdminRoles().Where(w => w.Name == RoleConstants.CreatorRole)
        //                .ToList();
        //            NewIgorUser result = new NewIgorUser()
        //            {
        //                UserName = response.UserName,
        //                Email = response.Email,
        //                FirstName = response.FirstName,
        //                LastName = response.LastName,
        //                UserRoles = roles.Select(s => s.Name)
        //            };
        //            return result;
        //        }
        //    }

        //    return null;
        //}
        /// <summary>
        /// Get user info from CORD service using initial token.
        /// </summary>
        /// <param name="tokenResponse"></param>
        /// <returns></returns>
        //protected async Task<IgorLoginApiResponse> GetIgorUserInfoJwtAsync(IgorToken tokenResponse)
        //{
        //    IgorLoginApiRequest request = new IgorLoginApiRequest(tokenResponse);
        //    IgorLoginApiResponse response = await request.MakeJsonRestApiCall<IgorLoginApiResponse>();
        //    return response;
        //}
        /// <inheritdoc/>
        public string GenerateIgorJwtToken(IgorUser user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string issuer = ConfigurationManager.AppSettings["JwtIssuer"];
            string audienceId = ConfigurationManager.AppSettings["JwtAudienceId"];
            string secret = ConfigurationManager.AppSettings["JwtSecret"];
            ClaimsIdentity userClaims = new ClaimsIdentity(new IgorUserClaims(user).GetClaims());
            JwtSecurityToken generatedToken = handler.CreateJwtSecurityToken(issuer, audienceId, userClaims);

            return handler.WriteToken(generatedToken);
        }

        #endregion
    }
}
