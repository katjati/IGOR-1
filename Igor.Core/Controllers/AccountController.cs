using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Igor.Core.Models.Account;
using Igor.Core.Models.API;
using Igor.Library.Abstract;
using Igor.Library.Models;

namespace Igor.Core.Controllers
{
    public class AccountController : IgorController
    {
        #region Properties

        #endregion

        #region Constructors

        public AccountController(IUserService uService) : base(uService)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Login screen.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Login", new LoginViewModel());
        }

        /// <summary>
        /// Verify and log user in and set up session and other identity framework stuff.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            bool loginSuccessfull = false;
            IgorUser user = _userService.FindUser(model.UserName, model.Password);
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    _userService.SignIn(user);
                    loginSuccessfull = true;
                }
            }
            if (loginSuccessfull)
            {
                return Redirect(returnUrl ?? Url.Action("Index", "Terminal"));
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View("Login", model);
            }
        }

        /// <summary>
        /// Log user off.
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult SignOut()
        {
            _userService.SignOut();
            return RedirectToAction("Index", "Terminal");
        }
        #endregion
    }
}