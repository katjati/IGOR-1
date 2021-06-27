using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Igor.Core.ControllerServices;
using Igor.Core.Models.Terminal;
using Igor.Library;
using Igor.Library.Abstract;
using Igor.Library.Global;
using Igor.Library.Models;
using Igor.Library.Services;

namespace Igor.Core.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class TerminalController : IgorController
    {
        #region Properties

        /// <summary>
        /// Instance of unit of work.
        /// </summary>
        private readonly ILarpUnitOfWork _uow;
        /// <summary>
        /// Terminal controller service instance.
        /// </summary>

        private readonly TerminalControllerService _terminalControllerService;
        #endregion

        #region Constructors

        public TerminalController(ILarpUnitOfWork uow, IUserService uService) : base(uService)
        {
            _uow = uow;
            //IUserService userService = new UserService(new HttpContextWrapper(System.Web.HttpContext.Current))
            _terminalControllerService = new TerminalControllerService(new IgorDataService(uow), uService);
        }

        #endregion

        #region Methods

        /// <summary>
        /// All or limited productions view.
        /// If limit set to true, only last x productions are shown.
        /// </summary>
        /// <param name = "limit" ></ param >
        /// < returns ></ returns >
        public async Task<ActionResult> Index()
        {
            SchoolScheduleViewModel model = _terminalControllerService.GetSchoolScheduleViewModel();
            return View("_SchoolSchedule", model);
        }

        #region Lessons
        /// <summary>
        /// Returns a view of a school schedule of a current edition of an event.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SchoolSchedule()
        {
            SchoolScheduleViewModel model = _terminalControllerService.GetSchoolScheduleViewModel();
            return PartialView("_SchoolSchedule", model);
        }
        /// <summary>
        /// Returns a view that contains a list of lesson's attendees.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public ActionResult LessonAttendees(int lessonId)
        {
            LessonAttendeesViewModel model = _terminalControllerService.GetLessonAttendeesViewModel(lessonId);
            return PartialView("_LessonAttendees", model);
        }
        /// <summary>
        /// Hides a lesson from users and archives it it database without affecting progresses related to the lesson.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public ActionResult DeleteLesson(int lessonId)
        {
            IgorDataService dataService = new IgorDataService(_uow);
            bool result = dataService.ArchiveLesson(lessonId);
            return RedirectToAction("SchoolSchedule", "Terminal");
        }
        /// <summary>
        /// Returns edit lesson view.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public PartialViewResult EditLesson(int lessonId = 0)
        {
            EditLessonViewModel model = _terminalControllerService.GetEditLessonViewModel(lessonId);
            return PartialView("_EditLesson", model);
        }
        /// <summary>
        /// Validates and saves a lesson.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveLesson(EditLessonViewModel model)
        {
            CharacterBasicInfoViewModel character = _terminalControllerService.FindCharacter(model.TeacherName);
            ViewBag.TeacherName = character.Name;
            model.StartTime = model.Day.AddHours(model.Start.Hour).AddMinutes(model.Start.Minute);
            if (character.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("TeacherName", "Character with a given name not found.");
                model.TeacherName = character.Name;
                model.Dates = _terminalControllerService.GetLarpWeekDays();
                return PartialView("_EditLesson", model);
            }
            if (_terminalControllerService.IsAttendee(character.Id, model.LessonId))
            {
                ModelState.AddModelError("TeacherName", "Character with a given name attended this lesson as a student.");
                model.TeacherName = character.Name;
                model.Dates = _terminalControllerService.GetLarpWeekDays();
                return PartialView("_EditLesson", model);
            }
            Lesson overlappingLesson = _terminalControllerService.GetOverlappingLessons(model)?.FirstOrDefault();
            if (overlappingLesson != null)
            {
                ModelState.AddModelError("Start", $"Lesson overlaps with '{overlappingLesson.Topic}' between {overlappingLesson.StartTime.ToShortTimeString()} - {overlappingLesson.EndTime.ToShortTimeString()}.");
                model.TeacherName = character.Name;
                model.Dates = _terminalControllerService.GetLarpWeekDays();
                return PartialView("_EditLesson", model);
            }
            if (model.Duration < 20)
            {
                ModelState.AddModelError("Duration", $"Lesson cannot be shorter than 20 minutes.");
                model.TeacherName = character.Name;
                model.Dates = _terminalControllerService.GetLarpWeekDays();
                return PartialView("_EditLesson", model);
            }
            if (model.Duration > 240)
            {
                ModelState.AddModelError("Duration", $"Lesson cannot be longer than 4 hours.");
                model.TeacherName = character.Name;
                model.Dates = _terminalControllerService.GetLarpWeekDays();
                return PartialView("_EditLesson", model);
            }
            if (!_terminalControllerService.SaveLesson(model))
            {
                
            }
            return RedirectToAction("SchoolSchedule", "Terminal");
        }
        /// <summary>
        /// Updates the value of attendence modifier.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult UpdateProgressModifier(LessonAttendeeViewModel model)
        {
            _terminalControllerService.UpdateProgress(model.ProgressId, model.Modifier);
            model.IsActiveAttendance = true;
            return PartialView("EditorTemplates/_LessonAttendeeViewModel", model);
        }
        /// <summary>
        /// Returns add attendee view.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public ActionResult AddNewAttendee(int lessonId)
        {
            AddAttendeeViewModel model = new AddAttendeeViewModel() {LessonId = lessonId, Modifier = 1};
            return PartialView("_LessonAttendees_Add", model);
        }
        /// <summary>
        /// Validates and saves attendee.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ValidateAttendee(AddAttendeeViewModel model)
        {
            CharacterBasicInfoViewModel character = _terminalControllerService.FindCharacter(model.Name);
            if (character.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("Name", "Character with a given name not found.");
                return PartialView("_LessonAttendees_Add", model);
            }
            if (_terminalControllerService.IsAttendee(character.Id, model.LessonId))
            {
                ModelState.AddModelError("Name", "Character with a given name is already on the attendance list.");
                return PartialView("_LessonAttendees_Add", model);
            }
            if (_terminalControllerService.IsTeacher(character.Id, model.LessonId))
            {
                ModelState.AddModelError("Name", "Character with a given name is a teacher of this lesson.");
                return PartialView("_LessonAttendees_Add", model);
            }
            bool isSuccessful = _terminalControllerService.AddLessonAttendee(model);
            return RedirectToAction("LessonAttendees", "Terminal", new {lessonId = model.LessonId});
        }

        #endregion


        #region Manage character

        /// <summary>
        /// All or limited productions view.
        /// If limit set to true, only last x productions are shown.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public PartialViewResult ManageCharacter(int characterId)
        {
            ManageCharacterProgressViewModel model = _terminalControllerService.GetManageCharacterProgressViewModel(characterId);
            return PartialView("_ManageCharacterProgress", model);
        }
        public ActionResult FindCharacter()
        {
            return PartialView("_FindUser", new CharacterBasicInfoViewModel());
        }
        public ActionResult ValidateCharacter(CharacterBasicInfoViewModel model)
        {
            CharacterBasicInfoViewModel character = _terminalControllerService.FindCharacter(model.Name);
            if (character.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("Name", "Character with a given name not found.");
                return PartialView("_FindUser", model);
            }
            return RedirectToAction("ManageCharacter", new { characterId = character.Id });
        }

        /// <summary>
        /// Hides a lesson from users and archives it it database without affecting progresses related to the lesson.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        // TODO: archive not delete!
        public ActionResult DeleteProgress(int progressId, int lessonId)
        {
            IgorDataService dataService = new IgorDataService(_uow);
            bool result = dataService.DeleteLearningProgress(progressId);
            return RedirectToAction("LessonAttendees", "Terminal", new { lessonId = lessonId });
        }
        /// <summary>
        /// Deletes specialization from a character.
        /// </summary>
        /// <param name="progressId"></param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public ActionResult DeleteProgressForCharacter(int progressId, int characterId)
        {
            bool isSuccessful = _terminalControllerService.DeleteProgress(progressId);
            return RedirectToAction("ManageCharacter", new { characterId = characterId });
        }
        /// <summary>
        /// Deletes specialization from a character.
        /// </summary>
        /// <param name="progressId"></param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public ActionResult DeleteSchemaForCharacter(int schemaId, int characterId)
        {
            bool isSuccessful = _terminalControllerService.DeleteSchemaForCharacter(schemaId, characterId);
            return RedirectToAction("ManageCharacter", new { characterId = characterId });
        }
        /// <summary>
        /// Spend character's learning points and buy a new schema.
        /// </summary>
        /// <param name="schemaId"></param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public ActionResult ExchangeSchema(int schemaId, int characterId)
        {
            bool isSuccessful = _terminalControllerService.LearnSchema(schemaId, characterId);
            return RedirectToAction("ManageCharacter", new { characterId = characterId });
        }


        #region Add progress

        #region Merit

        /// <summary>
        /// Returns a new merit view.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public ActionResult AddMerit(int characterId)
        {
            AddMeritViewModel model = _terminalControllerService.GetAddMeritViewModel(characterId);
            return PartialView("_AddProgress_Merit", model);
        }
        /// <summary>
        /// Validates and saves merit progress.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveAddMerit(AddMeritViewModel model)
        {
            CharacterBasicInfoViewModel character = _terminalControllerService.FindCharacter(model.CharacterId);
            model.Domains = _terminalControllerService.GetDomains();

            if (model.Modifier <= 0)
            {
                ModelState.AddModelError("Modifier", "Modifier must be greater than 0.");
                return PartialView("_AddProgress_Merit", model);
            }
            if (model.LearningPoints < 0)
            {
                ModelState.AddModelError("LearningPoints", "Learning points value cannot be negative.");
                return PartialView("_AddProgress_Merit", model);
            }
            if (_terminalControllerService.AddProgress(model, LearningTypeTypes.Merit))
            {
                return RedirectToAction("ManageCharacter", new { characterId = model.CharacterId });
            }
            return PartialView("_AddProgress_Merit", model);
        }

        #endregion

        #region Book

        /// <summary>
        /// Returns new book view.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public ActionResult AddBook(int characterId)
        {
            AddBookViewModel model = _terminalControllerService.GetAddBookViewModel(characterId);
            return PartialView("_AddProgress_Book", model);
        }
        /// <summary>
        /// Validates and adds new book progress.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveAddBook(AddBookViewModel model)
        {
            CharacterBasicInfoViewModel character = _terminalControllerService.FindCharacter(model.CharacterId);
            model.Domains = _terminalControllerService.GetDomains();

            if (model.Modifier <= 0)
            {
                ModelState.AddModelError("Modifier", "Modifier must be greater than 0.");
                return PartialView("_AddProgress_Book", model);
            }
            if (model.LearningPoints < 0)
            {
                ModelState.AddModelError("LearningPoints", "Learning points value cannot be negative.");
                return PartialView("_AddProgress_Book", model);
            }
            if (_terminalControllerService.AddProgress(model, LearningTypeTypes.Library))
            {
                return RedirectToAction("ManageCharacter", new { characterId = model.CharacterId });
            }
            return PartialView("_AddProgress_Book", model);
        }

        #endregion

        #region Schema

        /// <summary>
        /// Returns add new schema view.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public ActionResult AddSchema(int characterId)
        {
            AddSchemaViewModel model = _terminalControllerService.GetAddSchemaViewModel(characterId);
            return PartialView("_AddProgress_Schema", model);
        }
        /// <summary>
        /// Validates and saves new schema progress.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveAddSchema(AddSchemaViewModel model)
        {
            CharacterBasicInfoViewModel character = _terminalControllerService.FindCharacter(model.CharacterId);
            model.Domains = _terminalControllerService.GetDomains();
            model.Schemas = _terminalControllerService.GetSchemas(model.CharacterId);

            if (model.Modifier <= 0)
            {
                ModelState.AddModelError("Modifier", "Modifier must be greater than 0.");
                return PartialView("_AddProgress_Schema", model);
            }
            if (model.LearningPoints < 0)
            {
                ModelState.AddModelError("LearningPoints", "Learning points value cannot be negative.");
                return PartialView("_AddProgress_Schema", model);
            }
            if (_terminalControllerService.AddSchema(model))
            {
                return RedirectToAction("ManageCharacter", new { characterId = model.CharacterId });
            }
            return PartialView("_AddProgress_Schema", model);
        }

        #endregion

        #region Private lesson

        /// <summary>
        /// Returns a private lesson view.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public ActionResult AddPrivateLesson(int characterId)
        {
            AddPrivateLessonViewModel model = _terminalControllerService.GetAddPrivateLessonViewModel(characterId);
            return PartialView("_AddProgress_PrivateLesson", model);
        }
        /// <summary>
        /// Validates and saves private lesson progress.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveAddPrivateLesson(AddPrivateLessonViewModel model)
        {
            CharacterBasicInfoViewModel student = _terminalControllerService.FindCharacter(model.StudentName);
            CharacterBasicInfoViewModel teacher = _terminalControllerService.FindCharacter(model.TeacherName);
            model.Domains = _terminalControllerService.GetDomains();
            model.Schemas = _terminalControllerService.GetSchemas();

            if (student.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("StudentName", "Character with a give name not found.");
                return PartialView("_AddProgress_PrivateLesson", model);
            }
            if (teacher.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("TeacherName", "Character with a give name not found.");
                return PartialView("_AddProgress_PrivateLesson", model);
            }
            if (teacher.Id == student.Id)
            {
                ModelState.AddModelError("TeacherName", "Teacher and student cannot be the same person.");
                return PartialView("_AddProgress_PrivateLesson", model);
            }
            // TODO: validate if teacher is skilled enough
            if (model.Modifier <= 0)
            {
                ModelState.AddModelError("Modifier", "Modifier must be greater than 0.");
                return PartialView("_AddProgress_PrivateLesson", model);
            }
            if (model.LearningPoints < 0)
            {
                ModelState.AddModelError("LearningPoints", "Learning points value cannot be negative.");
                return PartialView("_AddProgress_PrivateLesson", model);
            }
            if (model.IsSchemaLearned && _terminalControllerService.KnowsSchema(student.Id, model.Schema))
            {
                ModelState.AddModelError("Schema", "Student already knows this schema.");
                return PartialView("_AddProgress_PrivateLesson", model);
            }
            if (model.IsSchemaLearned && !_terminalControllerService.CanTeachSchema(student.Id, model.Schema))
            {
                ModelState.AddModelError("TeacherName", "Teacher is not proficient enough in using schema to teach it.");
                return PartialView("_AddProgress_PrivateLesson", model);
            }
            if (_terminalControllerService.AddPrivateLesson(model))
            {
                return RedirectToAction("ManageCharacter", new { characterId = model.CharacterId });
            }
            return PartialView("_AddProgress_PrivateLesson", model);
        }


        #endregion

        #region Specialization

        /// <summary>
        /// Returns add specialization view,
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public ActionResult AddSpecialization(int characterId)
        {
            AddSpecializationViewModel model = _terminalControllerService.GetAddSpecializationViewModel(characterId);
            return PartialView("_AddProgress_Specialization", model);
        }
        /// <summary>
        /// Validates and saves specialization.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveAddSpecialization(AddSpecializationViewModel model)
        {
            CharacterBasicInfoViewModel character = _terminalControllerService.FindCharacter(model.CharacterId);
            model.Domains = _terminalControllerService.GetDomains();
            model.Specializations = _terminalControllerService.GetSpecializations(model.CharacterId);
            if (model.IsNewSpecialization)
            {
                if (model.NewSpecializationName.IsNullOrEmpty())
                {
                    ModelState.AddModelError("NewSpecializationName", "Specialization name must be provided.");
                    return PartialView("_AddProgress_Specialization", model);
                }
                if (model.Specializations.Any(a => a.Text.ToLower() == model.NewSpecializationName.ToLower()))
                {
                    ModelState.AddModelError("NewSpecializationName", "Specialization with a given name already exists.");
                    return PartialView("_AddProgress_Specialization", model);
                }
                if (model.Modifier <= 0)
                {
                    ModelState.AddModelError("Modifier", "Modifier must be greater than 0.");
                    return PartialView("_AddProgress_Specialization", model);
                }
                if (model.LearningPoints < 0)
                {
                    ModelState.AddModelError("LearningPoints", "Learning points value cannot be negative.");
                    return PartialView("_AddProgress_Specialization", model);
                }
            }
            if (_terminalControllerService.AddSpecialization(model))
            {
                return RedirectToAction("ManageCharacter", new { characterId = character.Id });
            }
            return PartialView("_AddProgress_Specialization", model);
        }

        #endregion


        #endregion

        #endregion


        #region Character

        /// <summary>
        /// All or limited productions view.
        /// If limit set to true, only last x productions are shown.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public PartialViewResult CharacterProgress()
        {
            IgorUser user = _userService.GetCurrentUser();
            CharacterProgressViewModel model = _terminalControllerService.GetCharacterProgressViewModel(user.Id);
            return PartialView("_CharacterProgress", model);
        }
        

        #endregion

        #region Schemas

        public PartialViewResult ItemSchemas()
        {
            //if (!_userService.IsLogged())
            //return RedirectToAction("Login", "Account");
            IgorUser user = _userService.GetCurrentUser();
            ItemSchemasViewModel model = _terminalControllerService.GetItemSchemasViewModel(user?.Id);
            return PartialView("_ItemSchemas", model);
        }
        [HttpPost]
        public PartialViewResult ItemSchemaDetails(int schemaId, bool IsKnown)
        {
            ItemSchemaViewModel model = _terminalControllerService.GetItemSchemaViewModel(schemaId, IsKnown);
            return PartialView("_ItemSchema_Details", model);
        }

        #endregion

        
        public PartialViewResult Test()
        {
            IgorUser user = _userService.GetCurrentUser();
            ItemSchemasViewModel model = _terminalControllerService.GetItemSchemasViewModel(user?.Id);
            return PartialView("_ItemSchemas", model);
        }

        #endregion
    }
}