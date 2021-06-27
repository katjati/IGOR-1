using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for adding a schema progress.
    /// </summary>
    public class AddSchemaViewModel : AddProgressViewModelBase
    {
        /// <summary>
        /// List of available schemas.
        /// </summary>
        public List<SelectListItem> Schemas { get; set; }
        /// <summary>
        /// Learned schema.
        /// </summary>
        public int Schema { get; set; }
    }
}