using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.CustomsFilter
{
    public class CustomException : FilterAttribute, IExceptionFilter
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CustomException));

        public void OnException(ExceptionContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            string exceptionmessage = filterContext.Exception.Message;
            filterContext.Result = new PartialViewResult()
            {
                ViewName = "ErrorPartial"
            };
            Log.Error(controller + "-" + actionName + "-" + exceptionmessage);
            filterContext.ExceptionHandled = true;
        }
    }
}