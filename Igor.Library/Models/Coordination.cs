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
    /// A base class for conditions for schemas.
    /// </summary>
    public abstract class Coordination

    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int CoordinatorId { get; set; }
        /// <summary>
        /// Edition of an event for coordination data.
        /// </summary>
        [Index]
        [Required]
        public virtual Edition Edition { get; set; }
        /// <summary>
        /// Additional comments.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Determines whether coordinator is a main coordinator or a support.
        /// </summary>
        public bool IsMainCoordinator { get; set; } = true;
    }
}
