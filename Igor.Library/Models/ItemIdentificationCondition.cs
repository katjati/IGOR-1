using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a single condition to learn a schema.
    /// </summary>
    public class ItemIdentificationCondition : Condition
    {
        /// <summary>
        /// A schema that can be learned.
        /// </summary>
        public virtual Item IdentifiedItem { get; set; }

        #region Constructors

        public ItemIdentificationCondition()
        {

        }

        public ItemIdentificationCondition(int total, int biology, int chemistry, int medicine, int technolgy) : base(
            total, biology, chemistry, medicine, technolgy)
        {

        }

        #endregion

    }
}
