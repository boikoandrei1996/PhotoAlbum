using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;

using BLL.Interfaces;
using BLL.Entities;
using System.Web.Security;
using System.Web.Helpers;
using System.IO;
using NLog;

namespace WebApplicationPL.Controllers
{
    using Models;

    public class AccountController : Controller
    {
        #region fields&constructor
        //private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public AccountController(IUserService _userService, IRoleService _roleService)
        {
            userService = _userService;
            roleService = _roleService;
        }
        #endregion

        #region Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View();

            if (!userService.CheckPassword(model.Login, model.Password))
            {
                ModelState.AddModelError("", "Incorrect login or password");
                return View();                
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                return Redirect(returnUrl ?? Url.Action(model.Login, "Profile"));
            }
        }
        #endregion

        #region Register
        public ActionResult Register() => View();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = new BLLUser
            {
                Login = model.Login,
                Password = Crypto.HashPassword(model.Password),
                RoleId = roleService.GetByName("user").Id,
                Profile = new BLLProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    AvatarPhoto = model.Avatar ?? GetDefaultAvatarPhoto(),
                    AvatarMimeType = model.AvatarMimeType
                }
            };

            try
            {
                userService.Save(user);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Login", "This login is not available");
                return View();
            }

            FormsAuthentication.SetAuthCookie(model.Login, false);

            return RedirectToAction(user.Login, "Profile");
        }
        #endregion

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private byte[] GetDefaultAvatarPhoto() 
            => System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Images/defaultAvatar.png"));
    }
}