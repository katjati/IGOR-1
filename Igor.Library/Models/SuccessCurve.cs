using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a success curve.
    /// </summary>
    public class SuccessCurve
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int SuccessCurveId { get; set; }
        /// <summary>
        /// Determines whether is default or custom value.
        /// </summary>
        public bool Default { get; set; }
        /// <summary>
        /// Min value.
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// Max value.
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// Type of curve used for calculation.
        /// </summary>
        public CurveTypes Type { get; set; }
    }
}
