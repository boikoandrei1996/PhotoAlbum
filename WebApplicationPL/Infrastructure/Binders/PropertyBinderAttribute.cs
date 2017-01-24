using System;

namespace WebApplicationPL.Infrastructure.Binders
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyBinderAttribute : Attribute
    {
        public Type BinderType { get; private set; }
        public PropertyBinderAttribute(Type binderType)
        {
            BinderType = binderType;
        }
        public IPropertyBinder GetBinder()
        {
            return System.Web.Mvc.DependencyResolver.Current.GetService(BinderType) as IPropertyBinder;
        }
    }
}