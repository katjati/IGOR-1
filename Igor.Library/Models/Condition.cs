using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Global;

namespace Igor.Library.Models
{
    /// <summary>
    /// A base class for conditions.
    /// </summary>
    public abstract class Condition
    {
        #region Properties

        /// <summary>
        /// Db id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Determines whether is default or custom value.
        /// </summary>
        public bool IsDefault { get; set; } = false;
        /// <summary>
        /// A list of learning points by domain.
        /// </summary>
        public virtual List<ConditionDomain> DomainConditions { get; set; }
        /// <summary>
        /// Specialization required.
        /// </summary>
        public virtual Specialization Specialization { get; set; }
        /// <summary>
        /// Total number of points required.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Determines whether points from domains not represented in DomainConditions can be included in calculation.
        /// </summary>
        public bool AllowMixing { get; set; } = false;

        #endregion

        #region Constructors
        public Condition()
        {
        }
        public Condition(int total, int biology, int chemistry, int medicine, int technology)
        {
            Total = total;
            if (DomainConditions.IsNullOrEmpty())
                DomainConditions = new List<ConditionDomain>();
            if(biology > 0)
                DomainConditions.Add(new ConditionDomain(DomainTypes.Biology, biology));
            if(chemistry > 0)
                DomainConditions.Add(new ConditionDomain(DomainTypes.Chemistry, chemistry));
            if(medicine > 0)
                DomainConditions.Add(new ConditionDomain(DomainTypes.Medicine, medicine));
            if(technology > 0)
                DomainConditions.Add(new ConditionDomain(DomainTypes.Technology, technology));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of domains obligatory for fulfilling conditions.
        /// </summary>
        /// <returns></returns>
        public List<DomainTypes> GetRelatedDomains()
        {
            return DomainConditions.Where(w => w.Points > 0).Select(s => s.Type).ToList();
        }

        #endregion

    }
}
