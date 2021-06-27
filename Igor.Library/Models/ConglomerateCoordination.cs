using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents an association between coordinators and conglomerates.
    /// </summary>
    public class ConglomerateCoordination : Coordination
    {

        /// <summary>
        /// Coordinated Conglomerate
        /// </summary>
        [Index]
        public virtual Conglomerate Conglomerate { get; set; }

        /// <summary>
        /// Main coordinator.
        /// </summary>
        [Index]
        [Required]
        public virtual IgorUser ConglomerateCoordinator { get; set; }
    }
}
