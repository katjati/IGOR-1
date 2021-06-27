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
    /// A class that represents a quest.
    /// </summary>
    public class Quest
    {
        #region Properties
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int QuestId { get; set; }
        /// <summary>
        /// Unique name of a quest.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Short description of a quest.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Determines whether quest is archived.
        /// </summary>
        public bool IsArchived { get; set; } = false;
        /// <summary>
        /// Determines whether quest is currently active.
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// A list of factions participating in coordinating a quest.
        /// </summary>
        public virtual List<Faction> Factions { get; set; }
        /// <summary>
        /// Quest coordinator
        /// </summary>
        public virtual IgorUser Coordinator { get; set; }
        /// <summary>
        /// List of items related to the quest.
        /// </summary>
        public virtual List<Item> Items { get; set; }
        #endregion

        #region Constructors
        public Quest()
        {
            IsActive = false;
            IsArchived = false;
        }
        #endregion
    }
}
