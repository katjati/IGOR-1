using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Models;

namespace Igor.Library.Abstract
{
    public interface IIgorDataService
    {

        #region Get
        /// <summary>
        /// Get user by db id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IgorUser GetIgorUser(string userId);

        #region Character

        /// <summary>
        /// Get character by id.
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Character GetCharacter(int characterID);

        /// <summary>
        /// Get first character with a given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Character GetCharacterByName(string name);
        /// <summary>
        /// Get first character with a given name and faction.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="faction"></param>
        /// <returns></returns>
        Character GetCharacterByNameAndFaction(string name, Faction faction);
        /// <summary>
        /// Get active character assigned to accreditation special identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Character GetCharacterByAccreditationIdentifier(string identifier);

        #endregion

        #region Accreditation

        /// <summary>
        /// Get accreditation by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Accreditation GetAccreditation(int id);

        /// <summary>
        /// Get accreditation by special identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Accreditation GetAccreditationByIdentifier(string id);
        /// <summary>
        /// Get all accreditations for an active edition of an event registered under email address.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        List<Accreditation> GetAccreditationsByEmail(int eventId, string email);
        /// <summary>
        /// Get character's accreditation for current edition.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        Accreditation GetActiveAccreditationByCharacter(Character character);
        /// <summary>
        /// Get active accreditation for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Accreditation GetActiveAccreditationByUser(string userId);
        /// <summary>
        /// Get accreditations by an email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        List<Accreditation> GetActiveAccreditationsByEmail(string email, bool currentEdition);

        #endregion

        #region Language

        /// <summary>
        /// Gets polish language.
        /// </summary>
        /// <returns></returns>
        Language GetLanguagePl();

        /// <summary>
        /// Gets english language.
        /// </summary>
        /// <returns></returns>
        Language GetLanguageEN();
        #endregion

        LearningProgress GetProgress(int id);

        #region Lesson

        /// <summary>
        /// Get lesson by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Lesson GetLesson(int id);

        /// <summary>
        /// Get all lessons registered under knowledge domain. Ignores lessons from archived editions by default.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        List<Lesson> GetLessonsByDomain(DomainTypes domain, bool includeArchived = false);

        /// <summary>
        /// Get a list of lessons taught by a character. Ignores lessons from archived editions by default.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        List<Lesson> GetLessonsByTeacher(int teacherId, bool includeArchived = false);
        /// <summary>
        /// Get all lessons for current edition.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        List<Lesson> GetLessonsForCurrentEdition(bool includeArchived = false);
        /// <summary>
        /// Get all lessons by domain for current edition.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        List<Lesson> GetLessonsByDomainForCurrentEdition(DomainTypes domain, bool includeArchived = false);
        /// <summary>
        /// Get all lessons by teacher for current edition.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        List<Lesson> GetLessonsByTeacherForCurrentEdition(int teacherId, bool includeArchived = false);
        #endregion
        /// <summary>
        /// Returns a list of known domain types.
        /// </summary>
        /// <returns></returns>
        List<DomainTypes> GetDomainTypes();
        /// <summary>
        /// Returns a list of all known learning progress types.
        /// </summary>
        /// <returns></returns>
        List<LearningTypeTypes> GetLearningProgressTypes();

        #region ItemSchema

        /// <summary>
        /// Get item schema by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ItemSchema GetItemSchema(int id);

        /// <summary>
        /// Get all basic schemas.
        /// </summary>
        /// <returns></returns>
        IList<ItemSchema> GetItemSchemasByAttainability(AttainabilityTypes attainability);
        #endregion

        /// <summary>
        /// Get a list of dates when current edition takes place.
        /// </summary>
        /// <returns></returns>
        IList<DateTime> GetDatesForCurrentEdition();
        /// <summary>
        /// Get current edition.
        /// </summary>
        /// <returns></returns>
        Edition GetCurrentEdition();
        /// <summary>
        /// Get specialization by id.
        /// </summary>
        /// <param name="specializationId"></param>
        /// <returns></returns>
        Specialization GetSpecialization(int specializationId);
        /// <summary>
        /// Get all specializations.
        /// </summary>
        /// <param name="specializationId"></param>
        /// <returns></returns>
        List<Specialization> GetAllSpecializations();
        /// <summary>
        /// Get item schema by id.
        /// </summary>
        /// <param name="schemaId"></param>
        /// <returns></returns>
        //List<ItemSchema> GetItemSchema(int schemaId);
        /// <summary>
        /// Get all item schemas.
        /// </summary>
        /// <returns></returns>
        List<ItemSchema> GetAllItemSchemas();
        #endregion

        #region Update

        /// <summary>
        /// Update character object in db.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        bool UpdateCharacter(Character character);

        /// <summary>
        /// Update lesson object in db.
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        bool UpdateLesson(Lesson lesson);

        /// <summary>
        /// Update learning progress object in db.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        bool UpdateLearningProgress(LearningProgress progress);

        #endregion

        #region Delete

        /// <summary>
        /// Delete character from db.
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        bool DeleteCharacter(int characterID);

        /// <summary>
        /// Delete lesson from db.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        bool DeleteLesson(int lessonId);
        /// <summary>
        /// Set lesson's status to archived.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        bool ArchiveLesson(int lessonId);

        /// <summary>
        /// Delete learning progress from db.
        /// </summary>
        /// <param name="learningProgressId"></param>
        /// <returns></returns>
        bool DeleteLearningProgress(int learningProgressId);
        /// <summary>
        /// Inserts a new progress into database.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>

        bool AddLearningProgress(LearningProgress progress);
        /// <summary>
        /// Inserts a new specialization into database.
        /// </summary>
        /// <param name="specialization"></param>
        /// <returns></returns>
        bool AddSpecialization(Specialization specialization);

        #endregion

        /// <summary>
        /// Saves db.
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}
