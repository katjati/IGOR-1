using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Global;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a schema used for crafting items.
    /// </summary>
    public class ItemSchema : IProgressBase
    {
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int ItemSchemaId { get; set; }
        /// <summary>
        /// Name of Item schema.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Short description of item schema.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Item consisting of components.
        /// </summary>
        public virtual List<Item> OutputItems { get; set; }
        /// <summary>
        /// List of components of Item.
        /// </summary>
        public virtual List<Item> InputItems { get; set; }
        /// <summary>
        /// Parameters of production time.
        /// </summary>
        //[ForeignKey("ProductionTime")]
        public virtual SuccessCurve ProductionTime { get; set; }
        /// <summary>
        /// Parameters of success rate.
        /// </summary>
        //[ForeignKey("SuccessRate")]
        public virtual SuccessCurve SuccessRate { get; set; }
        /// <summary>
        /// List of conditions needed to obtain a schema from learning points in case of basic schemas.
        /// </summary>
        public virtual List<ItemSchemaLearningCondition> LearnConditions { get; set; } = new List<ItemSchemaLearningCondition>();
        /// <summary>
        /// List of conditions needed to use a schema.
        /// </summary>
        public virtual List<ItemSchemaUsageCondition> UsageConditions { get; set; } = new List<ItemSchemaUsageCondition>();
        /// <summary>
        /// A list of characters knowing the schema.
        /// </summary>
        public virtual List<Character> KnownBy { get; set; } = new List<Character>();

        /// <summary>
        /// Type of schema - crafting or scraping.
        /// </summary>
        public ItemSchemaTypes SchemaType { get; set; } = ItemSchemaTypes.Crafting;

        /// <summary>
        /// Type of schema attainability.
        /// </summary>
        public AttainabilityTypes Attainability { get; set; } = AttainabilityTypes.Secret;

        #region Methods

        public string GetSchemaName()
        {
            if (Name.IsNullOrEmpty())
                return OutputItems.FirstOrDefault()?.Name + (SchemaType == ItemSchemaTypes.Crafting ? " crafting" : " scraping") + " Item schema.";
            return Name;
        }
#endregion
    }
}
