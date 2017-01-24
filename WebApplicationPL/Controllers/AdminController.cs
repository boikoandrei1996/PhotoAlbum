using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Interfaces;
using BLL.Entities;

namespace WebApplicationPL.Controllers
{
    using Models;

    [Authorize(Roles = "admin, moderator")]
    public class AdminController : Controller
    {
        #region fields&constructor
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        public AdminController(IUserService _userService, IRoleService _roleService)
        {
            userService = _userService;
            roleService = _roleService;
        }
        #endregion

        public ActionResult Index()
        {
            var listViewModelUsers = new List<AdminViewModel>();

            foreach(var user in userService.GetAll())
                listViewModelUsers.Add(MapToAdminViewModel(user, roleService.GetUserRole(user)));
            
            return View(listViewModelUsers);
        }

        #region Edit
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int userId)
        {
            var user = userService.GetById(userId);
            if (user == null)
                return HttpNotFound();

            var role = roleService.GetUserRole(user);

            var model = MapToAdminViewModel(user, role);

            ViewBag.Roles = new SelectList(roleService.GetAll(), "Id", "Name");
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminViewModel model)
        {
            var newRole = roleService.GetById(model.RoleId);
            if (newRole == null)
                ModelState.AddModelError("Role", "Incorrect role name");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(roleService.GetAll(), "Id", "Name");
                return View(model);
            }         

            var user = userService.GetById(model.UserId);
            if (user == null)
                return HttpNotFound();

            if (user.RoleId != newRole.Id)
            {
                user.RoleId = newRole.Id;
                userService.Save(user);
            }

            return RedirectToAction("Index", "Admin");
        }
        #endregion

        #region Delete
        public ActionResult Delete(int userId)
        {
            var user = userService.GetById(userId);
            if (user == null)
                return HttpNotFound();

            var role = roleService.GetUserRole(user);

            var model = MapToAdminViewModel(user, role);

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int userId)
        {
            var user = userService.GetById(userId);
            if (user == null)
                return HttpNotFound();

            var login = HttpContext.User.Identity.Name;

            if (user.Login != login)
                userService.Delete(userId);         

            return RedirectToAction("Index");
        }
        #endregion

        #region Private methods
        private AdminViewModel MapToAdminViewModel(BLLUser user, BLLRole role)
        {
            return new AdminViewModel
            {
                UserId = user.Id,
                Login = user.Login,
                Firstname = user.Profile.FirstName,
                Lastname = user.Profile.LastName,
                Email = user.Profile.Email ?? "<unknown>",
                Birthdate = user.Profile.BirthDate.ToShortDateString(),
                RoleId = role.Id,
                RoleName = role.Name
            };
        }
        #endregion
    }
}