using Igor.Library.Abstract;
using Igor.Library.DataAccess.Repositories;
using Igor.Library.Global;
using Igor.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.DataAccess
{
    public class LarpUnitOfWork : ILarpUnitOfWork, IDisposable
    {
        #region Properties

        /// <summary>
        /// DB connection instance.
        /// </summary>
        private readonly LarpContext _context;
        /// <summary>
        /// Log file instance to log errors.
        /// </summary>
        private readonly ILog _uowLog;

        #endregion

        #region Constructors

        /// <summary>
        /// Default instance test or not.
        /// </summary>

        public LarpUnitOfWork(LarpContext context)
        {
            _context = context;
            _uowLog = new LogWriter(ConfigurationManager.AppSettings["LogFilePath"]);
        }

        #endregion

        #region Internal repositories

        private IRepository<Accreditation> _accreditationRepo;
        private IRepository<Character> _characterRepo;
        private IRepository<Conglomerate> _conglomerateRepo;
        private IRepository<Condition> _KnowledgeConditionRepo;
        private IRepository<ConditionDomain> _KnowledgeConditionDomainRepo;
        private IRepository<Edition> _editionRepo;
        private IRepository<Event> _eventRepo;
        private IRepository<Faction> _factionRepo;
        private IRepository<FactionCoordination> _factionCoordinatorsRepo;
        private IRepository<ConglomerateCoordination> _conglomerateCoordinatorsRepo;
        private IRepository<Item> _itemRepo;
        private IRepository<ItemSchema> _itemSchemaRepo;
        private IRepository<Language> _languageRepo;
        private IRepository<LearningProgress> _learningProgressRepo;
        private IRepository<Lesson> _lessonRepo;
        private IRepository<IgorUser> _igorUserRepo;
        private IRepository<Perk> _perkRepo;
        private IRepository<Quest> _questRepo;
        private IRepository<SkillFailPenalty> _skillFailPenaltyRepo;
        private IRepository<Specialization> _specializationRepo;
        private IRepository<SuccessCurve> _successCurveRepo;
        private IRepository<Vehicle> _veicleRepo;

        #endregion
        /// <inheritdoc />

        public LarpContext GetContext ()
        {
            return _context;
        }

        #region Repository implementations
        /// <inheritdoc />
        public IRepository<Accreditation> AccreditationRepository
        {
            get
            {
                if (this._accreditationRepo == null)
                {
                    this._accreditationRepo = new Repository<Accreditation>(_context);
                }
                return _accreditationRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Character> CharacterRepository
        {
            get
            {
                if (this._characterRepo == null)
                {
                    this._characterRepo = new Repository<Character>(_context);
                }
                return _characterRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Conglomerate> ConglomerateRepository
        {
            get
            {
                if (this._conglomerateRepo == null)
                {
                    this._conglomerateRepo = new Repository<Conglomerate>(_context);
                }
                return _conglomerateRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Condition> KnowledgeConditionRepository
        {
            get
            {
                if (this._KnowledgeConditionRepo == null)
                {
                    this._KnowledgeConditionRepo = new Repository<Condition>(_context);
                }
                return _KnowledgeConditionRepo ;
            }
        }
        /// <inheritdoc />
        public IRepository<ConditionDomain> KnowledgeConditionDomainRepository
        {
            get
            {
                if (this._KnowledgeConditionDomainRepo == null)
                {
                    this._KnowledgeConditionDomainRepo = new Repository<ConditionDomain>(_context);
                }
                return _KnowledgeConditionDomainRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Edition> EditionRepository
        {
            get
            {
                if (this._editionRepo == null)
                {
                    this._editionRepo = new Repository<Edition>(_context);
                }
                return _editionRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Event> EventRepository
        {
            get
            {
                if (this._eventRepo == null)
                {
                    this._eventRepo = new Repository<Event>(_context);
                }
                return _eventRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Faction> FactionRepository
        {
            get
            {
                if (this._factionRepo == null)
                {
                    this._factionRepo = new Repository<Faction>(_context);
                }
                return _factionRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<FactionCoordination> FactionCoordinatorsRepository
        {
            get
            {
                if (this._factionCoordinatorsRepo == null)
                {
                    this._factionCoordinatorsRepo = new Repository<FactionCoordination>(_context);
                }
                return _factionCoordinatorsRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<ConglomerateCoordination> ConglomerateCoordinatorsRepository
        {
            get
            {
                if (this._conglomerateCoordinatorsRepo == null)
                {
                    this._conglomerateCoordinatorsRepo = new Repository<ConglomerateCoordination>(_context);
                }
                return _conglomerateCoordinatorsRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Item> ItemRepository
        {
            get
            {
                if (this._itemRepo == null)
                {
                    this._itemRepo = new Repository<Item>(_context);
                }
                return _itemRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<ItemSchema> ItemSchemaRepository
        {
            get
            {
                if (this._itemSchemaRepo == null)
                {
                    this._itemSchemaRepo = new Repository<ItemSchema>(_context);
                }
                return _itemSchemaRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Language> LanguageRepository
        {
            get
            {
                if (this._languageRepo == null)
                {
                    this._languageRepo = new Repository<Language>(_context);
                }
                return _languageRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<LearningProgress> LearningProgressRepository
        {
            get
            {
                if (this._learningProgressRepo == null)
                {
                    this._learningProgressRepo = new Repository<LearningProgress>(_context);
                }
                return _learningProgressRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Lesson> LessonRepository
        {
            get
            {
                if (this._lessonRepo == null)
                {
                    this._lessonRepo = new Repository<Lesson>(_context);
                }
                return _lessonRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<IgorUser> IgorUserRepository
        {
            get
            {
                if (this._igorUserRepo == null)
                {
                    this._igorUserRepo = new Repository<IgorUser>(_context);
                }
                return _igorUserRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Perk> PerkRepository
        {
            get
            {
                if (this._perkRepo == null)
                {
                    this._perkRepo = new Repository<Perk>(_context);
                }
                return _perkRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Quest> QuestRepository
        {
            get
            {
                if (this._questRepo == null)
                {
                    this._questRepo = new Repository<Quest>(_context);
                }
                return _questRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<SkillFailPenalty> SkillFailPenaltyRepository
        {
            get
            {
                if (this._skillFailPenaltyRepo == null)
                {
                    this._skillFailPenaltyRepo = new Repository<SkillFailPenalty>(_context);
                }
                return _skillFailPenaltyRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<Specialization> SpecializationRepository
        {
            get
            {
                if (this._specializationRepo == null)
                {
                    this._specializationRepo = new Repository<Specialization>(_context);
                }
                return _specializationRepo;
            }
        }
        /// <inheritdoc />
        public IRepository<SuccessCurve> SuccessCurveRepository
        {
            get
            {
                if (this._successCurveRepo == null)
                {
                    this._successCurveRepo = new Repository<SuccessCurve>(_context);
                }
                return _successCurveRepo;
            }
        }

        /// <inheritdoc />
        public IRepository<Vehicle> VehicleRepository
        {
            get
            {
                if (this._veicleRepo == null)
                {
                    this._veicleRepo = new Repository<Vehicle>(_context);
                }
                return _veicleRepo;
            }
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public async Task<int> SaveAsync()
        {
            try
            {
                if (_context.Configuration.AutoDetectChangesEnabled == false) _context.ChangeTracker.DetectChanges();
                return await _context.SaveChangesAsync();
            }
            catch (Exception dbex)
            {
                if (_uowLog != null) await _uowLog.LogExceptionAsync("Error saving to DB. ", dbex, LogLevels.Exception);
                throw;
            }
        }
        /// <inheritdoc/>
        public void Save()
        {
            //var valid = context.GetValidationErrors();
            try
            {
                if (_context.Configuration.AutoDetectChangesEnabled == false) _context.ChangeTracker.DetectChanges();
                _context.SaveChanges();
            }
            catch (Exception dbex)
            {
                _uowLog?.LogException("Error saving to DB. ", dbex, LogLevels.Exception);
                throw;
            }
        }
        /// <summary>
        /// Determines whether current unit of work is disposed or not.
        /// </summary>
        private bool _disposed = false;
        /// <summary>
        /// Close DB connection and dispose of unit of work object.
        /// </summary>
        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        /// <summary>
        /// Close DB connection and dispose of unit of work object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
