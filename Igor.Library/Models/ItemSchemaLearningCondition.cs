using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a single condition for learning a schema.
    /// </summary>
    public class ItemSchemaLearningCondition : Condition
    {
        /// <summary>
        /// Item schema that can be learned.
        /// </summary>
        [Index]
        public virtual ItemSchema LearnedItemSchema { get; set; }

        #region Constructors
        public ItemSchemaLearningCondition()
        {

        }

        public ItemSchemaLearningCondition(int total, int biology, int chemistry, int medicine, int technolgy) : base(
            total, biology, chemistry, medicine, technolgy)
        {

        }
        #endregion
    }

}
