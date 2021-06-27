using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Igor.Library.Models;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A base for view models adding progress.
    /// </summary>
    public class AddProgressViewModelBase
    {
        /// <summary>
        /// If of character that is to acquire specialization.
        /// </summary>
        public int CharacterId { get; set; }
        /// <summary>
        /// Id of player performing an action.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// List of available domains.
        /// </summary>
        public List<SelectListItem> Domains { get; set; }
        /// <summary>
        /// Internal comment.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Description of a progress.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Domain associated with a progress.
        /// </summary>
        public DomainTypes Domain { get; set; }
        /// <summary>
        /// Progress modifier value.
        /// </summary>
        public decimal Modifier { get; set; }
        /// <summary>
        /// Number of learning points assigned with the progress,
        /// </summary>
        public int LearningPoints { get; set; }
    }
}