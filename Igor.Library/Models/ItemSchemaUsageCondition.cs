using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a condition for using a schema.
    /// </summary>
    public class ItemSchemaUsageCondition : Condition
    {
        /// <summary>
        /// A schema that can be used.
        /// </summary>
        [Index]
        public virtual ItemSchema UsedItemSchema { get; set; }

        #region Constructors
        public ItemSchemaUsageCondition()
        {

        }

        public ItemSchemaUsageCondition(int total, int biology, int chemistry, int medicine, int technolgy) : base(
            total, biology, chemistry, medicine, technolgy)
        {

        }
        #endregion
    }
}
