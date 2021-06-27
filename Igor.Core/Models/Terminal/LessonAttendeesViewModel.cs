using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Web;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for a list of lesson's attendees.
    /// </summary>
    public class LessonAttendeesViewModel
    {
        /// <summary>
        /// Id of lesson.
        /// </summary>
        public int LessonId { get; set; }
        /// <summary>
        /// Topic of lesson.
        /// </summary>
        public string LessonTopic { get; set; }
        public SchoolLessonViewModel LessonModel { get; set; }
        /// <summary>
        /// List of attendees.
        /// </summary>
        public List<LessonAttendeeViewModel> Attendees { get; set; }
    }
    /// <summary>
    /// A view model for a lesson attendee.
    /// </summary>
    public class LessonAttendeeViewModel
    {
        /// <summary>
        /// I of student.
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Id of learning progress associated with a character attending class.
        /// </summary>
        public int ProgressId { get; set; }
        /// <summary>
        /// Name of student.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Student's faction name.
        /// </summary>
        public string Faction { get; set; }
        /// <summary>
        /// Activity modifier.
        /// </summary>
        public decimal Modifier { get; set; }
        /// <summary>
        /// Date of approval.
        /// </summary>
        public DateTime ApprovalDate { get; set; }
        /// <summary>
        /// Determines whether attendance list can be modified.
        /// </summary>
        public bool IsActiveAttendance { get; set; }
    }
}