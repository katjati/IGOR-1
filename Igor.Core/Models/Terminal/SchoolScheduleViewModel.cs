using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Igor.Library.Models;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for school schedule.
    /// </summary>
    public class SchoolScheduleViewModel
    {
        #region Properties

        /// <summary>
        /// Determines whether user is a teacher.
        /// </summary>
        public bool IsTeacher { get; set; }
        /// <summary>
        /// Determines whether user is a headmaster.
        /// </summary>
        public bool IsHeadmaster { get; set; }
        /// <summary>
        /// Determines whether user is a polish language speaker.
        /// </summary>
        public bool IsForeigner { get; set; }
        /// <summary>
        /// Determines whether Lessons can be managed.
        /// </summary>
        public bool IsArchived { get; set; }
        /// <summary>
        /// A list of lessons available during the edition.
        /// </summary>
        public List<SchoolLessonViewModel> ScheduledLessons { get; set; }
        /// <summary>
        /// A list of class that are yet to take place.
        /// </summary>
        public List<SchoolLessonViewModel> ActiveScheduledLessons => ScheduledLessons?.Where(w => !w.IsArchived)?.ToList() ?? new List<SchoolLessonViewModel>();
        /// <summary>
        /// A list of class taught by a player.
        /// </summary>
        public List<SchoolLessonViewModel> TeacherScheduledLessons => ScheduledLessons?.Where(w => w.IsTeacher)?.ToList() ?? new List<SchoolLessonViewModel>();
        #endregion

        #region Constructors

        public SchoolScheduleViewModel()
        {
            IsTeacher = false;
            IsHeadmaster = false;
            ScheduledLessons = new List<SchoolLessonViewModel>();
        }

        #endregion

    }
    /// <summary>
    /// A view model fro a school lesson.
    /// </summary>
    public class SchoolLessonViewModel
    {
        /// <summary>
        /// Id of lesson.
        /// </summary>
        public int LessonId { get; set; }
        /// <summary>
        /// Topic of the class.
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// Domain type of the class.
        /// </summary>
        public DomainTypes Domain { get; set; }
        /// <summary>
        /// Determines whether attendance.
        /// </summary>
        public bool IsActiveAttendance { get; set; } /*=> EndTime < DateTime.Now;*/
        /// <summary>
        /// Language in which the class is conducted.
        /// </summary>
        public LanguageTypes Language { get; set; }
        /// <summary>
        /// Scheduled start of class.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Scheduled end of class.
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Name of the teacher conducting class.
        /// </summary>
        public string Teacher { get; set; }
        /// <summary>
        /// Determines whether user is a teacher of the class.
        /// </summary>
        public bool IsTeacher { get; set; }
        /// <summary>
        /// Determines whether lesson already took place.
        /// </summary>
        public bool IsArchived { get; set; }
        /// <summary>
        /// Additional information on the class visible t all users.
        /// </summary>
        public string AdditionalInformation { get; set; }
    }
}