using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that stores conditions in a single domain type.
    /// </summary>
    public class ConditionDomain
    {
        #region Properties

        /// <summary>
        /// Db id.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Type of knowledge domain.
        /// </summary>
        public DomainTypes Type { get; set; }
        /// <summary>
        /// Number of points.
        /// </summary>
        public int Points { get; set; }
        public virtual Condition ParentCondition { get; set; }

        #endregion

        #region Constructors
        public ConditionDomain(){}

        public ConditionDomain(DomainTypes domain, int points)
        {
            Type = domain;
            Points = points;
        }
        #endregion
    }
}
