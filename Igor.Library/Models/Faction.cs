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
    /// A class that represents a faction.
    /// </summary>
    public class Faction
    {
        #region Properties
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int FactionId { get; set; }
        /// <summary>
        /// Unique name of a faction.
        /// </summary>
        [Required]
        [Index("Faction_Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// Type of Faction profile.
        /// </summary>
        public List<FactionProfileTypes> Profiles { get; set; }
        /// <summary>
        /// Conglomerate the faction is currently a member of.
        /// </summary>
        public virtual Conglomerate Conglomerate { get; set; }
        /// <summary>
        /// Catch phrase of Faction.
        /// </summary>
        public string Motto { get; set; }
        /// <summary>
        /// Link to a website or to a fanpage.
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// List of faction members.
        /// </summary>
        public virtual List<Character> Characters { get; set; }
        /// <summary>
        /// List of quests owned by a faction.
        /// </summary>
        public virtual List<Quest> Quests { get; set; }
        /// <summary>
        /// Faction coordinators on particular editions of events.
        /// </summary>
        public virtual List<FactionCoordination> Coordination { get; set; }
        /// <summary>
        /// Determines whether faction no longer exists.
        /// </summary>
        public bool IsArchived { get; set; } = false;
        #endregion

        #region Cnstructors
        public Faction()
        {
            Profiles = new List<FactionProfileTypes>();
        }
        #endregion
    }
}
