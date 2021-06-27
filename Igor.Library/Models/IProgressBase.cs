using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// An base class for the source of a learning progress.
    /// </summary>
    public abstract class IProgressBase
    {
        /// <summary>
        /// Check whether an item represents specialization.
        /// </summary>
        /// <returns></returns>
        public bool IsSpecialization()
        {
            return this is Specialization;
        }
        /// <summary>
        /// Check whether an item represents Item.
        /// </summary>
        /// <returns></returns>
        public bool IsItem()
        {
            return this is Item;
        }
        /// <summary>
        /// Check whether an item represents item schema.
        /// </summary>
        /// <returns></returns>
        public bool IsItemSchema()
        {
            return this is ItemSchema;
        }
        /// <summary>
        /// Check whether an item represents lesson.
        /// </summary>
        /// <returns></returns>
        public bool IsLesson()
        {
            return this is Lesson;
        }
    }
}
