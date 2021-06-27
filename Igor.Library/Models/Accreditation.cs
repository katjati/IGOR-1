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
    /// A class that represents an accreditation to an edition of an event.
    /// </summary>
    public class Accreditation
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int AccreditationId { get; set; }
        /// <summary>
        /// Parent edition of an event.
        /// </summary>
        [Required]
        [Index] 
        public virtual Edition Edition { get; set; }
        /// <summary>
        /// Id of a user.
        /// </summary>
        [Required]
        [Index]
        public virtual IgorUser IgorUser { get; set; }
        /// <summary>
        /// Role of a user in the event.
        /// </summary>
        [Required]
        public ParticipationTypes Type { get; set; }
        /// <summary>
        /// Datetime when accreditation was registered in the system.
        /// </summary>
        [Required]
        public DateTime Registered { get; set; }
        /// <summary>
        /// Unique identifier of accreditation if exists.
        /// </summary>
        //[Required]
        [Index]
        [MaxLength(50)]
        public string AccreditationIdentifier { get; set; }
        /// <summary>
        /// Determines whether accreditation is active.
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
        /// <summary>
        /// Active character associated with accreditation.
        /// </summary>
        public virtual Character ActiveCharacter { get; set; }
        /// <summary>
        /// Price of accreditation.
        /// </summary>
        public decimal Price { get; set; } = 0;

        #region Constructors
        public Accreditation() {
            IsActive = true;
            Type = ParticipationTypes.Unknown;
            AccreditationIdentifier = "";
        }
        #endregion

    }
}
