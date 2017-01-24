using System.ComponentModel;
using System.Web.Mvc;

namespace WebApplicationPL.Infrastructure.Binders
{
    public class ImageExtensionBinder : IPropertyBinder
    {
        public object BindProperty(ControllerContext controllerContext,
                                   ModelBindingContext bindingContext,
                                   PropertyDescriptor propertyDescriptor)
        {
            var propertyName = propertyDescriptor.Name;
            //AvatarMimeType => Avatar MimeType => Avatar
            //PhotoMimeType => Photo MimeType => Photo
            var file = controllerContext.HttpContext.Request.Files[propertyName.Remove(propertyName.Length - 8, 8)];

            if (file == null || file.ContentLength == 0)
                return "image/png";
            else
                return file.ContentType;
        }
    }
}