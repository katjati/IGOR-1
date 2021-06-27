using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Igor.Library.Models;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for adding new specialization.
    /// </summary>
    public class AddSpecializationViewModel : AddProgressViewModelBase
    {
        /// <summary>
        /// List of known specializations.
        /// </summary>
        public List<SelectListItem> Specializations { get; set; }
        /// <summary>
        /// Name of a new specialization
        /// </summary>
        public string NewSpecializationName { get; set; }
        /// <summary>
        /// Determines whether a new specialization should be created.
        /// </summary>
        public bool IsNewSpecialization { get; set; } = false;
        /// <summary>
        /// If of a selected specialization
        /// </summary>
        public int SpecializationId { get; set; }
        /// <summary>
        /// Determines whether specialization is visible to players.
        /// </summary>
        public bool IsHidden { get; set; } = false;
    }
}