using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using BLL.Interfaces;
using BLL.Entities;

namespace WebApplicationPL.Controllers
{
    using Models;

    [Authorize]
    public class ProfileController : Controller
    {
        #region fields&constructor
        private readonly IProfileService profileService;
        private readonly IUserService userService;
        private readonly IPhotoService photoService;
        private readonly ILikeService likeService;
        public ProfileController(IProfileService _profileService, IUserService _userService,
                                    IPhotoService _photoService, ILikeService _likeService)
        {
            profileService = _profileService;
            userService = _userService;
            photoService = _photoService;
            likeService = _likeService;
        }
        #endregion

        #region Index
        [AllowAnonymous]
        public ActionResult Index(int id, int page = 1)
        {
            var user = userService.GetById(id);
            if (user == null)
                return HttpNotFound();

            var profileInfo = GetProfileInfo(user);

            ViewBag.CurrentPage = page;
            return View(profileInfo);
        }
        
        [AllowAnonymous]
        public ActionResult IndexByLogin(string login, int page = 1)
        {
            if (login == null || login == string.Empty)
                return HttpNotFound();

            var user = userService.GetByLogin(login);
            if (user == null)
                return HttpNotFound();

            var profileInfo = GetProfileInfo(user);

            ViewBag.CurrentPage = page;
            return View("Index", profileInfo);
        }
        #endregion
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int userId)
        {
            var user = userService.GetById(userId);
            if (user == null)
                return HttpNotFound();

            if (user.Login != HttpContext.User.Identity.Name)
                return RedirectToAction("Login", "Account");

            userService.Delete(user.Id);
            System.Web.Security.FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #region Edit
        public ActionResult Edit(int id)
        {
            var user = userService.GetById(id);
            if (user == null)
                return HttpNotFound();
            
            var profileEdit = new ProfileEditViewModel
            {
                UserId = user.Id,
                Login = user.Login,
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName,
                Email = user.Profile.Email ?? "<unknown>",
                BirthDate = user.Profile.BirthDate
            };

            return View(profileEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = userService.GetById(model.UserId);
            if (user == null)
                return HttpNotFound();

            if (user.Login != HttpContext.User.Identity.Name)
                return RedirectToAction("Login", "Account");            

            if (user.Login != model.Login)
            {
                user.Login = model.Login;
                userService.Save(user);
            }

            var profile = user.Profile;

            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.Email = model.Email;

            if (model.BirthDate != null)
                profile.BirthDate = model.BirthDate.Value;

            if (model.Avatar != null)
            {
                profile.AvatarPhoto = model.Avatar;
                profile.AvatarMimeType = model.AvatarMimeType;
            }

            profileService.Save(profile);

            return RedirectToAction(user.Id.ToString(), "Profile");
        }
        #endregion

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult UserPhotos(int userId, int page)
        {
            int pageSize = 3;
            var viewPhotos = new List<ProfilePhotoItemViewModel>();
            
            foreach (var photo in photoService.GetUserPhotos(userId))
                viewPhotos.Add(new ProfilePhotoItemViewModel
                {
                    PhotoId = photo.Id,
                    LikeCount = likeService.GetLikesForPhoto(photo.Id).Count
                });

            var pageInfo = new PageInfoViewModel
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = viewPhotos.Count
            };

            var model = new ListProfilePhotoViewModel
            {
                UserId = userId,
                PageInfo = pageInfo,
                Photos = viewPhotos.Skip(pageSize * (page - 1)).Take(pageSize)
            };
            
            return PartialView("_UserPhotos", model);
        }

        #region Private methods
        private ProfileInfoViewModel GetProfileInfo(BLLUser user)
        {
            return new ProfileInfoViewModel
            {
                UserId = user.Id,
                Login = user.Login,
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName,
                Email = user.Profile.Email ?? "<unknown>",
                Age = GetAge(user.Profile.BirthDate)
            };
        }
        private int GetAge(DateTime birthdate)
        {
            var today = DateTime.Today;
            var years = today.Year - birthdate.Year;

            if (today < birthdate.AddYears(years))
                --years;

            return years;
        }
        #endregion
    }
}