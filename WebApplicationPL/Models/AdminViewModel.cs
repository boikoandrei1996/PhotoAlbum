using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationPL.Models
{
    public class AdminViewModel
    {
        [Display(Name = "Id")]
        public int UserId { get; set; }
        
        public string Login { get; set; }
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public string Birthdate { get; set; }
        
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}