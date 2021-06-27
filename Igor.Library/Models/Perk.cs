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
    /// A class that represents character's unique perk.
    /// </summary>
    public class Perk
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int PerkId { get; set; }
        /// <summary>
        /// Name of perk.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Character that owns a perk.
        /// </summary>
        [Required] 
        [Index]
        public virtual Character Character { get; set; }
        /// <summary>
        /// Special mechanic provided by a perk.
        /// </summary>
        public string Mechanic { get; set; }
        /// <summary>
        /// Explanation behind the perk.
        /// </summary>
        public string Background { get; set; }
        /// <summary>
        /// Approval date of a perk.
        /// </summary>
        public DateTime ApprovalTimestamp { get; set; }
        /// <summary>
        /// Person who approved the perk.
        /// </summary>
        [Required] 
        public virtual IgorUser ApprovedBy { get; set; }
        /// <summary>
        /// Additional comments.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Type of a perk.
        /// </summary>
        public PerkTypes PerkType { get; set; } = PerkTypes.Individual;
    }
}
