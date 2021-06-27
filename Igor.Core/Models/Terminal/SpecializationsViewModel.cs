using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Igor.Library.Models;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view modl for specializations.
    /// </summary>
    public class SpecializationsViewModel
    {
        /// <summary>
        /// List of specializations.
        /// </summary>
        public List<SpecializationViewModel> Specializations { get; set; }
    }

    public class SpecializationViewModel
    {
        #region Properties
        /// <summary>
        /// Specialization name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Specialization description.
        /// </summary>
        public string Description { get; set; }
        public int Id { get; set; }
        #endregion
        #region Constructors
        public SpecializationViewModel()
        {
        }
        public SpecializationViewModel(LearningProgress progress)
        {
            if (progress.Type == LearningTypeTypes.Specialization)
            {
                Name = progress.Specialization.Name;
                Description = progress.Description;
                Id = progress.LearningProgressId;
            }
        }

        #endregion
    }

}