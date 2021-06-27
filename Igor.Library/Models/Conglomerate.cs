using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a conglomerate.
    /// </summary>
    public class Conglomerate
    {
        
        #region Properties
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int ConglomerateId { get; set; }
        /// <summary>
        /// Unique name of a conglomerate.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Abbreviation name.
        /// </summary>
        [Required]
        public string Abbreviation { get; set; }
        /// <summary>
        /// List of member factions of a conglomerate.
        /// </summary>
        public virtual List<Faction> MemberFactions { get; set; }
        /// <summary>
        /// Main coordinator.
        /// </summary>
        public virtual List<ConglomerateCoordination> Coordination { get; set; }
        /// <summary>
        /// Type of conglomerate profile.
        /// </summary>
        public List<FactionProfileTypes> Profiles { get; set; }
        #endregion

        #region Constructors
        public Conglomerate()
        {
            Profiles = new List<FactionProfileTypes>();
        }
        #endregion
    }
}
