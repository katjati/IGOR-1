using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents available learning points of a character.
    /// </summary>
    public class AvailableKnowledgePoints
    {
        public List<AvailableKnowledgePointsDomains> DomainPoints { get; set; }
        /// <summary>
        /// Sum of all learning points.
        /// </summary>
        public decimal Total => DomainPoints.Sum(s=>s.Value);

        #region Constructors

        public AvailableKnowledgePoints()
        {
            DomainPoints = new List<AvailableKnowledgePointsDomains>();
        }
        #endregion
    }
    /// <summary>
    /// A class that represents learning points in a single domain.
    /// </summary>
    public class AvailableKnowledgePointsDomains
    {
        public DomainTypes Domain { get; set; }
        public decimal Value { get; set; }


        public AvailableKnowledgePointsDomains(){}

        public AvailableKnowledgePointsDomains(DomainTypes domain, decimal value)
        {
            Domain = domain;
            Value = value;
        }

    }
}
