using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Igor.Library.Abstract;
using Igor.Library.Global;
using Igor.Library.Models;

namespace Igor.Core.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class IgorController : Controller
    {
        #region Properties

        /// <summary>
        /// Provider for online helps.
        /// </summary>
        //protected readonly IHelpProvider _helpProvider;
        /// <summary>
        /// User service instance.
        /// </summary>
        protected readonly IUserService _userService;
        /// <summary>
        /// Performance log for the controller.
        /// </summary>
        protected ILog _controllerLog;

        #endregion

        #region Constuctors

        public IgorController(IUserService uService)
        {
            _userService = uService;
        }

        #endregion

        #region Status messages

        ///// <summary>
        ///// Check request data if there is any status message set (e.g. before redirection).
        ///// </summary>
        ///// <returns></returns>
        //protected StatusMessage GetStatusMessage()
        //{
        //    object resultValue = TempData["StatusMessage"];
        //    //if (resultValue == null) resultValue = Session["StatusMessage"];
        //    StatusMessage result = resultValue as StatusMessage;
        //    //StatusMessage result = Session["StatusMessage"] as StatusMessage;
        //    //Session["StatusMessage"] = null;
        //    //TempData["StatusMessage"] = null;
        //    return result;
        //}
        ///// <summary>
        ///// Set status message in request data (useful to pass message between redirections).
        ///// </summary>
        ///// <returns></returns>
        //protected void SetStatusMessage(StatusMessage message)
        //{
        //    TempData["StatusMessage"] = message;
        //    //Session["StatusMessage"] = message;
        //}
        ///// <summary>
        ///// Set success message in request data.
        ///// </summary>
        ///// <param name="message"></param>
        //protected void SetSuccessMessage(string message)
        //{
        //    SetStatusMessage(new StatusMessage(message, StatusMessageTypes.Success));
        //}
        ///// <summary>
        ///// Set success message in request data.
        ///// </summary>
        ///// <param name="message"></param>
        //protected void SetErrorMessage(string message)
        //{
        //    SetStatusMessage(new StatusMessage(message, StatusMessageTypes.Error));
        //}
        ///// <summary>
        ///// Set success message in request data.
        ///// </summary>
        ///// <param name="message"></param>
        //protected void SetInfoMessage(string message)
        //{
        //    SetStatusMessage(new StatusMessage(message, StatusMessageTypes.Info));
        //}

        //#endregion

        //#region Log files

        /// <summary>
        /// Set and return controller log in given path.
        /// If forced, new instance is created, otherwise existing one is returned (default).
        /// </summary>
        /// <param name="path"></param>
        /// <param name="force">If true, new log instance is created. If false, existing instance is returned.</param>
        /// <returns></returns>
        protected ILog SetControllerLog(string path, bool force = false)
        {
            if (!force && _controllerLog != null) return _controllerLog;
            _controllerLog = new LogWriter(path, "a");
            return _controllerLog;
        }
        /// <summary>
        /// Set and return controller log in default path based on controller name and user.
        /// </summary>
        /// <param name="controllerName">Name of the controller</param>
        /// <param name="userName">User name who executes action.</param>
        /// <returns></returns>
        //protected ILog SetControllerLog(string controllerName, string userName)
        //{
        //    string logFileName = controllerName + "_" + userName + "_" +
        //                         DateTime.Now.ToStandardDateString("_") + ".log";
        //    string logPath = PathUtil.Combine(GlobalSettings.PerformanceLogsFolder, logFileName);
        //    return SetControllerLog(logPath);
        //}
        ///// <summary>
        ///// Set and return controller log in default path based on controller instance.
        ///// </summary>
        ///// <param name="controller">Instance of the controller</param>
        ///// <returns></returns>
        //protected ILog SetControllerLog(IgorController controller)
        //{
        //    string controllerName = controller?.ControllerContext?.RouteData?.Values?["controller"]?.ToString() ?? "Igorcontroller";
        //    return SetControllerLog(controllerName, (_userService?.GetCurrentUserName() ?? "Igoruser"));
        //}
        ///// <summary>
        ///// Get name of the performance log file based on controller and current user.
        ///// </summary>
        ///// <param name="controller"></param>
        ///// <returns></returns>
        //protected string GenerateControllerLogName(IgorController controller)
        //{
        //    string controllerName = controller?.ControllerContext?.RouteData?.Values?["controller"]?.ToString() ?? "Igorcontroller";
        //    string userName = _userService?.GetCurrentUserName() ?? "Igoruser";
        //    string logFileName = controllerName + "_" + userName + "_" +
        //                         DateTime.Now.ToStandardDateString("_") + ".log";
        //    return logFileName;
        //}

        #endregion

        #region Authorization

        ///// <summary>
        ///// Set token data in the session.
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //protected string SetAuthorizationSessionData(IgorUser user)
        //{
        //    if (user == null) return string.Empty;
        //    string token = _userService.GenerateIgorJwtToken(user);
        //    Session.SetJwtTokenSessionData(token);
        //    return token;
        //}

        ///// <summary>
        ///// Get user encoded in the token data in the session.
        ///// </summary>
        ///// <returns></returns>
        //protected IgorUser GetAuthorizationSessionData()
        //{
        //    string token = Session.GetJwtTokenSessionData();
        //    IgorUser result = token.GetUserFromIgorJwtToken();
        //    return result;
        //}

        #endregion

        #region Get helpers

        /// <summary>
        /// Get HTML helper instance in context of standard IgorBasicView.
        /// </summary>
        /// <returns></returns>
        protected HtmlHelper GetHtmlHelper()
        {
            ViewContext viewContext = new ViewContext(this.ControllerContext, new RazorView(this.ControllerContext, @"~/Views/Shared/_IgorView.cshtml", null, false, new List<string>()),
                this.ViewData, this.TempData, TextWriter.Null);
            HtmlHelper htmlHelper = new HtmlHelper(viewContext, new ViewPage());
            return htmlHelper;
        }
        /// <summary>
        /// Get AJAX helper instance in context of standard IgorBasicView.
        /// </summary>
        /// <returns></returns>
        protected AjaxHelper GetAjaxHelper()
        {
            ViewContext viewContext = new ViewContext(this.ControllerContext,
                new RazorView(this.ControllerContext, @"~/Views/Shared/_IgorBasicView.cshtml", null, false, new List<string>()),
                this.ViewData, this.TempData, TextWriter.Null);
            AjaxHelper ajaxHelper = new AjaxHelper(viewContext, new ViewPage());
            return ajaxHelper;
        }

        #endregion

        #region Exceptions and errors

        /// <summary>
        /// Handle exception in controller methods.
        /// </summary>
        /// <param name="filterContext"></param>
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    filterContext.ExceptionHandled = true;

        //    string controller = filterContext?.RouteData?.Values?["controller"]?.ToString() ?? "igorcontroller";
        //    string action = filterContext?.RouteData?.Values?["action"]?.ToString() ?? "unknown action";

        //    SetControllerLog(filterContext.Controller as IgorController);
        //    _controllerLog?.LogException("Exception occurred in controller: " + controller + ", action: " + action, filterContext?.Exception,
        //        LogLevels.Exception, true);

        //    bool isAjax = filterContext?.RequestContext?.HttpContext?.Request?.IsAjaxRequest() ?? false;

        //    int statusCode = (filterContext?.Exception as HttpException)?.GetHttpCode() ?? 500;
        //    PageErrorViewModel model = new PageErrorViewModel(statusCode, "IGOR - Error!", "Error occurred!",
        //        "Something went wrong. Please try again later or contact support. Refer to: " +
        //        Path.GetFileNameWithoutExtension(_controllerLog?.GetLogPath() ?? string.Empty));

        //    if (isAjax)
        //    {
        //        //bool isOpeningModal = filterContext?.RouteData?.Values?["openmodal"]?.ToString()?.ToBool() ?? false;
        //        bool isOpeningModal = filterContext?.RequestContext?.HttpContext?.Request?.Params?.GetValues("openmodal")?[0]?.ToBool() ?? false;
        //        filterContext.Result = new PartialViewResult()
        //        {
        //            ViewName = isOpeningModal ? "~/Views/Shared/_ErrorPageModal.cshtml" : "~/Views/Shared/_ErrorPage.cshtml",
        //            ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
        //            {
        //                Model = model
        //            }
        //        };
        //    }
        //    else
        //    {
        //        filterContext.Result = new ViewResult()
        //        {
        //            ViewName = "~/Views/Shared/_ErrorPage.cshtml",
        //            ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
        //            {
        //                Model = model
        //            }
        //        };
        //    }
        //}
        ///// <summary>
        ///// Get error message from exception for debug environment or empty for others.
        ///// </summary>
        ///// <param name="ex"></param>
        ///// <returns></returns>
        //public string GetExceptionMessage(Exception ex)
        //{
        //    if (GlobalSettings.Environment == IgorEnvironments.Debug) return ex.GetExceptionMessage();
        //    return string.Empty;
        //}

        #endregion

        #region Other - json - url

        /// <summary>
        /// Get json result with standard content type and encoding with allow GET.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult JsonResult(object data)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }
        /// <summary>
        /// Get base url in format protocol://url:port.
        /// </summary>
        /// <returns></returns>
        public virtual string GetBaseUrl()
        {
            return $"{Request?.Url?.Scheme}://{Request?.Url?.Authority}";
        }

        #endregion
    }
}