using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Global;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a system user.
    /// </summary>
    public class IgorUser : IdentityUser
    {
        #region Properties
        /// <summary>
        /// A list of characters played by a player.
        /// </summary>
        public virtual List<Character> Characters { get; set; }
        /// <summary>
        /// First name of a user.
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of a user.
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Person to contact in case of emergency.
        /// </summary>
        public string EmergencyContactPerson { get; set; }

        /// <summary>
        /// Telephone number to a person to contact in case of an emergency.
        /// </summary>
        public string EmergencyContactTelephoneNumber { get; set; }
        /// <summary>
        /// Information on medical conditions and medicaments.
        /// </summary>
        public string MedicalInformation { get; set; }
        /// <summary>
        /// A list of accreditations of a user.
        /// </summary>
        public virtual List<Accreditation> Accreditations { get; set; }
        /// <summary>
        /// A list of quests coordinated by a user.
        /// </summary>
        public virtual List<Quest> CoordinatedQuests { get; set; }
        /// <summary>
        /// List of coordinated factions history.
        /// </summary>
        public virtual List<FactionCoordination> CoordinatedFactions { get; set; }
        /// <summary>
        /// List of coordinated conglomerates history.
        /// </summary>
        public virtual List<ConglomerateCoordination> CoordinatedConglomerates { get; set; }
        /// <summary>
        /// List of acquired perks.
        /// </summary>
        public virtual List<Perk> ApprovedPerks { get; set; }
        /// <summary>
        /// Determines whether user is a polish speaker or not.
        /// </summary>
        public bool IsForeigner { get; set; }
        #endregion

        #region Constructors
        public IgorUser()
        {
            IsForeigner = false;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Generate claims for the user.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public ClaimsIdentity GenerateUserIdentity(UserManager<IgorUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);

            if (!this.Id.IsNullOrEmpty()) userIdentity.AddClaim(new Claim("UserId", this.Id));
            if (!this.FirstName.IsNullOrEmpty()) userIdentity.AddClaim(new Claim("FirstName", this.FirstName));
            if (!this.LastName.IsNullOrEmpty()) userIdentity.AddClaim(new Claim("LastName", this.LastName));
            return userIdentity;
        }

        #endregion
    }
}
