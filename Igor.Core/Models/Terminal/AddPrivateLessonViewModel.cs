using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for adding a private lesson.
    /// </summary>
    public class AddPrivateLessonViewModel : AddProgressViewModelBase
    {
        /// <summary>
        /// Name of a student.
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// Name of a teacher.
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// List of available schemas.
        /// </summary>
        public List<SelectListItem> Schemas { get; set; }
        /// <summary>
        /// Learned schema.
        /// </summary>
        public int Schema { get; set; }
        /// <summary>
        /// Determines whether a student learned a schema.
        /// </summary>
        public bool IsSchemaLearned { get; set; }
}
}