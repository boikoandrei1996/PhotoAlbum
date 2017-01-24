using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Interfaces;
using BLL.Entities;
using NLog;

namespace WebApplicationPL.Controllers
{
    using Models;

    public class HomeController : Controller
    {
        #region fields&constructor
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        private readonly IPhotoService photoService;
        public HomeController(IUserService _userService, IProfileService _profileService, 
                                IPhotoService _photoService)
        {
            userService = _userService;
            profileService = _profileService;
            photoService = _photoService;
        }
        #endregion

        public ActionResult Index(int page = 1)
        {
            int pageSize = 3;
            var allUsers = userService.GetAll().OrderBy(u => u.Login);
            var selectedBLLUsers = allUsers.Skip(pageSize * (page - 1)).Take(pageSize);
            var pageInfo = new PageInfoViewModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                TotalItems = allUsers.Count()
            };

            var viewUsers = new List<UserViewModel>();
            foreach (var user in selectedBLLUsers)
                viewUsers.Add(new UserViewModel
                {
                    Id = user.Id,
                    Login = user.Login
                });

            var model = new ListUserViewModel
            {
                Users = viewUsers,
                PageInfo = pageInfo
            };

            return View(model);
        }

        public ActionResult SearchResult(string searchText, int page = 1)
        {
            int pageSize = 3;
            ListSearchResultViewModel model;
            var results = new List<SearchResultViewModel>();
            var pageInfo = new PageInfoViewModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                TotalItems = results.Count
            };

            ViewBag.SearchText = searchText ?? string.Empty;
            if (searchText == null || searchText == string.Empty)
            {
                model = new ListSearchResultViewModel
                {
                    PageInfo = pageInfo,
                    Results = results
                };
                return View(model);
            }

            if (searchText.Length > 20)
                searchText = searchText.Remove(20);
                        
            SearchProfileMatch(searchText, results);
            SearchPhotoMatch(searchText, results);

            pageInfo.TotalItems = results.Count;
            model = new ListSearchResultViewModel
            {
                PageInfo = pageInfo,
                Results = results.Skip(pageSize * (page - 1)).Take(pageSize)
            };            

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = new List<string> {
                "You can register, upload photos.",
                "You have ability to view and to like the photos of other users.",
                "You have ability to search photos by description."
            };

            return View();
        }

        public ActionResult Contact() => View();

        #region Private methods
        private void SearchProfileMatch(string firstOrLastName, ICollection<SearchResultViewModel> results)
        {
            var partsOfName = firstOrLastName.TrimEnd(new char[] { ' ' }).Split(new char[] { ' ' });

            foreach (var part in partsOfName)
                foreach (var profile in profileService.GetAllByName(part))
                    results.Add(new SearchResultViewModel
                    {
                        Id = profile.Id,
                        Text = $"{profile.LastName} {profile.FirstName}",
                        TypeResult = TypeSearchResult.Profile
                    });
        }
        private void SearchPhotoMatch(string description, ICollection<SearchResultViewModel> results)
        {
            var photos = photoService.GetPhotosByDescription(description, TimeSpan.FromMilliseconds(250));

            foreach (var photo in photos)
                results.Add(new SearchResultViewModel
                {
                    Id = photo.Id,
                    Text = photo.Description,
                    TypeResult = TypeSearchResult.Photo
                });
        }
        #endregion
    }
}