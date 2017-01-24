using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplicationPL.Models
{
    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Login { get; set; }
    }

    public class ListUserViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public PageInfoViewModel PageInfo { get; set; }
    }


    public enum TypeSearchResult
    {
        Profile,
        Photo
    }

    public class SearchResultViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public TypeSearchResult TypeResult { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        public string Text { get; set; }
    }

    public class ListSearchResultViewModel
    {
        public IEnumerable<SearchResultViewModel> Results { get; set; }
        public PageInfoViewModel PageInfo { get; set; }
    }
}