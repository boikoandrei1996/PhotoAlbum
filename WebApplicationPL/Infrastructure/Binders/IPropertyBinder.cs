using System.ComponentModel;
using System.Web.Mvc;

namespace WebApplicationPL.Infrastructure.Binders
{
    public interface IPropertyBinder
    {
        object BindProperty(ControllerContext controllerContext, 
                            ModelBindingContext bindingContext, 
                            PropertyDescriptor propertyDescriptor);
    }
}
