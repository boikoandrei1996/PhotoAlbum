using System.Web.Mvc;
using System.Linq;
using System.ComponentModel;

namespace WebApplicationPL.Infrastructure.Binders
{
    public class ModelBinderBase : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext,
                                             ModelBindingContext bindingContext,
                                             PropertyDescriptor propertyDescriptor)
        {
            var attribute = FindPropertyBinderAttribute(propertyDescriptor);
            if (attribute != null)
            {
                var value = attribute.GetBinder().BindProperty(controllerContext, bindingContext, propertyDescriptor);
                propertyDescriptor.SetValue(bindingContext.Model, value);
            }
            else
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }

        PropertyBinderAttribute FindPropertyBinderAttribute(PropertyDescriptor propertyDescriptor)
        {
            return propertyDescriptor.Attributes.OfType<PropertyBinderAttribute>().FirstOrDefault();
        }
    }
}