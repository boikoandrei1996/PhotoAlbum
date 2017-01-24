using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Interfaces;
using BLL.Entities;

namespace WebApplicationPL.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        #region fields&constructor
        private readonly IPhotoService photoService;
        private readonly ILikeService likeService;
        private readonly IUserService userService;
        public LikeController(IPhotoService _photoService, ILikeService _likeService,
                               IUserService _userService)
        {
            photoService = _photoService;
            likeService = _likeService;
            userService = _userService;
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int photoId)
        {
            var photo = photoService.GetById(photoId);
            if (photo == null)
                return HttpNotFound();

            var login = HttpContext.User.Identity.Name;
            var user = userService.GetByLogin(login);

            var like = likeService.GetLikeFromUserToPhoto(user.Id, photo.Id);
            if (like == null)
                likeService.Save(new BLLLike
                {
                    PhotoId = photo.Id,
                    UserId = user.Id
                });

            var likeCount = likeService.GetLikesForPhoto(photo.Id).Count;

            return PartialView("_NewCountLike", likeCount);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int photoId)
        {
            var photo = photoService.GetById(photoId);
            if (photo == null)
                return HttpNotFound();

            var login = HttpContext.User.Identity.Name;
            var user = userService.GetByLogin(login);

            var like = likeService.GetLikeFromUserToPhoto(user.Id, photo.Id);
            if (like != null)
                likeService.Delete(like.Id);

            var likeCount = likeService.GetLikesForPhoto(photo.Id).Count;

            return PartialView("_NewCountLike", likeCount);
        }

        [AllowAnonymous]
        public JsonResult ListLikesJson(int photoId)
        {
            var likes = likeService.GetLikesForPhoto(photoId);

            var users = new List<BLLUser>();
            foreach (var like in likes)
                users.Add(userService.GetById(like.UserId));

            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}