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
    /// A base class that represents a learning progress.
    /// </summary>
    public class LearningProgress
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int LearningProgressId { get; set; }
        /// <summary>
        /// Character that obtained knowledge.
        /// </summary>
        [Index]
        [Required]
        public virtual Character Character { get; set; }

        /// <summary>
        /// Learning method.
        /// </summary>
        [Required] 
        [Index]
        public LearningTypeTypes Type { get; set; }
        /// <summary>
        /// Modifier of progress value. 1 by default.
        /// </summary>
        public decimal Modifier { get; set; } = 1;
        /// <summary>
        /// Domain of obtained knowledge.
        /// </summary>
        [Index]
        [Required]
        public DomainTypes Domain { get; set; }
        /// <summary>
        /// Lesson as a result of which progress has been registered. Optional, in case of school.
        /// </summary>
        public virtual Lesson Lesson { get; set; }
        /// <summary>
        /// Specialization as a result of which progress has been registered. Optional, in case of specialization.
        /// </summary>
        public virtual Specialization Specialization { get; set; }
        /// <summary>
        /// Item as a result of identifying which a progress has been registered. Optional, in case of identification.
        /// </summary>
        public virtual Item IdentifiedItem { get; set; }
        /// <summary>
        /// Schema as a result of practicing which a progress has been registered. Optional, in case of schema.
        /// </summary>
        public virtual ItemSchema Schema { get; set; }
        /// <summary>
        /// Date and time when progress was approved.
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Person who approved the progress.
        /// </summary>
        [Required]
        public virtual IgorUser ApprovedBy { get; set; }
        /// <summary>
        /// Id of edition of an event during which the progress was registered.
        /// </summary>
        [Required]
        public virtual Edition Edition { get; set; }
        /// <summary>
        /// Additional comments hidden from players.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Information on progress in case of unique actions.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Determines whether action was successful, in case of crafting or identification.
        /// </summary>
        public bool IsSuccessful { get; set; } = true;
        /// <summary>
        /// Determines whether progress provides active learning points.
        /// </summary>
        public decimal ActiveLearningPoints { get; set; }
        /// <summary>
        /// Determines whether progress has any learning points to be spent.
        /// </summary>
        public bool HasActiveLearningPoints => ActiveLearningPoints > 0;
    }
}
