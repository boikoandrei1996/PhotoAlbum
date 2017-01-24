using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPL.Models
{
    using Infrastructure.Binders;
    using Infrastructure;

    public class ProfileInfoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [Display(Name = "Login:")]
        public string Login { get; set; }

        [Display(Name = "Firstname:")]
        public string FirstName { get; set; }

        [Display(Name = "Lastname:")]
        public string LastName { get; set; }

        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Age:")]
        public int Age { get; set; }
    }

    public class ProfilePhotoViewModel
    {
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public int LikeCount { get; set; }
    }

    public class ProfileEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Login field is required")]
        [MaxLength(20, ErrorMessage = "Too long login")]
        [MinLength(5, ErrorMessage = "Too short login")]
        [Display(Name = "Your login:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Firstname field is required")]
        [MaxLength(20, ErrorMessage = "Too long firstname")]
        [Display(Name = "Your firstname:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname field is required")]
        [MaxLength(20, ErrorMessage = "Too long lastname")]
        [Display(Name = "Your lastname:")]
        public string LastName { get; set; }

        [Display(Name = "Your email:")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        [MaxLength(50, ErrorMessage = "Too long email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Birthdate(true, ErrorMessage = "Incorrect birthdate")]
        [Display(Name = "Your birthdate:")]
        public DateTime? BirthDate { get; set; }

        [PropertyBinder(typeof(FromImageToByteArrayBinder))]
        [Display(Name = "Avatar photo:")]
        public byte[] Avatar { get; set; }

        [HiddenInput(DisplayValue = false)]
        [PropertyBinder(typeof(ImageExtensionBinder))]
        public string AvatarMimeType { get; set; }
    }

    public class ProfilePhotoItemViewModel
    {
        public int PhotoId { get; set; }
        public int LikeCount { get; set; }
    }

    public class ListProfilePhotoViewModel
    {
        public int UserId { get; set; }
        public IEnumerable<ProfilePhotoItemViewModel> Photos { get; set; }
        public PageInfoViewModel PageInfo { get; set; }
    }
}