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
    /// A class that represents a relation between faction and coordinators.
    /// </summary>
    public class FactionCoordination : Coordination
    {
        /// <summary>
        /// Coordinated faction.
        /// </summary>
        [Index]
        public virtual Faction Faction { get; set; }

        /// <summary>
        /// Main coordinator.
        /// </summary>
        [Index]
        [Required]
        public virtual IgorUser FactionCoordinator { get; set; }
    }
}
