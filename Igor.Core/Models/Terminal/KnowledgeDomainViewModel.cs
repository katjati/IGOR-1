using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Igor.Library.Global;
using Igor.Library.Helpers;
using Igor.Library.Models;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for character's progress in a domain.
    /// </summary>
    public class KnowledgeDomainViewModel
    {
        #region Properties

        /// <summary>
        /// Total calculated level of domain knowledge.
        /// </summary>
        public decimal Level { get; set; }
        /// <summary>
        /// Level of domain knowledge displayed in a format provided in cofnfiguration.
        /// </summary>
        public string LevelDisplay => Level.GetProgressRepresentation();
        /// <summary>
        /// Domain type of progress.
        /// </summary>
        public DomainTypes Domain { get; set; }

        /// <summary>
        /// Detailed data on progress.
        /// </summary>
        public List<LearningTypeViewModel> LearningTypesDetails { get; set; }

        public List<LearningTypeViewModel> LearningTypeDetailsFilterd => LearningTypesDetails?.Where(w => w.Type.IsKnownToUser())?.ToList() ?? new List<LearningTypeViewModel>();

        #endregion
        #region Constructors

        public KnowledgeDomainViewModel()
        {
            LearningTypesDetails = new List<LearningTypeViewModel>();
            Level = 0;
        }

        #endregion

    }
    /// <summary>
    /// A view model for a single learning type containing progress information.
    /// </summary>
    public class LearningTypeViewModel
    {
        #region Properties

        /// <summary>
        /// A learning type of a progress.
        /// </summary>
        public LearningTypeTypes Type { get; set; }
        /// <summary>
        /// Number of active learning points.
        /// </summary>
        public decimal ActiveCount { get; set; }
        /// <summary>
        /// Total progress.
        /// </summary>
        public decimal TotalCount { get; set; }

        #endregion

        #region Constructors

        public LearningTypeViewModel()
        {
            ActiveCount = 0;
            TotalCount = 0;
        }

        #endregion
    }
}