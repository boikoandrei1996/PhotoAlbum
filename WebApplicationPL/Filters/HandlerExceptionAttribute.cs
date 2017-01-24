using System;
using System.Web.Mvc;
using NLog;

namespace WebApplicationPL.Filters
{
    public class HandlerAllExceptionAttribute : HandleErrorAttribute, IExceptionFilter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public new void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                logger.Log(LogLevel.Fatal, 
                    $"---{DateTime.Now}---" + 
                    $"{Environment.NewLine}Message: {filterContext.Exception.Message}{Environment.NewLine}" + 
                    $"InnerException: {filterContext.Exception.InnerException}{Environment.NewLine}" + 
                    $"StackTrace: {filterContext.Exception.StackTrace}{Environment.NewLine}{Environment.NewLine}");
                base.OnException(filterContext);
            }
        }
    }
}