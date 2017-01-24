using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Entities;
using BLL.Interfaces;

namespace WebApplicationPL.Controllers
{
    using Models;

    [Authorize]
    public class PhotoController : Controller
    {
        #region fields&constructor
        private readonly IPhotoService photoService;
        private readonly ILikeService likeService;
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        public PhotoController(IPhotoService _photoService, ILikeService _likeService,
                               IUserService _userService, IProfileService _profileService)
        {
            photoService = _photoService;
            likeService = _likeService;
            userService = _userService;
            profileService = _profileService;
        }
        #endregion

        #region Upload
        public ActionResult Upload() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadPhotoViewModel model)
        {
            if (model.Photo == null)
                ModelState.AddModelError("Photo", "Can't upload this photo");

            if (!ModelState.IsValid)
                return View();

            string login = HttpContext.User.Identity.Name;
            int userId = userService.GetByLogin(login).Id;

            photoService.Save(new BLLPhoto
            {
                UserId = userId,
                Content = model.Photo,
                ContentMimeType = model.PhotoMimeType,
                DateCreation = DateTime.Today.Date,
                Description = model.Description
            });

            return RedirectToAction(userId.ToString(), "Profile");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int photoId)
        {
            var photo = photoService.GetById(photoId);
            if (photo == null)
                return HttpNotFound();

            var login = HttpContext.User.Identity.Name;
            if (!CheckUserPhoto(photo, login))
                return RedirectToAction("Login", "Account");

            var viewPhoto = new EditPhotoViewModel
            {
                PhotoId = photo.Id,
                Description = photo.Description
            };

            return View(viewPhoto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPhotoViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var photo = photoService.GetById(model.PhotoId);
            if (photo == null)
                return HttpNotFound();

            var login = HttpContext.User.Identity.Name;
            if (!CheckUserPhoto(photo, login))
                return RedirectToAction("Login", "Account");

            if (photo.Description != model.Description)
            {
                photo.Description = model.Description;
                photoService.Save(photo);
            }

            return RedirectToAction(photo.Id.ToString(), "Photo");
        }
        #endregion

        public ActionResult List(int userId)
        {
            var viewPhotos = new List<EditPhotoViewModel>();
            var photos = photoService.GetUserPhotos(userId);

            foreach (var photo in photos)
                viewPhotos.Add(new EditPhotoViewModel
                {
                    PhotoId = photo.Id,
                    Description = photo.Description
                });

            return View(viewPhotos);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int photoId)
        {
            var photo = photoService.GetById(photoId);
            if (photo == null)
                return HttpNotFound();

            var login = HttpContext.User.Identity.Name;

            if (!CheckUserPhoto(photo, login))
                return RedirectToAction("Login", "Account");
            
            photoService.Delete(photo.Id);

            return RedirectToAction(photo.UserId.ToString(), "Profile");
        }

        [AllowAnonymous]
        public ActionResult ShowPhoto(int id)
        {
            var photo = photoService.GetById(id);
            if (photo == null)
                return HttpNotFound();

            var user = userService.GetPhotoUser(photo);
            if (user == null)
                return HttpNotFound();

            var viewPhoto = new ShowPhotoViewModel
            {
                UserId = photo.UserId,
                Login = user.Login,
                PhotoId = photo.Id,
                DateCreation = photo.DateCreation,
                Description = photo.Description,
                LikeCount = likeService.GetLikesForPhoto(photo.Id).Count
            };

            return View(viewPhoto);
        }

        [AllowAnonymous]
        public FileResult Get(int photoId)
        {
            var photo = photoService.GetById(photoId);
            if (photo == null)
                return null;

            return File(photo.Content, photo.ContentMimeType);
        }
        
        [AllowAnonymous]
        public FileResult Avatar(int userId)
        {
            var profile = profileService.GetById(userId);
            if (profile == null)
                return null;

            return File(profile.AvatarPhoto, profile.AvatarMimeType);
        }
        
        private bool CheckUserPhoto(BLLPhoto photo, string login)
        {
            if (photo == null)
                return false;

            var user = userService.GetPhotoUser(photo);
            if (user == null)
                return false;

            return user.Login == login;
        }
    }
}