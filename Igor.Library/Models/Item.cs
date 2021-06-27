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
    /// A class that represents a single item.
    /// </summary>
    public class Item : IProgressBase
    {
        #region Properties.
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int ItemId { get; set; }
        /// <summary>
        /// Name of item.
        /// </summary>
        [Required]
        [Index("Item_Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// Short description of an item.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Type of an item.
        /// </summary>
        public ItemTypes Type { get; set; }
        /// <summary>
        /// Commonness of an item.
        /// </summary>
        public ItemCommonnessTypes Commonness { get; set; }
        /// <summary>
        /// Determines whether item is active in the game.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Determines whether item has an RFID attached.
        /// </summary>
        public bool HasRfid { get; set; }
        /// <summary>
        /// Mechanics of an item, if any.
        /// </summary>
        public string Mechanics { get; set; }
        /// <summary>
        /// List of quests related to the item.
        /// </summary>
        public virtual List<Quest> Quests { get; set; }
        ///// <summary>
        ///// Schemas that take item as an input.
        ///// </summary>
        public virtual List<ItemSchema> CraftingInputSchemas { get; set; }
        /// <summary>
        /// Schemas that return item as an output.
        /// </summary>
        public virtual List<ItemSchema> CraftingOutputSchemas { get; set; }
        /// <summary>
        /// List of identification conditions of an item.
        /// </summary>
        public virtual List<ItemIdentificationCondition> IdentificationConditions  { get; set; }

        /// <summary>
        /// Additional comments.
        /// </summary>
        public string Comment { get; set; }
        #endregion

        #region Constructors

        public Item()
        {
            Type = ItemTypes.Unknown;
            Commonness = ItemCommonnessTypes.Unknown;
            IsActive = true;
            HasRfid = false;
        }
        #endregion
    }
}
