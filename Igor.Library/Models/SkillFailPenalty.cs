using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a penalty for a failed crafting test.
    /// </summary>
    public class SkillFailPenalty
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int SkillFailPenaltyId { get; set; }
        /// <summary>
        /// Type of penalty for a failed skill action.
        /// </summary>
        public SkillFailPenaltyTypes Type { get; set; }
        /// <summary>
        /// Value of penalty of a certain type.
        /// </summary>
        public int Value { get; set; }
    }
}
