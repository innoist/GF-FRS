using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Filters;
using FRS.WebBase.UnityConfiguration;
using FRS.Interfaces.IServices;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using FRS.ExceptionHandling;

namespace FRS.WebBase.Mvc
{
    /// <summary>
    /// Api Exception filter attribute for Api controller methods
    /// </summary>
    public class ApiException : ActionFilterAttribute
    {
        #region Private

        // ReSharper disable InconsistentNaming
        private static ILogger frsLogger;
        // ReSharper restore InconsistentNaming
        /// <summary>
        /// Get Configured logger
        /// </summary>
        // ReSharper disable InconsistentNaming
        private static ILogger FRSLogger
        // ReSharper restore InconsistentNaming
        {
            get
            {
                if (frsLogger != null) return frsLogger;
                frsLogger = (UnityConfig.GetConfiguredContainer()).Resolve<ILogger>();
                return frsLogger;
            }
        }
        /// <summary>
        /// Set status code and contents of the Application exception
        /// </summary>
        private void SetApplicationResponse(HttpActionExecutedContext filterContext)
        {
            FRSExceptionContent contents = new FRSExceptionContent
            {
                Message = filterContext.Exception.Message
            };
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(contents))
            };
        }
        /// <summary>
        /// Set General Exception
        /// </summary>
        private void SetGeneralExceptionApplicationResponse(HttpActionExecutedContext filterContext)
        {
            FRSExceptionContent contents = new FRSExceptionContent
            {
                Message = Resources.GeneralErrors.ErrorPerformingOperation
            };
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(contents))
            };
        }
        #endregion
        #region Public
        /// <summary>
        /// Exception Handler for api calls; apply this attribute for all the Api calls
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                return;
            }
            if (filterContext.Exception is FRSException)
            {
                SetApplicationResponse(filterContext);
                FRSLogger.Write(filterContext.Exception, LoggerCategories.Error, -1, 0, TraceEventType.Warning, "Web Api Exception - FRS Exception", null);
            }
            else
            {
                SetGeneralExceptionApplicationResponse(filterContext);
                FRSLogger.Write(filterContext.Exception, LoggerCategories.Error, -1, 0, TraceEventType.Warning, "Web Api Exception - General", null);
            }
        }

        #endregion
    }
}
