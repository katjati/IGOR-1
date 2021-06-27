using Igor.Library.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.DataAccess
{
    [DbConfigurationType(typeof(LarpMySqlConfiguration))]
    public class LarpContext : IdentityDbContext<IgorUser>
    {
        #region Properties

        /// <summary>
        /// List of supported languages.
        /// </summary>
        public DbSet<Language> Languages { get; set; }

        /// <summary>
        /// Items.
        /// </summary>
        public DbSet<Item> Items { get; set; }
        /// <summary>
        /// Item schema.
        /// </summary>
        public DbSet<ItemSchema> ItemSchemas { get; set; }

        /// <summary>
        /// IgorUser.
        /// </summary>
        //public DbSet<IgorUser> IgorUsers { get; set; }

        /// <summary>
        /// IgorUser.
        /// </summary>
        public DbSet<Conglomerate> Conglomerates { get; set; }
        /// <summary>
        /// IgorUser.
        /// </summary>
        public DbSet<Faction> Factions { get; set; }
        /// <summary>
        /// IgorUser.
        /// </summary>
        public DbSet<Character> Characters { get; set; }
        /// <summary>
        /// Coordination.
        /// </summary>
        public DbSet<ConglomerateCoordination> ConglomerateCoordinators { get; set; }
        /// <summary>
        /// Coordination.
        /// </summary>
        public DbSet<FactionCoordination> FactionCoordinators { get; set; }
        /// <summary>
        /// Event.
        /// </summary>
        public DbSet<Event> Events { get; set; }
        /// <summary>
        /// Edition.
        /// </summary>
        public DbSet<Edition> Editions { get; set; }
        /// <summary>
        /// Accreditation.
        /// </summary>
        public DbSet<Accreditation> Accreditations { get; set; }
        /// <summary>
        /// Lesson.
        /// </summary>
        public DbSet<Lesson> Lessons { get; set; }
        /// <summary>
        /// LearningProgress.
        /// </summary>
        public DbSet<LearningProgress> LearningProgresses { get; set; }
        /// <summary>
        /// Perks.
        /// </summary>
        public DbSet<Perk> Perks { get; set; }
        /// <summary>
        /// Specialization.
        /// </summary>
        public DbSet<Specialization> Specializations { get; set; }
        #endregion
        /// <summary>
        /// Default instance with standard DB.
        /// </summary>
        public LarpContext() : base("LarpContext", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            Database.SetInitializer(new LarpSqlInitializer());
            Database.SetInitializer(new LarpSqlInitDataInitializer());
            
            Database.CommandTimeout = 300000;
        }
        /// <summary>
        /// Fluent api to define relations.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
            modelBuilder.Entity<Event>().HasMany(h => h.Editions).WithRequired(r => r.Event).WillCascadeOnDelete(true);

            modelBuilder.Entity<Edition>().HasMany(h => h.Accreditations).WithRequired(r => r.Edition).WillCascadeOnDelete(true);
            
            modelBuilder.Entity<ItemSchema>().HasMany(h => h.LearnConditions).WithRequired(r => r.LearnedItemSchema).WillCascadeOnDelete(true);
            modelBuilder.Entity<ItemSchema>().HasMany(h => h.UsageConditions).WithRequired(r => r.UsedItemSchema).WillCascadeOnDelete(true);

            modelBuilder.Entity<Item>().HasMany(h => h.IdentificationConditions).WithRequired(r => r.IdentifiedItem).WillCascadeOnDelete(true);
            modelBuilder.Entity<Item>().HasMany(h => h.CraftingInputSchemas).WithMany(w => w.InputItems).Map(m => m.MapLeftKey("ItemId").MapRightKey("ItemSchemaId").ToTable("Item_ItemSchema_Input"));
            modelBuilder.Entity<Item>().HasMany(h => h.CraftingOutputSchemas).WithMany(w => w.OutputItems).Map(m => m.MapLeftKey("ItemId").MapRightKey("ItemSchemaId").ToTable("Item_ItemSchema_Output"));
            
            modelBuilder.Entity<IgorUser>().HasMany(h => h.Accreditations).WithRequired(r => r.IgorUser).WillCascadeOnDelete(false);
            modelBuilder.Entity<IgorUser>().HasMany(h => h.Characters).WithRequired(r => r.Player).WillCascadeOnDelete(false);
            modelBuilder.Entity<IgorUser>().HasMany(h => h.ApprovedPerks).WithRequired(r => r.ApprovedBy).WillCascadeOnDelete(false);
            modelBuilder.Entity<IgorUser>().HasMany(h => h.CoordinatedFactions).WithRequired(r => r.FactionCoordinator).WillCascadeOnDelete(true);
            modelBuilder.Entity<IgorUser>().HasMany(h => h.CoordinatedConglomerates).WithRequired(r => r.ConglomerateCoordinator).WillCascadeOnDelete(true);
            modelBuilder.Entity<IgorUser>().HasMany(h => h.CoordinatedQuests).WithRequired(r => r.Coordinator).WillCascadeOnDelete(false);
       
            modelBuilder.Entity<Faction>().HasMany(h => h.Quests).WithMany(w => w.Factions).Map(m => m.MapLeftKey("FactionId").MapRightKey("QuestId").ToTable("factions_quests"));
            modelBuilder.Entity<Faction>().HasMany(h => h.Coordination).WithRequired(r => r.Faction).WillCascadeOnDelete(true);

            modelBuilder.Entity<Conglomerate>().HasMany(h => h.Coordination).WithRequired(r => r.Conglomerate).WillCascadeOnDelete(true);

            modelBuilder.Entity<Condition>().HasMany(h => h.DomainConditions).WithRequired(r => r.ParentCondition).WillCascadeOnDelete(true);
            
            modelBuilder.Entity<Character>().HasMany(h => h.Perks).WithRequired(r => r.Character).WillCascadeOnDelete(true);
            modelBuilder.Entity<Character>().HasMany(h => h.LearningProgresses).WithRequired(r => r.Character).WillCascadeOnDelete(true);
            modelBuilder.Entity<Character>().HasMany(h => h.KnownSchemas).WithMany(w => w.KnownBy).Map(m => m.MapLeftKey("ItemSchemaId").MapRightKey("CharacterId").ToTable("character_schema"));

            modelBuilder.Entity<Quest>().HasMany(h => h.Items).WithMany(w => w.Quests).Map(m => m.MapLeftKey("QuestId").MapRightKey("ItemId").ToTable("items_quests"));
        }
    }

    public class LarpSqlInitializer : IDatabaseInitializer<LarpContext>
    {
        public void InitializeDatabase(LarpContext context)
        {
            if (!context.Database.Exists()) context.Database.Create();
        }
    }

    public class LarpSqlInitDataInitializer : CreateDatabaseIfNotExists<LarpContext>
    {
 
    }
}