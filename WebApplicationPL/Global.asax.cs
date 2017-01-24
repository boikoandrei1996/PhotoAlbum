using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Mapping;
using NLog;
using WebApplicationPL.Infrastructure.Binders;

namespace WebApplicationPL
{
    public class MvcApplication : HttpApplication
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new ModelBinderBase();
            MapperInitializer.Initialize();
            logger.Info("\nMVCApplication start work");
        }
    }
}
