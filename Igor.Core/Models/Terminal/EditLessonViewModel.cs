using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Igor.Library.Helpers;
using Igor.Library.Models;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for editing a lesson.
    /// </summary>
    public class EditLessonViewModel
    {
        /// <summary>
        /// Db id of lesson if edited.
        /// </summary>
        public int LessonId { get; set; }
        /// <summary>
        /// Name fo teacher.
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// Db id of teacher.
        /// </summary>
        public int TeacherId { get; set; } = 0;
        /// <summary>
        /// Time of lesson start saved on client side.
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// Language of the lesson saved on client side.
        /// </summary>
        public LanguageTypes Language { get; set; }
        /// <summary>
        /// Day when lesson will be conducted saved on client side.
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// Domain string saved on client side.
        /// </summary>
        public DomainTypes Domain { get; set; }
        /// <summary>
        /// Determines whether lesson is being added or edited.
        /// </summary>
        public bool IsEdited => LessonId > 0;
        /// <summary>
        /// Main topic of the class.
        /// </summary>
        [Required]
        public string Topic { get; set; }
        /// <summary>
        /// Additional information on class available to all players.
        /// </summary>
        public string AdditionalInformation { get; set; }
        /// <summary>
        /// Additional comment on the class.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Scheduled start of class.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Scheduled end of class.
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Length of lesson in minutes.
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// List of available lesson domains to select.
        /// </summary>
        public List<SelectListItem> Domains { get; set; }
        /// <summary>
        /// List of days when lesson can be conducted based on edition's dates.
        /// </summary>
        public List<SelectListItem> Dates { get; set; }
        /// <summary>
        /// List of available languages in which lesson can be conducted.
        /// </summary>
        public List<SelectListItem> Languages { get; set; }

        #region Constructors

        public EditLessonViewModel()
        {
            //IsEdited = false;
            LessonId = 0;
            Duration = 45;

            List<DomainTypes> domains = new List<DomainTypes>() {DomainTypes.Biology, DomainTypes.Chemistry, DomainTypes.Medicine, DomainTypes.Technology};
            Domains = domains.Select(s=>new SelectListItem(){Text = s.GetDomainNameString(), Value = s.GetDomainNameString()}).ToList();
            List<string> languages = new List<string>() {"pl", "en"};
            Languages = languages.Select(s=>new SelectListItem(){Text = s, Value = s}).ToList();
            Dates = new List<SelectListItem>();
        }
#endregion
    }
}