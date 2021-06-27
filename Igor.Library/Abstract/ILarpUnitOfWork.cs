using Igor.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.DataAccess;

namespace Igor.Library.Abstract
{
    public interface ILarpUnitOfWork
    {
        LarpContext GetContext();

        #region Repositories

        /// <summary>
        /// Accreditations.
        /// </summary>
        IRepository<Accreditation> AccreditationRepository { get; }
        /// <summary>
        /// Chatacters.
        /// </summary>
        IRepository<Character> CharacterRepository { get; }
        /// <summary>
        /// Conglomerates.
        /// </summary>
        IRepository<Conglomerate> ConglomerateRepository { get; }
        /// <summary>
        /// Craft conditions.
        /// </summary>
        IRepository<Condition> KnowledgeConditionRepository { get; }
        /// <summary>
        /// Craft conditions by domain.
        /// </summary>
        IRepository<ConditionDomain> KnowledgeConditionDomainRepository { get; }
        /// <summary>
        /// Editions.
        /// </summary>
        IRepository<Edition> EditionRepository { get; }
        /// <summary>
        /// Events.
        /// </summary>
        IRepository<Event> EventRepository { get; }
        /// <summary>
        /// Factions.
        /// </summary>
        IRepository<Faction> FactionRepository { get; }
        /// <summary>
        /// Faction coordinators.
        /// </summary>
        IRepository<FactionCoordination> FactionCoordinatorsRepository { get; }
        /// <summary>
        /// Conglomerate coordinators.
        /// </summary>
        IRepository<ConglomerateCoordination> ConglomerateCoordinatorsRepository { get; }
        /// <summary>
        /// Items.
        /// </summary>
        IRepository<Item> ItemRepository { get; }
        /// <summary>
        /// Item schemas.
        /// </summary>
        IRepository<ItemSchema> ItemSchemaRepository { get; }
        /// <summary>
        /// Languages.
        /// </summary>
        IRepository<Language> LanguageRepository { get; }
        /// <summary>
        /// Learning progresses.
        /// </summary>
        IRepository<LearningProgress> LearningProgressRepository { get; }
        /// <summary>
        /// Lessons.
        /// </summary>
        IRepository<Lesson> LessonRepository { get; }
        /// <summary>
        /// Igor users.
        /// </summary>
        IRepository<IgorUser> IgorUserRepository { get; }
        /// <summary>
        /// Perks.
        /// </summary>
        IRepository<Perk> PerkRepository { get; }
        /// <summary>
        /// Quests.
        /// </summary>

        IRepository<Quest> QuestRepository { get; }
        /// <summary>
        /// Skill fail penalties.
        /// </summary>
        IRepository<SkillFailPenalty> SkillFailPenaltyRepository { get; }
        /// <summary>
        /// Specializations.
        /// </summary>
        IRepository<Specialization> SpecializationRepository { get; }
        /// <summary>
        /// Success Curves.
        /// </summary>
        IRepository<SuccessCurve> SuccessCurveRepository { get; }
        /// <summary>
        /// Vehicles.
        /// </summary>
        IRepository<Vehicle> VehicleRepository { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Save changes of tracked entities to db.
        /// </summary>
        void Save();
        /// <summary>
        /// Save changes of tracked entities to db.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();
        /// <summary>
        /// Close DB connection and dispose of unit of work object.
        /// </summary>
        void Dispose();

        #endregion
    }
}
