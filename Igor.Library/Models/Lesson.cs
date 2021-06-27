using Igor.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library
{
    /// <summary>
    /// A class that represents a school lesson.
    /// </summary>
    public class Lesson : IProgressBase
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int LessonId { get; set; }
        /// <summary>
        /// Character that obtained knowledge.
        /// </summary>
        [Required] 
        public virtual Character Teacher { get; set; }
        /// <summary>
        /// Modulus of a lesson.
        /// </summary>
        public DomainTypes Domain { get; set; }
        /// <summary>
        /// Language in which class was conducted.
        /// </summary>
        public LanguageTypes Language { get; set; } = LanguageTypes.Polish;
        /// <summary>
        /// Scheduled start of class.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Scheduled end of class.
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Main topic of the class.
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// Additional information on class available to all players.
        /// </summary>
        public string AdditionalInformation { get; set; }
        /// <summary>
        /// Additional comment on the class.
        /// </summary>
        public string Comment  {get; set;}
        /// <summary>
        /// Edition of an event during which the class was conducted.
        /// </summary>
        [Required] 
        public virtual Edition Edition { get; set; }
        /// <summary>
        /// List of students progresses associated with the class.
        /// </summary>
        public virtual List<LearningProgress> Progresses { get; set; }
        /// <summary>
        /// Determines whether lesson has been removed from user's display.
        /// </summary>
        public bool IsArchived { get; set; } = false;


    }
}
