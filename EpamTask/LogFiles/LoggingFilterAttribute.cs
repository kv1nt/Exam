using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using NLog.Fluent;

namespace EpamTask.LogFiles
{
    public class LoggingFilterAttribute: ActionFilterAttribute
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log.Info("ActionResult " + filterContext.ActionDescriptor.ActionName + " () invocked succesfully " +
                                      filterContext.Controller);
           
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            //if (filterContext.Exception != null)
            //{
            //    Log.Error(filterContext.Exception);
               
            //}
        }
    }

    public class ErrorFilterAttribute : HandleErrorAttribute
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext filterContext)
        {
            Log.Error(filterContext.Exception);
        }
    }
}