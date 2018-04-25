using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARPARK.Web.Code
{
    public class CustomErrorAttribute : HandleErrorAttribute
    {
        private readonly Logger _logger;

        public CustomErrorAttribute()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public override void OnException(ExceptionContext filterContext)
        {
            // log the error using log4net.
            _logger.Error(filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}