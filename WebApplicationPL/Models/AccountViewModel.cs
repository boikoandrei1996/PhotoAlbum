using System;
using System.ComponentModel.DataAnnotations;


namespace WebApplicationPL.Models
{
    using Infrastructure.Binders;
    using Infrastructure;

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login field is required")]
        [MaxLength(20, ErrorMessage = "Too long login")]
        [MinLength(5, ErrorMessage = "Too short login")]
        [Display(Name = "Your login:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Too long password")]
        [MinLength(5, ErrorMessage = "Too short password")]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login field is required")]
        [MaxLength(20, ErrorMessage = "Too long login")]
        [MinLength(5, ErrorMessage = "Too short login")]
        [Display(Name = "Your login:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [MaxLength(20, ErrorMessage = "Too long password")]
        [MinLength(5, ErrorMessage = "Too short password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password don't match")]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }

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

        [Required(ErrorMessage = "Birthdate field is required")]
        [DataType(DataType.Date)]
        [Birthdate(false, ErrorMessage = "Incorrect birthdate")]
        [Display(Name = "Your birthdate:")]
        public DateTime BirthDate { get; set; }

        [PropertyBinder(typeof(FromImageToByteArrayBinder))]
        [Display(Name = "Avatar photo:")]
        public byte[] Avatar { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [PropertyBinder(typeof(ImageExtensionBinder))]
        public string AvatarMimeType { get; set; }
    }
}