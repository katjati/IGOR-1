using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int VehicleId { get; set; }
        /// <summary>
        /// Unique name of a vehicle.
        /// </summary>
        public string Name { get; set; }
    }
}
