using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace Igor.Library.Models
{
    public class Character
    {
        ///A class that represents a character.
        #region Properties
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int CharacterId { get; set; }
        /// <summary>
        /// Unique name of a character.
        /// </summary>
        [Index("Character_Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// IgorUser that plays the character.
        /// </summary>
        [Index]
        [Required]
        public virtual IgorUser Player { get; set; }
        /// <summary>
        /// Determines whether a character is an NPC or a regular character.
        /// </summary>
        public bool IsNpc { get; set; }
        /// <summary>
        /// Faction the character is affiliated with.
        /// </summary>
        [Index]
        public virtual Faction Faction { get; set; }
        /// <summary>
        /// Race of a character.
        /// </summary>
        public RaceTypes Race { get; set; }
        /// <summary>
        /// Current status of a character.
        /// </summary>
        public CharacterStatusTypes Status {get;set;}
        /// <summary>
        /// List of special perks of a character.
        /// </summary>
        public virtual List<Perk> Perks { get; set; }
        /// <summary>
        /// List of learning progresses obtained by a character.
        /// </summary>
        public virtual List<LearningProgress> LearningProgresses { get; set; }
        /// <summary>
        /// A list of schemas known to a character.
        /// </summary>
        public virtual List<ItemSchema> KnownSchemas { get; set; }
        /// <summary>
        /// A list of specializations owned by a character.
        /// </summary>
        [NotMapped]
        public virtual List<Specialization> Specializations => LearningProgresses
            .Where(w => w.Type == LearningTypeTypes.Specialization).Select(s => s.Specialization).DistinctBy(d => d.SpecializationId).ToList();
        /// <summary>
        /// A list of lessons attended by a character.
        /// </summary>
        [NotMapped]
        public virtual List<Lesson> AttendedLessons => LearningProgresses
            .Where(w => w.Type == LearningTypeTypes.School).Select(s => s.Lesson).ToList();
        /// <summary>
        /// A list of items identified by a character.
        /// </summary>
        [NotMapped]
        public virtual List<Item> ItemsIdentified => LearningProgresses
            .Where(w => w.Type == LearningTypeTypes.Crafting).Select(s => s.IdentifiedItem).ToList();
        /// <summary>
        /// A list of artifacts crafted by a character.
        /// </summary>
        [NotMapped]
        public virtual List<ItemSchema> ArtifactsCrafted => LearningProgresses
            .Where(w => w.Type == LearningTypeTypes.Crafting).Select(s => s.Schema).ToList();
        /// <summary>
        /// A list of lessons conducted by a character.
        /// </summary>
        [NotMapped]
        public virtual List<Lesson> LessonsConducted => LearningProgresses
            .Where(w => w.Type == LearningTypeTypes.Teaching).Select(s => s.Lesson).ToList();

        #endregion

        #region Constructors
        public Character()
        {
            IsNpc = false;
            Race = RaceTypes.Human;
            Status = CharacterStatusTypes.Active;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a list of learning progresses associated with a given learning type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<LearningProgress> GetLearningProgressesByType(LearningTypeTypes type)
        {
            return LearningProgresses.Where(w => w.Type == type).ToList();
        }
        /// <summary>
        /// Returns a number of available learning points of a character.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public decimal GetKnowledgePointsByType(DomainTypes domain)
        {
            return LearningProgresses.Where(w => w.Domain == domain && w.HasActiveLearningPoints).Sum(s=>s.ActiveLearningPoints);
        }
        
        #endregion
    }
}
