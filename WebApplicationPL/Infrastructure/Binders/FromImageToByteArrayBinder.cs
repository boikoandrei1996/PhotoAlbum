using System.ComponentModel;
using System.Web.Mvc;

namespace WebApplicationPL.Infrastructure.Binders
{
    public class FromImageToByteArrayBinder : IPropertyBinder
    {
        public object BindProperty(ControllerContext controllerContext,
                                   ModelBindingContext bindingContext,
                                   PropertyDescriptor propertyDescriptor)
        {
            var file = controllerContext.HttpContext.Request.Files[propertyDescriptor.Name];

            if (file == null || file.ContentLength == 0)
            {
                //bindingContext.ModelState.AddModelError(propertyDescriptor.Name, "Image not recognized");
                return null;
            }
            
            var imageBytes = new byte[file.ContentLength];
            file.InputStream.Read(imageBytes, 0, file.ContentLength);
            return imageBytes;
        }
    }
}