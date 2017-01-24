using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPL.Models
{    
    using Infrastructure.Binders;

    public class UploadPhotoViewModel
    {
        [PropertyBinder(typeof(FromImageToByteArrayBinder))]
        public byte[] Photo { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [PropertyBinder(typeof(ImageExtensionBinder))]
        public string PhotoMimeType { get; set; }

        [Display(Name = "Description photo: ")]
        [MaxLength(255, ErrorMessage = "Too long description")]
        public string Description { get; set; }
    }

    public class ShowPhotoViewModel
    {
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public int LikeCount { get; set; }
    }

    public class EditPhotoViewModel
    {
        public int PhotoId { get; set; }

        [Display(Name = "Description photo: ")]
        [MaxLength(255, ErrorMessage = "Too long description")]
        public string Description { get; set; }
    }
}