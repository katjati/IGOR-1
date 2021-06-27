using Igor.Library.Abstract;
using Igor.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Global;
using Igor.Library.Models.Shared;

namespace Igor.Library.Services
{
    public class IgorDataService : IIgorDataService
    {
        #region Properties

        /// <summary>
        /// Unit of work instance.
        /// </summary>
        private readonly ILarpUnitOfWork _uow;

        #endregion

        #region Constructors
        /// <summary>
        /// Default instance.
        /// </summary>
        /// <param name="uow"></param>
        public IgorDataService(ILarpUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion

        #region Methods

        #region Get

        /// <inheritdoc/>
        public IgorUser GetIgorUser(string userId)
        {
            if (userId == null) return null;
            return _uow.IgorUserRepository.GetFirstOrLocal(w => w.Id == userId);
        }

        #region Character
        /// <inheritdoc/>
        public Character GetCharacter(int characterID)
        {
            return _uow.CharacterRepository.GetFirstOrLocal(w => w.CharacterId == characterID);
        }
        /// <inheritdoc/>
        public Character GetCharacterByName(string name)
        {
            if (name == null) return null;
            return _uow.CharacterRepository.GetFirstOrLocal(w => w.Name.ToLower() == name.ToLower());
        }
        /// <inheritdoc/>
        public Character GetCharacterByNameAndFaction(string name, Faction faction)
        {
            if (name == null || faction == null) return null;
            return _uow.CharacterRepository.GetFirstOrLocal(w => w.Name == name && w.Faction.FactionId == faction.FactionId);
        }
        /// <inheritdoc/>
        public Character GetCharacterByAccreditationIdentifier(string identifier)
        {
            if (identifier == null) return null;
            return GetAccreditationByIdentifier(identifier)?.ActiveCharacter;
        }
        #endregion

        #region Accreditation
        /// <inheritdoc/>
        public Accreditation GetAccreditation(int id)
        {
            return _uow.AccreditationRepository.GetFirstOrLocal(w=>w.AccreditationId == id);
        }
        /// <inheritdoc/>
        public Accreditation GetAccreditationByIdentifier(string id)
        {
            return _uow.AccreditationRepository.GetFirstOrLocal(w => w.AccreditationIdentifier == id);
        }
        /// <inheritdoc/>
        public List<Accreditation> GetAccreditationsByEmail(int eventId, string email)
        {
            return _uow.AccreditationRepository.Get(w => w.IgorUser.Email == email && !w.Edition.IsArchived && w.Edition.Event.EventId == eventId)?.ToList();
        }
        /// <inheritdoc/>
        public Accreditation GetActiveAccreditationByCharacter(Character character)
        {
            int editionId = IgorSettings.CurrentEditionId.ToInt();
            if (editionId == 0 || character == null) return null;
            return _uow.AccreditationRepository.GetFirstOrLocal(w => w.ActiveCharacter != null && w.ActiveCharacter.CharacterId == character.CharacterId && w.Edition.EditionId == editionId);
        }
        /// <inheritdoc/>
        public Accreditation GetActiveAccreditationByUser(string userId)
        {
            int editionId = IgorSettings.CurrentEditionId.ToInt();
            if (editionId == 0 || userId.IsNullOrEmpty()) return null;
            return _uow.AccreditationRepository.GetFirstOrLocal(w => w.IgorUser != null && w.IgorUser.Id == userId && w.Edition.EditionId == editionId);
        }
        /// <inheritdoc/>
        public List<Accreditation> GetActiveAccreditationsByEmail(string email, bool isCurrentEdition)
        {
            if(!isCurrentEdition)
                return _uow.AccreditationRepository.Get(w => w.IgorUser.Email == email)?.ToList();
            int editionId = IgorSettings.CurrentEditionId.ToInt();
            if (editionId == 0 || email.IsNullOrEmpty()) return null;
            return _uow.AccreditationRepository.Get(w => w.IgorUser.Email == email && w.Edition.EditionId == editionId)?.ToList();
        }
        #endregion

        #region Language
        /// <inheritdoc/>
        public Language GetLanguagePl()
        {
            return _uow.LanguageRepository.GetFirstOrLocal(w => w.Abbreviation == "pl");
        }
        /// <inheritdoc/>
        public Language GetLanguageEN()
        {
            return _uow.LanguageRepository.GetFirstOrLocal(w => w.Abbreviation == "en");
        }
        #endregion
        /// <inheritdoc/>
        public LearningProgress GetProgress(int id)
        {
            return _uow.LearningProgressRepository.GetFirstOrLocal(w => w.LearningProgressId == id);
        }

        #region Lesson
        /// <inheritdoc/>
        public Lesson GetLesson(int id)
        {
            return _uow.LessonRepository.GetFirstOrLocal(w => w.LessonId == id);
        }
        /// <inheritdoc/>
        public List<Lesson> GetLessonsByDomain(DomainTypes domain, bool includeArchived = false)
        {
            if(includeArchived)
            return _uow.LessonRepository.Get(w => w.Domain == domain).ToList();
            return _uow.LessonRepository.Get(w => w.Domain == domain && !w.Edition.IsArchived && !w.IsArchived).ToList();
        }
        /// <inheritdoc/>
        public List<Lesson> GetLessonsByTeacher(int teacherId, bool includeArchived = false)
        {
            if(includeArchived)
                return _uow.LessonRepository.Get(w => w.Teacher.CharacterId == teacherId).ToList();
            return _uow.LessonRepository.Get(w => w.Teacher.CharacterId == teacherId && !w.Edition.IsArchived).ToList();
        }
        /// <inheritdoc/>
        public List<Lesson> GetLessonsForCurrentEdition(bool includeArchived = false)
        {
            int editionId = IgorSettings.CurrentEditionId.ToInt();
            if (editionId == 0) return null;
            if (!includeArchived)
            {
                DateTime now = DateTime.Now;
                return _uow.LessonRepository.Get(w => w.Edition.EditionId == editionId && w.Edition.EditionId == editionId && !w.IsArchived).ToList();
            }
            return _uow.LessonRepository.Get(w=>w.Edition.EditionId == editionId).ToList();
        }
        /// <inheritdoc/>
        public List<Lesson> GetLessonsByDomainForCurrentEdition(DomainTypes domain, bool includeArchived = false)
        {
            int editionId = IgorSettings.CurrentEditionId.ToInt();
            if (editionId == 0) return null;
            if (!includeArchived)
            {
                DateTime now = DateTime.Now;
                return _uow.LessonRepository.Get(w => w.Domain == domain && w.Edition.EditionId == editionId && !w.IsArchived).ToList();
            }
            return _uow.LessonRepository.Get(w => w.Domain == domain && w.Edition.EditionId == editionId).ToList();

        }
        /// <inheritdoc/>
        public List<Lesson> GetLessonsByTeacherForCurrentEdition(int teacherId, bool includeArchived = false)
        {
            int editionId = IgorSettings.CurrentEditionId.ToInt();
            if (editionId == 0) return null;
            if (!includeArchived)
            {
                DateTime now = DateTime.Now;
                return _uow.LessonRepository.Get(w => w.Teacher.CharacterId == teacherId && w.Edition.EditionId == editionId && !w.IsArchived).ToList();
            }
            return _uow.LessonRepository.Get(w => w.Teacher.CharacterId == teacherId && w.Edition.EditionId == editionId).ToList();
        }
        #endregion
        /// <inheritdoc/>
        public List<DomainTypes> GetDomainTypes()
        {
            return new List<DomainTypes>() {DomainTypes.Biology, DomainTypes.Chemistry, DomainTypes.Medicine, DomainTypes.Technology};
        }

        /// <inheritdoc/>
        public List<LearningTypeTypes> GetLearningProgressTypes()
        {
            return new List<LearningTypeTypes>() { LearningTypeTypes.Crafting, LearningTypeTypes.Identification, LearningTypeTypes.Library, LearningTypeTypes.Mentoring, LearningTypeTypes.Merit, LearningTypeTypes.School, LearningTypeTypes.Specialization, LearningTypeTypes.Teaching };
        }
        #region ItemSchema

        /// <inheritdoc/>
        public ItemSchema GetItemSchema(int id)
        {
            return _uow.ItemSchemaRepository.GetFirstOrLocal(w => w.ItemSchemaId == id);
        }
        /// <inheritdoc/>
        public IList<ItemSchema> GetItemSchemasByAttainability(AttainabilityTypes attainabilityType)
        {
            return _uow.ItemSchemaRepository.Get(w => w.Attainability == attainabilityType)?.ToList();
        }
        #endregion

        /// <inheritdoc/>
        public IList<DateTime> GetDatesForCurrentEdition()
        {
            try
            {
                int editionId = IgorSettings.CurrentEditionId.ToInt();
                if (editionId == 0) return null;
                Edition edition = _uow.EditionRepository.GetByID(editionId, false);
                if (edition == null || edition.DateStart > edition.DateEnd) return null;
                return Enumerable.Range(0, (edition.DateEnd - edition.DateStart).Days + 1)
                    .Select(day => edition.DateStart.AddDays(day)).ToList();
            }
            catch
            {
                return null;
            }
        }
        /// <inheritdoc/>
        public Edition GetCurrentEdition()
        {
            int editionId = IgorSettings.CurrentEditionId.ToInt();
            if (editionId == 0) return null; 
            return _uow.EditionRepository.GetByID(editionId, false);
        }

        /// <inheritdoc/>
        public Specialization GetSpecialization(int specializationId)
        {
            return _uow.SpecializationRepository.GetFirstOrLocal(g=>g.SpecializationId == specializationId);
        }
        /// <inheritdoc/>
        public List<Specialization> GetAllSpecializations()
        {
            return _uow.SpecializationRepository.Get()?.ToList() ?? new List<Specialization>();
        }

        /// <summary>
        /// Get all item schemas.
        /// </summary>
        /// <returns></returns>
        public List<ItemSchema> GetAllItemSchemas()
        {
            return _uow.ItemSchemaRepository.Get()?.ToList() ?? new List<ItemSchema>();
        }
        #endregion

        #region Update

        /// <inheritdoc/>
        public bool UpdateCharacter(Character character)
        {
            try
            {
                if (character != null)
                {
                    if(character.Faction != null)
                    _uow.FactionRepository.Attach(character.Faction);
                    if(character.Player != null)
                    _uow.IgorUserRepository.Attach(character.Player);
                    _uow.CharacterRepository.Attach(character);
                    _uow.CharacterRepository.Update(character);
                    _uow.Save();
                    return true;
                }
            }
            catch (Exception e) { }
            return false;
        }
        /// <inheritdoc/>
        public bool UpdateLesson(Lesson lesson)
        {
            try
            {
                if (lesson != null)
                {
                    if (lesson.LessonId > 0)
                    {
                        _uow.LessonRepository.Update(lesson);
                    }
                    else
                    {
                        _uow.LessonRepository.Insert(lesson);
                    }

                    _uow.Save();
                    return true;
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }

        /// <inheritdoc/>
        public bool Save()
        {
            try
            {
                _uow.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <inheritdoc/>
        public bool UpdateLearningProgress(LearningProgress progress)
        {
            try
            {
                if (progress != null)
                {
                    progress.TimeStamp = DateTime.Now;
                    _uow.IgorUserRepository.Attach(progress.ApprovedBy);
                    _uow.EditionRepository.Attach(progress.Edition);
                    _uow.CharacterRepository.Attach(progress.Character);
                    if(progress.Lesson != null)
                        _uow.LessonRepository.Attach(progress.Lesson);
                    _uow.LearningProgressRepository.Attach(progress);
                    _uow.LearningProgressRepository.Update(progress);
                    //_uow.Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                
            }
            return false;
        }
        /// <inheritdoc/>
        public bool ArchiveLesson(int lessonId)
        {
            try
            {
                Lesson lesson = GetLesson(lessonId);
                if (lesson != null)
                {
                    _uow.LessonRepository.Attach(lesson);
                    _uow.CharacterRepository.Attach(lesson.Teacher);
                    _uow.EditionRepository.Attach(lesson.Edition);
                    _uow.LessonRepository.Delete(lesson);
                    _uow.Save();
                    return true;
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }

        #endregion

        #region Delete

        /// <inheritdoc/>
        public bool DeleteCharacter(int characterID)
        {
            if (characterID > 0)
            {
                try
                {
                    Character character = GetCharacter(characterID);
                    if (character != null)
                    {
                        _uow.CharacterRepository.Delete(character);
                        _uow.Save();
                        return true;
                    }
                }
                catch (Exception e)
                {
                }
            }

            return false;
        }
        /// <inheritdoc/>
        public bool DeleteLesson(int lessonId)
        {
            if (lessonId > 0)
            {
                try
                {
                    Lesson lesson = GetLesson(lessonId);
                    if (lesson != null)
                    {
                        _uow.LessonRepository.Delete(lesson);
                        _uow.Save();
                        return true;
                    }
                }
                catch (Exception e)
                {

                }
            }

            return false;
        }
        /// <inheritdoc/>
        public bool DeleteLearningProgress(int learningProgressId)
        {
            if (learningProgressId > 0)
            {
                try
                {
                    _uow.LearningProgressRepository.Delete(learningProgressId);
                    _uow.Save();
                    return true;
                }
                catch (Exception e)
                {

                }
            }

            return false;
        }

        #endregion

        #region Add
        /// <inheritdoc/>
        public bool AddLearningProgress(LearningProgress progress)
        {
            try
            {
                if (progress != null)
                {
                    progress.TimeStamp = DateTime.Now;
                    _uow.CharacterRepository.Attach(progress.Character);
                    _uow.IgorUserRepository.Attach(progress.ApprovedBy);
                    _uow.EditionRepository.Attach(progress.Edition);
                    if(progress.Lesson != null)
                        _uow.LessonRepository.Attach(progress.Lesson);

                    _uow.LearningProgressRepository.Insert(progress);
                    _uow.Save();
                    return true;
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }

        /// <inheritdoc/>
        public bool AddSpecialization(Specialization specialization)
        {
            try
            {
                if (specialization != null)
                {
                    _uow.SpecializationRepository.Insert(specialization);
                    _uow.Save();
                    return true;
                }
            }
            catch (Exception e)
            {

            }

            return false;
        }
        #endregion

        #endregion
    }
}
