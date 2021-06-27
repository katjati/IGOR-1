using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for character's management.
    /// </summary>
    public class ManageCharacterProgressViewModel
    {
        #region Properties
        /// <summary>
        ///  Id of character.
        /// </summary>
        public int CharacterId { get; set; }
        /// <summary>
        /// Name of character.
        /// </summary>
        public string CharacterName { get; set; }
        /// <summary>
        /// Faction of character.
        /// </summary>
        public string CharacterFaction { get; set; }
        /// <summary>
        /// List of progresses of a character by domain type.
        /// </summary>
        public List<KnowledgeDomainViewModel> KnowledgeDomainProgresses { get; set; }
        /// <summary>
        /// List of character's specializations.
        /// </summary>
        public List<SpecializationViewModel> Specializations { get; set; }
        /// <summary>
        /// List of known item schemas of a character.
        /// </summary>
        public List<ItemSchemaViewModel> KnownItemSchemas { get; set; }
        /// <summary>
        /// List of available item schemas of a character.
        /// </summary>
        public List<ItemSchemaViewModel> AvailableItemSchemas { get; set; }
        #endregion

        #region Constructors

        public ManageCharacterProgressViewModel()
        {
            AvailableItemSchemas = new List<ItemSchemaViewModel>();
            KnowledgeDomainProgresses = new List<KnowledgeDomainViewModel>();
            KnownItemSchemas = new List<ItemSchemaViewModel>();
            Specializations = new List<SpecializationViewModel>();
        }

        #endregion
    }
}