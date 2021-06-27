using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model with information necessary to generate a progress view for a character.
    /// </summary>
    public class CharacterProgressViewModel
    {
        #region Properties
        /// <summary>
        /// Db id of character.
        /// </summary>
        public int CharacterId { get; set; }
        /// <summary>
        /// List of progresses of a character by domain type.
        /// </summary>
        public List<KnowledgeDomainViewModel> KnowledgeDomainProgresses { get; set; }
        /// <summary>
        /// A list of specializations of a character.
        /// </summary>
        public List<SpecializationViewModel> Specializations { get; set; }

        #endregion

        #region Constructors

        public CharacterProgressViewModel()
        {
            KnowledgeDomainProgresses = new List<KnowledgeDomainViewModel>();
        }

        #endregion
    }
    

}