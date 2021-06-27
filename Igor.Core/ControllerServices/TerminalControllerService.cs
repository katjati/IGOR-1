using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using Igor.Core.Models.Terminal;
using Igor.Library;
using Igor.Library.Abstract;
using Igor.Library.Global;
using Igor.Library.Helpers;
using Igor.Library.Models;
using Igor.Library.Models.Shared;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace Igor.Core.ControllerServices
{
    public class TerminalControllerService
    {
        #region Properties

        /// <summary>
        /// Instance of the Igor production service.
        /// </summary>
        private readonly IIgorDataService _IgorDataService;
        /// <summary>
        /// Instance of user service.
        /// </summary>
        private readonly IUserService _userService;

        #endregion

        #region Constructors

        /// <summary>
        /// Default instance.
        /// </summary>
        /// <param name="igorDataService"></param>
        public TerminalControllerService(IIgorDataService igorDataService, IUserService uService)
        {
            _IgorDataService = igorDataService;
            _userService = uService;
        }

        #endregion

        #region Methods 

        #region School

        #region School schedule
        /// <summary>
        /// Get school schedule view model filled with data.
        /// </summary>
        /// <returns></returns>
        public SchoolScheduleViewModel GetSchoolScheduleViewModel()
        {
            SchoolScheduleViewModel result = new SchoolScheduleViewModel()
            {
                IsArchived = IgorSettings.IsDebugMode ? false : _IgorDataService.GetCurrentEdition().DateEnd.AddDays(7) > DateTime.Now, 
            };
            Character character = null;
            if (_userService.IsLogged())
            {
                character = _IgorDataService.GetActiveAccreditationByUser(_userService.GetCurrentUser()?.Id)?.ActiveCharacter;
                result.IsHeadmaster = _userService.GetCurrentUserRoles()?.Contains(RoleConstants.HeadmasterRole) ?? false;
                result.IsTeacher = character?.IsTeacher() ?? false;
            }
            List<Lesson> lessonList = _IgorDataService.GetLessonsForCurrentEdition();
            result.ScheduledLessons.AddRange(lessonList.Select(s => GetSchoolLessonViewModel(s, character)));
            result.IsForeigner = character?.Player?.IsForeigner ?? false;
            result.ScheduledLessons = result.ScheduledLessons.OrderBy(o => o.StartTime).ToList();
            return result;
        }
        /// <summary>
        /// Gets a school lesson view model filled with data.
        /// </summary>
        /// <param name="lesson">Db id of a lesson</param>
        /// <param name="character">Instance of character for whom a view model will be generated.</param>
        /// <returns></returns>
        protected SchoolLessonViewModel GetSchoolLessonViewModel(Lesson lesson, Character character)
        {
            if (lesson == null) return null;
            string[] larpdays = GetLarpWeekDays().Select(s => s.Value).ToArray();
            Random random = new Random();
            int i = random.Next(larpdays.Length);
            DateTime tempDate = DateTime.MaxValue;
            string date = larpdays[i];
            DateTime.TryParse(date, out tempDate);
            return new SchoolLessonViewModel
            {
                Domain = lesson.Domain,
                IsActiveAttendance = IgorSettings.IsDebugMode ? true : (lesson.StartTime < DateTime.Now && DateTime.Now < lesson.StartTime.AddHours(24)),
                Teacher = lesson.Teacher.Name,
                Topic = lesson.Topic,
                StartTime = lesson.StartTime,
                EndTime = lesson.EndTime,
                Language = lesson.Language,
                LessonId = lesson.LessonId,
                IsTeacher = lesson.Teacher.CharacterId == (character?.CharacterId ?? -1),
                AdditionalInformation = lesson.AdditionalInformation
            };
        }


        #endregion

        #region Edit Lesson

        /// <summary>
        /// Returns a view model of a lesson ready for edit in a view.
        /// </summary>
        /// <param name="lessonId">Db id of a lesson</param>
        /// <returns></returns>
        public EditLessonViewModel GetEditLessonViewModel(int lessonId)
        {
            Lesson lesson = _IgorDataService.GetLesson(lessonId);
            EditLessonViewModel model = new EditLessonViewModel();
            model.LessonId = lessonId;
            model.Domain = lesson?.Domain ?? DomainTypes.Unknown;
            model.Topic = lesson?.Topic ?? "";
            model.AdditionalInformation = lesson?.AdditionalInformation ?? "";
            model.Comment = lesson?.Comment ?? "";
            model.TeacherName = lesson?.Teacher?.Name ?? "";
            model.TeacherId = lesson?.Teacher?.CharacterId ?? 0;
            model.EndTime = lesson?.EndTime ?? DateTime.MinValue;
            model.Dates = GetLarpWeekDays();
            DateTime start = DateTime.MinValue;
            DateTime.TryParse(model.Dates.FirstOrDefault()?.Value, out start);
            model.StartTime = lesson?.StartTime ?? start;
            model.Language = lesson?.Language ?? LanguageTypes.English;
            model.Day = lesson?.StartTime.Date ?? DateTime.Now.Date;

            if (model.IsEdited)
            {
                model.Duration = Convert.ToInt16(model.EndTime.Subtract(model.StartTime).TotalMinutes);
            }
            return model;
        }
        /// <summary>
        /// Assigns view model's properties to a target class and saves it in db.
        /// </summary>
        /// <param name="model">View model of a lesson.</param>
        /// <returns></returns>
        public bool SaveLesson(EditLessonViewModel model)
        {
            if (model == null) return false;
            Lesson lesson = model.IsEdited ? _IgorDataService.GetLesson(model.LessonId) : new Lesson();
            if (lesson == null) return false;
            lesson.Topic = model.Topic;
            lesson.AdditionalInformation = model.AdditionalInformation;
            lesson.Comment = model.Comment;
            lesson.Domain = model.Domain;
            lesson.Teacher = _IgorDataService.GetCharacterByName(model.TeacherName);
            lesson.StartTime = model.Day.AddHours(model.Start.Hour).AddMinutes(model.Start.Minute);
            lesson.EndTime = lesson.StartTime.AddMinutes(model.Duration);
            lesson.Edition = _IgorDataService.GetCurrentEdition();
            lesson.Language = model.Language;

            return _IgorDataService.UpdateLesson(lesson);
        }

        /// <summary>
        /// Determines whether dates overlap.
        /// </summary>
        /// <param name="startA"></param>
        /// <param name="startB"></param>
        /// <param name="endA"></param>
        /// <param name="endB"></param>
        /// <returns></returns>
        protected bool IsOverlapping(DateTime startA, DateTime startB, DateTime endA, DateTime endB)
        {
            if (startA < startB)
            {
                return endA > startB;
            }
            return endB > startA;
        }
        /// <summary>
        /// Gets a list of lessons that overlap with a specified lesson.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Lesson> GetOverlappingLessons(EditLessonViewModel model)
        {
            if (model == null) return new List<Lesson>();
            DateTime start = model.Day.AddHours(model.Start.Hour).AddMinutes(model.Start.Minute);
            DateTime end = start.AddMinutes(model.Duration);
            List<Lesson> lessons = _IgorDataService.GetLessonsForCurrentEdition();
            return lessons.Where(w => w != null && w.LessonId != model.LessonId && IsOverlapping(w.StartTime, start, w.EndTime, end))?.ToList() ?? new List<Lesson>();
        }

        /// <summary>
        /// Returns a list of week days in duration of a larp.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetLarpWeekDays()
        {
            return _IgorDataService.GetDatesForCurrentEdition()?.Select(s => new SelectListItem() { Text = s.DayOfWeek.ToString(), Value = s.ToShortDateString() })?.ToList() ?? new List<SelectListItem>();
        }

        #endregion

        #region Attendance

        /// <summary>
        /// Get full lesson attendees view model.
        /// </summary>
        /// <param name="lessonId">Db id of a lesson</param>
        /// <returns></returns>
        public LessonAttendeesViewModel GetLessonAttendeesViewModel(int lessonId)
        {
            Lesson lesson = _IgorDataService.GetLesson(lessonId);
            if (lesson == null) return null;
            LessonAttendeesViewModel result = new LessonAttendeesViewModel()
            {
                LessonId = lessonId,
                LessonTopic = lesson.Topic,
                LessonModel = GetSchoolLessonViewModel(lesson, null)
            };
            result.Attendees = lesson.Progresses.Select(s => new LessonAttendeeViewModel()
            {
                Name = s.Character.Name,
                Faction = s.Character.Faction?.Name ?? "",
                StudentId = s.Character.CharacterId,
                Modifier = s.Modifier,
                ProgressId = s.LearningProgressId,
                ApprovalDate = s.TimeStamp,
                IsActiveAttendance = IgorSettings.IsDebugMode ? true : DateTime.Now > lesson.StartTime && DateTime.Now < lesson.StartTime.AddHours(24),
            })?.ToList();
            result.Attendees = result.Attendees.OrderByDescending(o => o.ApprovalDate).ToList();
            return result;
        }
        /// <summary>
        /// Updates value of progress modifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public bool UpdateProgress(int id, decimal modifier)
        {
            try
            {
                if (modifier > 0)
                {
                    LearningProgress progress = _IgorDataService.GetProgress(id);
                    if (progress != null)
                    {
                        progress.Modifier = modifier;
                         _IgorDataService.UpdateLearningProgress(progress);
                         return _IgorDataService.Save();
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// Determines whether character attends lesson.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public bool IsAttendee(int characterId, int lessonId)
        {
            Character character = _IgorDataService.GetCharacter(characterId);
            if (character == null) return false;
            return character.AttendedLessons.Any(a => a.LessonId == lessonId);
        }
        /// <summary>
        /// Determines whether character teaches lesson.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public bool IsTeacher(int characterId, int lessonId)
        {
            Lesson lesson = _IgorDataService.GetLesson(lessonId);
            if (lesson == null) return false;
            return lesson.Teacher.CharacterId == characterId;
        }
        /// <summary>
        /// Adds a new lesson attendee.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddLessonAttendee(AddAttendeeViewModel model)
        {
            Character character = _IgorDataService.GetCharacterByName(model.Name);
            if (character != null)
            {
                Lesson lesson = _IgorDataService.GetLesson(model.LessonId);
                if (lesson != null)
                {
                    string userId = _userService.GetCurrentUser().Id;
                    LearningProgress progress = new LearningProgress()
                    {
                        Modifier = model.Modifier,
                        Character = character,
                        Domain = lesson.Domain,
                        Type = LearningTypeTypes.School,
                        Lesson = lesson,
                        ApprovedBy = _IgorDataService.GetIgorUser(userId),
                        Edition = _IgorDataService.GetCurrentEdition(),
                        ActiveLearningPoints = model.Modifier
                    };
                    return _IgorDataService.AddLearningProgress(progress);
                }
            }
            return false;
        }
        #endregion

        #endregion

        #region CharacterProgressManagement

        public ManageCharacterProgressViewModel GetManageCharacterProgressViewModel(int characterId)
        {
            ManageCharacterProgressViewModel result = new ManageCharacterProgressViewModel();
            Character character = _IgorDataService.GetCharacter(characterId);
            if (character != null)
            {
                result.CharacterId = characterId;
                result.CharacterName = character.Name;
                result.CharacterFaction = character.Faction?.Name ?? "";
                result.Specializations = character.GetLearningProgressesByType(LearningTypeTypes.Specialization).Select(s => GetSpecializationViewModel(s))?.ToList();
                result.KnownItemSchemas = GetCharactersItemSchemas(character);
                AvailableKnowledgePoints learningPoints = character?.GetAvailableKnowledgePoints();
                result.AvailableItemSchemas = GetAllKnownItemSchemas(AttainabilityTypes.Basic, learningPoints).Where(w => w.IsAvailable).ToList();
                List<DomainTypes> domainTypes = _IgorDataService.GetDomainTypes();
                result.KnowledgeDomainProgresses = domainTypes.Select(s => GetKnowledgeDomainViewMode(s, character))?.ToList();
            }
            return result;
        }
        /// <summary>
        /// Returns a list of all known specializatons.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSpecializations()
        {
            return _IgorDataService.GetAllSpecializations()?.Select(s => new SelectListItem() { Text = s.Name, Value = s.SpecializationId.ToString() })?.ToList() ?? new List<SelectListItem>();
        }
        /// <summary>
        /// Returns a list of all known specializatons except the ones owned by a character.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSpecializations(int characterId)
        {
            Character character = _IgorDataService.GetCharacter(characterId);
            if (character != null)
                return _IgorDataService.GetAllSpecializations()?.Where(w => !character.Specializations.Select(s => s.SpecializationId).Contains(w.SpecializationId)).Select(s => new SelectListItem() { Text = s.Name, Value = s.SpecializationId.ToString() })?.ToList() ?? new List<SelectListItem>();
            return _IgorDataService.GetAllSpecializations()?.Select(s => new SelectListItem() { Text = s.Name, Value = s.SpecializationId.ToString() })?.ToList() ?? new List<SelectListItem>();
        }
        /// <summary>
        /// Returns a list of all known schemas.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSchemas()
        {
            return _IgorDataService.GetAllItemSchemas()?.Select(s => new SelectListItem() { Text = s.GetSchemaName(), Value = s.ItemSchemaId.ToString() })?.ToList() ?? new List<SelectListItem>();
        }
        /// <summary>
        /// Returns a list of all known schemas except the ones the character already knows.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSchemas(int characterId)
        {
            Character character = _IgorDataService.GetCharacter(characterId);
            if (character != null)
                return _IgorDataService.GetAllItemSchemas()?.Where(w => !character.KnownSchemas.Select(s => s.ItemSchemaId).Contains(w.ItemSchemaId))?.Select(s => new SelectListItem() { Text = s.GetSchemaName(), Value = s.ItemSchemaId.ToString() })?.ToList() ?? new List<SelectListItem>();
            return _IgorDataService.GetAllItemSchemas()?.Select(s => new SelectListItem() { Text = s.GetSchemaName(), Value = s.ItemSchemaId.ToString() })?.ToList() ?? new List<SelectListItem>();
        }


        #region Add progress

        #region Books

        /// <summary>
        /// Returns a view model for adding book progress.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public AddBookViewModel GetAddBookViewModel(int characterId)
        {
            return new AddBookViewModel()
            {
                CharacterId = characterId,
                Modifier = 1,
                Domain = DomainTypes.Biology,
                LearningPoints = 0,
                Domains = GetDomains(),
                UserId = _userService.GetCurrentUserId()
            };
        }

        #endregion

        #region Merit

        /// <summary>
        /// Returns a view model for adding merit progress.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public AddMeritViewModel GetAddMeritViewModel(int characterId)
        {
            return new AddMeritViewModel()
            {
                CharacterId = characterId,
                Modifier = 1,
                Domain = DomainTypes.Biology,
                LearningPoints = 0,
                Domains = GetDomains(),
                UserId = _userService.GetCurrentUserId()
            };
        }
        /// <summary>
        /// Adds new learning progress to database for books and merits.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool AddProgress(AddProgressViewModelBase model, LearningTypeTypes type)
        {
            if (model == null) return false;
            Character character = _IgorDataService.GetCharacter(model.CharacterId);
            if (character == null) return false;

            LearningProgress progress = new LearningProgress()
            {
                Modifier = model.Modifier,
                Domain = model.Domain,
                Character = character,
                ActiveLearningPoints = model.LearningPoints,
                ApprovedBy = _IgorDataService.GetIgorUser(model.UserId),
                Comment = model.Comment,
                Description = model.Description,
                Edition = _IgorDataService.GetCurrentEdition(),
                Type = type,
            };
            return _IgorDataService.AddLearningProgress(progress);
        }

        #endregion

        #region Schema

        /// <summary>
        /// Returns add schema view model.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public AddSchemaViewModel GetAddSchemaViewModel(int characterId)
        {
            return new AddSchemaViewModel()
            {
                CharacterId = characterId,
                Modifier = 1,
                Domain = DomainTypes.Biology,
                LearningPoints = 0,
                Domains = GetDomains(),
                Schemas = GetSchemas(characterId),
                UserId = _userService.GetCurrentUserId()
            };
        }
        /// <summary>
        /// Saves schema progress.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSchema(AddSchemaViewModel model)
        {
            if (model == null) return false;
            Character character = _IgorDataService.GetCharacter(model.CharacterId);
            if (character == null) return false;
            ItemSchema schema = _IgorDataService.GetItemSchema(model.Schema);

            LearningProgress progress = new LearningProgress()
            {
                Modifier = model.Modifier,
                Domain = model.Domain,
                Character = character,
                ActiveLearningPoints = model.LearningPoints,
                ApprovedBy = _IgorDataService.GetIgorUser(model.UserId),
                Comment = model.Comment,
                Description = model.Description,
                Edition = _IgorDataService.GetCurrentEdition(),
                Type = LearningTypeTypes.Mentoring,
                Schema = schema
            };
            if (schema != null)
            {
                character.KnownSchemas.Add(schema);
                if (!_IgorDataService.UpdateCharacter(character)) ;
            }
            return _IgorDataService.AddLearningProgress(progress);
        }

        #endregion

        #region Private lesson

        /// <summary>
        /// Returns add private lesson view model.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public AddPrivateLessonViewModel GetAddPrivateLessonViewModel(int characterId)
        {
            return new AddPrivateLessonViewModel()
            {
                CharacterId = characterId,
                Modifier = 1,
                Domain = DomainTypes.Biology,
                LearningPoints = 0,
                Domains = GetDomains(),
                Schemas = GetSchemas(),
                UserId = _userService.GetCurrentUserId()
            };
        }
        /// <summary>
        /// Adds private lesson progress to db.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddPrivateLesson(AddPrivateLessonViewModel model)
        {
            if (model == null) return false;
            Character teacher = _IgorDataService.GetCharacterByName(model.TeacherName);
            Character student = _IgorDataService.GetCharacterByName(model.StudentName);
            if (student == null || teacher == null) return false;
            ItemSchema schema = null;
            if (model.IsSchemaLearned)
            {
                schema = _IgorDataService.GetItemSchema(model.Schema);
            }
            LearningProgress studentProgress = new LearningProgress()
            {
                Modifier = model.Modifier,
                Domain = model.Domain,
                Character = student,
                ActiveLearningPoints = model.LearningPoints,
                ApprovedBy = _IgorDataService.GetIgorUser(model.UserId),
                Comment = model.Comment,
                Description = model.Description,
                Edition = _IgorDataService.GetCurrentEdition(),
                Type = LearningTypeTypes.Mentoring,
                Schema = schema
            };
            LearningProgress teacherProgress = new LearningProgress()
            {
                Modifier = 1,
                Domain = model.Domain,
                Character = teacher,
                ActiveLearningPoints = IgorProgressSettings.TeachingPrivateLearningPoints,
                ApprovedBy = _IgorDataService.GetIgorUser(model.UserId),
                Comment = model.Comment,
                Description = model.Description,
                Edition = _IgorDataService.GetCurrentEdition(),
                Type = LearningTypeTypes.Teaching,
                Schema = schema
            };
            if (schema != null)
            {
                student.KnownSchemas.Add(schema);
                if (!_IgorDataService.UpdateCharacter(student)) ;
            }
            bool isSuccessfulStudent = _IgorDataService.AddLearningProgress(studentProgress);
            bool isSuccessfulTeacher = _IgorDataService.AddLearningProgress(teacherProgress);

            return isSuccessfulTeacher && isSuccessfulStudent;
        }

        #endregion

        #region Specialization

        /// <summary>
        /// Returns add specialization view model
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public AddSpecializationViewModel GetAddSpecializationViewModel(int characterId)
        {
            return new AddSpecializationViewModel()
            {
                CharacterId = characterId,
                Modifier = 1,
                Domain = DomainTypes.Biology,
                IsNewSpecialization = false,
                LearningPoints = 0,
                Domains = GetDomains(),
                Specializations = GetSpecializations(characterId),
                UserId = _userService.GetCurrentUserId()
            };
        }
        /// <summary>
        /// Adds specialization progress to db.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSpecialization(AddSpecializationViewModel model)
        {
            if (model == null) return false;
            Character character = _IgorDataService.GetCharacter(model.CharacterId);
            if (character == null) return false;
            Specialization specialization = null;
            if (!model.IsNewSpecialization && model.SpecializationId > 0)
            {
                specialization = _IgorDataService.GetSpecialization(model.SpecializationId);
            }
            else
            {
                specialization = new Specialization()
                {
                    Name = model.NewSpecializationName,
                    IsHidden = model.IsHidden
                };
                if (!_IgorDataService.AddSpecialization(specialization))
                    return false;
            }

            if (specialization == null)
                return false;

            LearningProgress progress = new LearningProgress()
            {
                Modifier = model.Modifier,
                Domain = model.Domain,
                Character = character,
                ActiveLearningPoints = model.LearningPoints,
                ApprovedBy = _IgorDataService.GetIgorUser(model.UserId),
                Comment = model.Comment,
                Description = model.Description,
                Edition = _IgorDataService.GetCurrentEdition(),
                Type = LearningTypeTypes.Specialization,
                Specialization = specialization
            };
            return _IgorDataService.AddLearningProgress(progress);
        }

        #endregion

        #endregion

        /// <summary>
        /// Gets a list of all domain types used in views.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetDomains()
        {
            List<DomainTypes> domains = new List<DomainTypes>() { DomainTypes.Biology, DomainTypes.Chemistry, DomainTypes.Medicine, DomainTypes.Technology };
            return domains.Select(s => new SelectListItem() { Text = s.GetDomainNameString(), Value = s.GetDomainNameString() }).ToList();
        }

        /// <summary>
        /// Deletes a learning progress from db.
        /// </summary>
        /// <param name="progressId"></param>
        /// <returns></returns>
        public bool DeleteProgress(int progressId)
        {
            return _IgorDataService.DeleteLearningProgress(progressId);
        }

        public bool DeleteSchemaForCharacter(int schemaId, int characterId)
        {
            Character character = _IgorDataService.GetCharacter(characterId);
            ItemSchema schema = _IgorDataService.GetItemSchema(schemaId);
            if (character == null || schema == null) return false;
            character.KnownSchemas.Remove(schema);
            return _IgorDataService.UpdateCharacter(character);
        }

        #endregion

        #region CharacterProgress

        /// <summary>
        /// Returns a view model filled with progress information.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CharacterProgressViewModel GetCharacterProgressViewModel(string userId)
        {
            CharacterProgressViewModel result = new CharacterProgressViewModel();
            Character character = _IgorDataService.GetActiveAccreditationByUser(userId)?.ActiveCharacter;
            if (character != null)
            {
                result.CharacterId = character.CharacterId;
                List<DomainTypes> domainTypes = _IgorDataService.GetDomainTypes();
                result.KnowledgeDomainProgresses.AddRange(domainTypes.Select(s => GetKnowledgeDomainViewMode(s, character)));
                result.Specializations = GetSpecializationsViewModel(character);
            }
            return result;
        }
        /// <summary>
        /// Returns a list of specialization view models for a character.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        protected List<SpecializationViewModel> GetSpecializationsViewModel(Character character)
        {
            if (character == null) return null;
            return character.GetLearningProgressesByType(LearningTypeTypes.Specialization).Select(s => GetSpecializationViewModel(s))?.ToList();
        }
        /// <summary>
        /// Returns a specialization view model filled with data.
        /// </summary>
        /// <param name="specialization"></param>
        /// <returns></returns>
        protected SpecializationViewModel GetSpecializationViewModel(LearningProgress specialization)
        {
            if (specialization == null) return null;
            return new SpecializationViewModel(specialization);
        }
        /// <summary>
        /// Deturns a view model of knowledge domain filled with data.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        protected KnowledgeDomainViewModel GetKnowledgeDomainViewMode(DomainTypes domain, Character character)
        {
            KnowledgeDomainViewModel result = new KnowledgeDomainViewModel()
            {
                Domain = domain,
            };
            List<LearningTypeTypes> learningTypes = _IgorDataService.GetLearningProgressTypes();
            result.LearningTypesDetails.AddRange(learningTypes.Select(s => GetLearningTypeViewModel(domain, character, s)));
            result.Level = CalculateKnowledgeLevel(result.LearningTypesDetails);
            return result;
        }
        /// <summary>
        /// Returns a view model foe a selected learning domain and type.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="character"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected LearningTypeViewModel GetLearningTypeViewModel(DomainTypes domain, Character character, LearningTypeTypes type)
        {
            LearningTypeViewModel result = new LearningTypeViewModel()
            {
                Type = type
            };
            List<LearningProgress> progresses = character.GetLearningProgressesByType(type)?.Where(w => w.Domain == domain)?.ToList() ?? new List<LearningProgress>();
            result.TotalCount = progresses.Sum(s => s.Modifier);
            result.ActiveCount = progresses.Where(w => w.HasActiveLearningPoints).Sum(s => s.ActiveLearningPoints);
            return result;
        }
        /// <summary>
        /// Calculates a total knowledge level based on modifiers and max values.
        /// </summary>
        /// <param name="types">A list of LearningTypeViewModel objects</param>
        /// <returns></returns>
        protected decimal CalculateKnowledgeLevel(List<LearningTypeViewModel> types)
        {
            decimal result = 0;
            foreach (LearningTypeViewModel type in types)
            {
                result += CalculateKnowledgeLevelForLearningType(type);
            }
            return result;
        }
        /// <summary>
        /// Returns a calculated total progress value for a learning type.
        /// </summary>
        /// <param name="learningType"></param>
        /// <returns></returns>
        protected decimal CalculateKnowledgeLevelForLearningType(LearningTypeViewModel learningType)
        {
            return Math.Min(learningType.TotalCount * learningType.Type.GetProgressModifier(), learningType.Type.GetMaxProgressValue());
        }
        #endregion

        #region ItemSchemas

        /// <summary>
        /// Get view models filled with item schemas data.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ItemSchemasViewModel GetItemSchemasViewModel(string userId)
        {
            ItemSchemasViewModel result = new ItemSchemasViewModel()
            {
                UserId = userId
            };
            Character activeCharacter = _IgorDataService.GetActiveAccreditationByUser(userId)?.ActiveCharacter;
            if (activeCharacter != null)
            {
                result.ItemSchemas.AddRange(GetCharactersItemSchemas(activeCharacter));
            }
            AvailableKnowledgePoints learningPoints = activeCharacter?.GetAvailableKnowledgePoints();
            result.ItemSchemas.AddRange(GetAllKnownItemSchemas(AttainabilityTypes.Basic, learningPoints));
            result.ItemSchemas.AddRange(GetAllKnownItemSchemas(AttainabilityTypes.Common, null));
            return result;
        }
        /// <summary>
        /// Get all known item schemas by types.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected List<ItemSchemaViewModel> GetAllKnownItemSchemas(AttainabilityTypes type, AvailableKnowledgePoints points)
        {
            List<ItemSchemaViewModel> result = new List<ItemSchemaViewModel>();
            return _IgorDataService.GetItemSchemasByAttainability(type).Select(s => GetItemSchemaViewModel(s, false, null, points)).ToList();
        }
        /// <summary>
        /// Get all schemas of a character with summed progress modifiers.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        protected List<ItemSchemaViewModel> GetCharactersItemSchemas(Character character)
        {
            if (character == null) return null;
            List<ItemSchemaViewModel> result = new List<ItemSchemaViewModel>();
            List<LearningProgress> schemaProgresses = character.GetLearningProgressesByType(LearningTypeTypes.Crafting).ToList();
            //schemaProgresses.AddRange(character.GetLearningProgressesByType(LearningTypeTypes.Specialization).ToList());
            List<ItemSchema> usedSchemas = character.ArtifactsCrafted.DistinctBy(d => d.ItemSchemaId).ToList();
            foreach (ItemSchema schema in usedSchemas)
            {
                ItemSchemaViewModel schemaViewModel = GetItemSchemaViewModel(schema, true, schemaProgresses);
                if (schemaViewModel != null)
                    result.Add(schemaViewModel);
            }

            foreach (ItemSchema schema in character.KnownSchemas.Where(w => !usedSchemas.Select(s => s.ItemSchemaId).Contains(w.ItemSchemaId)))
            {
                ItemSchemaViewModel schemaViewModel = GetItemSchemaViewModel(schema, true, null);
                if (schemaViewModel != null)
                    result.Add(schemaViewModel);
            }
            return result;
        }
        /// <summary>
        /// Returns item schema view model filled with data.
        /// </summary>
        /// <param name="schemaId"></param>
        /// <param name="IsKnown"></param>
        /// <returns></returns>
        public ItemSchemaViewModel GetItemSchemaViewModel(int schemaId, bool IsKnown)
        {
            ItemSchema schema = _IgorDataService.GetItemSchema(schemaId);
            return GetItemSchemaViewModel(schema, IsKnown);
        }

        /// <summary>
        /// Gets Item schema view model filled with data.
        /// </summary>
        /// <param name="schema">Item schema instance.</param>
        /// <param name="IsKnown">Determines whether the schema is known by a character.</param>
        /// <param name="schemaProgresses">List of schema progresses of a character needed for calculating progress.</param>
        /// <returns></returns>
        protected ItemSchemaViewModel GetItemSchemaViewModel(ItemSchema schema, bool IsKnown, List<LearningProgress> schemaProgresses = null, AvailableKnowledgePoints points = null, List<Specialization> specializations = null)
        {
            if (schema == null) return new ItemSchemaViewModel();
            if (specializations.IsNullOrEmpty())
                specializations = new List<Specialization>();
            ItemSchemaViewModel result = new ItemSchemaViewModel()
            {
                Name = schema.Name,
                SchemaType = schema.SchemaType,
                Description = schema.Description.IsNullOrEmpty()
                    ? schema.OutputItems?.FirstOrDefault()?.Description
                    : "",
                Mechanics = schema.OutputItems?.FirstOrDefault()?.Mechanics ?? "",
                Attainability = schema.Attainability,
                IsKnown = IsKnown,
                SchemaId = schema.ItemSchemaId,
                Domains = schema.LearnConditions?.SelectMany(s => s.GetRelatedDomains())?.Distinct()?.ToList() ??
                          new List<DomainTypes>(),
                Progress = schemaProgresses?.Where(w => w.Type == LearningTypeTypes.Crafting && w.Schema != null && w.Schema.ItemSchemaId == schema.ItemSchemaId)
                    ?.Sum(s => s.Modifier) ?? 0,
                IsAvailable = schema.LearnConditions.Any(a => IsLearningConditionFulfilled(a, points, specializations)),
                InputItems = schema.InputItems.Select(s => new ItemViewModel(s)).ToList(),
                OutputItems = schema.OutputItems.Select(s => new ItemViewModel(s)).ToList(),
            };
            result.Difficulty = DifficultyLevels.Unknown;
            return result;
        }

        #endregion

        #region Teach schema

        /// <summary>
        /// Determines whether character collected enough learning points to satisfy conditions.
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="points"></param>
        /// <param name="specializations"></param>
        /// <returns></returns>
        protected bool IsLearningConditionFulfilled(Condition conditions, AvailableKnowledgePoints points, List<Specialization> specializations)
        {
            if (conditions == null || points == null) return false;
            return (!conditions.DomainConditions.Any(a => (points?.DomainPoints?
                       .Where(w => w.Domain == a.Type)?
                       .Sum(s => s.Value) ?? 0) < a.Points)) 
                   && (conditions.AllowMixing ? points.Total >= conditions.Total :
                conditions.Total <= points.DomainPoints.
                    Where(w => conditions.GetRelatedDomains().Contains(w.Domain))
                    .Sum(s => s.Value))
                && (conditions.Specialization == null || (specializations?.Select(s => s.SpecializationId)?
                    .Contains(conditions.Specialization.SpecializationId) ?? false));
        }
        /// <summary>
        /// Exchanges learning points and adds schema to character's known schemas.
        /// </summary>
        /// <param name="schemaId"></param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public bool LearnSchema(int schemaId, int characterId)
        {
            Character character = _IgorDataService.GetCharacter(characterId);
            ItemSchema schema = _IgorDataService.GetItemSchema(schemaId);
            if (character == null | schema == null) return false;
            AvailableKnowledgePoints points = character?.GetAvailableKnowledgePoints();
            ItemSchemaLearningCondition condition = schema.LearnConditions.FirstOrDefault(f => IsLearningConditionFulfilled(f, points, character.Specializations));
            if (condition == null) return false;
            if (!ExchangeLearningPoints(condition, character))
                return false;
            return AddKnownSchemaToCharacter(schema, character);
        }

        public bool AddKnownSchemaToCharacter(ItemSchema schema, Character character)
        {
            if (character == null | schema == null) return false;
            character.KnownSchemas.Add(schema);
            return _IgorDataService.UpdateCharacter(character);
        }
        /// <summary>
        /// Reduces the number of learning points according to the conditions..
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        protected bool ExchangeLearningPoints(Condition conditions, Character character) {
            if (conditions == null || character == null) return false;
            if (!IsLearningConditionFulfilled(conditions, character?.GetAvailableKnowledgePoints(), character.Specializations)) return false;
            List<LearningProgress> result = new List<LearningProgress>();

            List<DomainTypes> domains = conditions.AllowMixing ? _IgorDataService.GetDomainTypes() : conditions.GetRelatedDomains();
            foreach (DomainTypes domain in conditions.GetRelatedDomains()) {
                decimal domainPointsLeft =
                    conditions.DomainConditions.FirstOrDefault(w => w.Type == domain)?.Points ?? 0;
                while (domainPointsLeft > 0) {
                    LearningProgress progress = character.GetLearningProgressesByDomain(domain, true).FirstOrDefault();
                    if (progress == null) {
                        return false;
                    }
                    if (progress.ActiveLearningPoints >= domainPointsLeft) {
                        progress.ActiveLearningPoints -= domainPointsLeft;
                        domainPointsLeft = 0;
                    }
                    else {
                        domainPointsLeft -= progress.ActiveLearningPoints;
                        progress.ActiveLearningPoints = 0;
                    }
                    result.Add(progress);
                }
            }
            decimal pointsLeft = conditions.Total - conditions.DomainConditions.Sum(s => s.Points);
            while (pointsLeft > 0) {
                LearningProgress progress = character.LearningProgresses.FirstOrDefault(w => domains.Contains(w.Domain) && w.ActiveLearningPoints > 0);
                if (progress == null) {
                    return false;
                }
                if (progress.ActiveLearningPoints >= pointsLeft) {
                    progress.ActiveLearningPoints -= pointsLeft;
                    pointsLeft = 0;
                }
                else {
                    pointsLeft -= progress.ActiveLearningPoints;
                    progress.ActiveLearningPoints = 0;
                }
                result.Add(progress);
            }
            if (pointsLeft > 0) return false;
            List<bool> progResults = new List<bool>();
            foreach (LearningProgress item in result.Distinct())
            {
                progResults.Add(_IgorDataService.UpdateLearningProgress(item));
                _IgorDataService.Save();
            }
            return progResults.All(a => a);
        }


        #endregion

        #region Character

        /// <summary>
        /// Returns character view model based on character name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CharacterBasicInfoViewModel FindCharacter(string name)
        {
            Character character = _IgorDataService.GetCharacterByName(name);
            return GetCharacterBasicInfoViewModel(character);
        }
        /// <summary>
        /// Returns character view model based on character id.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public CharacterBasicInfoViewModel FindCharacter(int characterId)
        {
            Character character = _IgorDataService.GetCharacter(characterId);
            return GetCharacterBasicInfoViewModel(character);
        }
        /// <summary>
        /// Returns character view model filled tirh data.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        protected CharacterBasicInfoViewModel GetCharacterBasicInfoViewModel(Character character)
        {
            if (character != null)
            {
                return new CharacterBasicInfoViewModel()
                {
                    Id = character.CharacterId,
                    Faction = character.Faction?.Name ?? "",
                    Name = character.Name,
                };
            }
            return new CharacterBasicInfoViewModel();
        }
        /// <summary>
        /// Determines whether a character knows a schema.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="schemaId"></param>
        /// <returns></returns>
        public bool KnowsSchema(int characterId, int schemaId)
        {
            Character character = _IgorDataService.GetCharacter(characterId);
            if (character == null) return false;
            return character.KnowsSchema(schemaId);
        }
        public bool CanTeachSchema(int characterId, int schemaId)
        {
            Character character = _IgorDataService.GetCharacter(characterId);
            if (character == null || schemaId < 1) return false;
            return character.CanTeachSchema(schemaId);
        }

        #endregion


        #endregion

    }
}