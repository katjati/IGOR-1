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
    /// A class that represents an edition of an event.
    /// </summary>
    public class Edition
    {
        #region Properties
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int EditionId { get; set; }
        /// <summary>
        /// Unique name of an event.
        /// </summary>
        [Required]
        [Index("Edition_Name")]
        [MaxLength(50)]
        string Name { get; set; }
        /// <summary>
        /// Parent event.
        /// </summary>
        [Required]
        [Index]
        public virtual Event Event { get; set; }
        /// <summary>
        /// Event start.
        /// </summary>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// Event end.
        /// </summary>
        public DateTime DateEnd { get; set; }
        /// <summary>
        /// List of accreditations for the edition.
        /// </summary>
        public virtual List<Accreditation> Accreditations { get; set; }
        /// <summary>
        /// Determines whether edition is a past event.
        /// </summary>
        public bool IsArchived { get; set; }
        #endregion

        #region Constructors
        public Edition()
        {
            IsArchived = false;
        }
        #endregion
    }
}
