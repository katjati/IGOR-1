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
    /// A class that represents an event that might be reoccurring and hosts a larp.
    /// </summary>
    public class Event
    {
        #region Properties
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int EventId { get; set; }
        ///// <summary>
        ///// Unique name of a specialization.
        ///// </summary>
        [Required]
        [Index("Event_Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// List of editions of an event.
        /// </summary>
        public virtual List<Edition> Editions { get; set; }
        /// <summary>
        /// Main organizer of an event.
        /// </summary>
        public virtual IgorUser MainOrganizer { get; set; }
#endregion

        #region Constructors
        public Event()
        {
        }
        #endregion
    }
}
