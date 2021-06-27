using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A basic view model with characters information.
    /// </summary>
    public class CharacterBasicInfoViewModel
    {
        /// <summary>
        /// Character's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Character's faction.
        /// </summary>
        public string Faction { get; set; }
        /// <summary>
        /// Character's id in db.
        /// </summary>
        public int Id { get; set; }
}
}
