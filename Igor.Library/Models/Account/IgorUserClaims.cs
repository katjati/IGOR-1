using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    class IgorUserClaims
    {
        #region Properties

        /// <summary>
        /// First name of user.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of user.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User e-mail.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User ID.
        /// </summary>
        public string Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default instance from user data.
        /// </summary>
        /// <param name="user"></param>
        public IgorUserClaims(IgorUser user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Id = user.Id;
            Email = user.Email;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get list of claims from the user.
        /// </summary>
        /// <returns></returns>
        public IList<Claim> GetClaims()
        {
            return new List<Claim>()
            {
                new Claim("FirstName", FirstName ?? string.Empty),
                new Claim("LastName", LastName ?? string.Empty),
                new Claim("Email", Email ?? string.Empty),
                new Claim("Id", Id ?? string.Empty),
            };
        }

        #endregion
    }
}
