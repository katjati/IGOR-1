using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Igor.Library.Models;

namespace Igor.Core.Models.Terminal
{
    /// <summary>
    /// A view model for a schema.
    /// </summary>
    public class ItemSchemasViewModel
    {
        /// <summary>
        /// List of available item schemas schemas.
        /// </summary>
        public List<ItemSchemaViewModel> ItemSchemas { get; set; }

        /// <summary>
        /// List of all known item schemas.
        /// </summary>
        public List<ItemSchemaViewModel> KnownItemSchemas => ItemSchemas.Where(w => w.IsKnown).ToList();

        /// <summary>
        /// List of commonly known item schemas.
        /// </summary>
        public List<ItemSchemaViewModel> CommonItemSchemas => ItemSchemas.Where(w => w.Attainability == AttainabilityTypes.Common).ToList();
        /// <summary>
        /// List of basic item schemas that can be learned.
        /// </summary>
        public List<ItemSchemaViewModel> BasicItemSchemas => ItemSchemas.Where(w => w.Attainability == AttainabilityTypes.Basic).ToList();
        /// <summary>
        /// If od user.
        /// </summary>
        public string UserId { get; set; }

        #region Constructors

        public ItemSchemasViewModel()
        {
            ItemSchemas = new List<ItemSchemaViewModel>();
        }

        #endregion
    }
    /// <summary>
    /// Single schema for craft.
    /// </summary>
    public class ItemSchemaViewModel
    {
        /// <summary>
        /// Id of schema.
        /// </summary>
        public int SchemaId { get; set; }
        /// <summary>
        /// Name of schema.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Output item on success.
        /// </summary>
        public List<ItemViewModel> OutputItems { get; set; }
        /// <summary>
        /// Component list.
        /// </summary>
        public List<ItemViewModel> InputItems { get; set; }
        /// <summary>
        /// Difficulty level for a character.
        /// </summary>

        public DifficultyLevels Difficulty { get; set; }
        /// <summary>
        /// Type of schema - scraping or crafting.
        /// </summary>
        public ItemSchemaTypes SchemaType { get; set; }
        /// <summary>
        /// Type of schema attainability.
        /// </summary>
        public AttainabilityTypes Attainability { get; set; }
        /// <summary>
        /// Short description of a schema.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Short information on mechanics of output item..
        /// </summary>
        public string Mechanics { get; set; }
        /// <summary>
        /// Skill level in performing schema.
        /// </summary>
        public decimal Progress { get; set; }
        /// <summary>
        /// Determines whether a character knows schema.
        /// </summary>
        public bool IsKnown { get; set; }
        /// <summary>
        /// Determines whether character collected enough points to learn the schema.
        /// </summary>
        public bool IsAvailable { get; set; }
        /// <summary>
        /// A list of domains that are required for acquiring the schema.
        /// </summary>
        public List<DomainTypes> Domains { get; set; }

        #region Constructors

        public ItemSchemaViewModel()
        {
            InputItems = new List<ItemViewModel>();
            OutputItems = new List<ItemViewModel>();
        }

        #endregion
    }
    /// <summary>
    /// View model of a single item.
    /// </summary>
    public class ItemViewModel
    {
        /// <summary>
        /// Item id.
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Item name.
        /// </summary>
        public string Name { get; set; }

        public ItemViewModel(Item item)
        {
            ItemId = item.ItemId;
            Name = item.Name;
        }
    }
}