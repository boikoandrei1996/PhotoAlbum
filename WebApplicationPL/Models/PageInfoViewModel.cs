using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationPL.Models
{
    public class PageInfoViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / PageSize);
            }
        }
    }
}