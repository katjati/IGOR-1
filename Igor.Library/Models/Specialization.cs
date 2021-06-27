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
    /// A class that represents a specialization.
    /// </summary>
    public class Specialization : IProgressBase
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int SpecializationId { get; set; }
        /// <summary>
        /// Unique name of a specialization.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Determines whether information is hidden from a player.
        /// </summary>
        public bool IsHidden { get; set; }
        /// <summary>
        /// A list of learning progresses resulting in acquiring the specialization.
        /// </summary>
        public virtual List<LearningProgress> LearnedBy { get; set; } = new List<LearningProgress>();
        /// <summary>
        /// A list of learning progresses resulting in acquiring the specialization.
        /// </summary>
        [NotMapped]
        public virtual List<Character> OwnedBy => LearnedBy.Where(w => w.Type == LearningTypeTypes.Specialization)
            .Select(s => s.Character).ToList();


        public Specialization(){}

        public Specialization(string name)
        {
            Name = name;
        }
    }
}
