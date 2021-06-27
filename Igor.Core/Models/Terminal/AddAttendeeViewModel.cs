using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Igor.Library.Models;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// View model containing information on a lesson attendee.
    /// </summary>
    public class AddAttendeeViewModel : CharacterBasicInfoViewModel
    {
        /// <summary>
        /// Id if a lesson.
        /// </summary>
        public int LessonId { get; set; }
        /// <summary>
        /// A modified to attendance.
        /// </summary>
        public decimal Modifier { get; set; }

        public AddAttendeeViewModel() { }
        public AddAttendeeViewModel(int lessonId)
        {
            LessonId = lessonId;
            Modifier = 1;
        }
    }
}