//using System.Diagnostics;
//using System.Net.Http;
//using System.Web.Http.Filters;
//using Cares.WebBase.Mvc;
//using Cares.WebBase.UnityConfiguration;
//using FRS.Interfaces.IServices;
//using Microsoft.Practices.Unity;
//using Newtonsoft.Json;

//namespace FRS.WebBase.Mvc
//{
//    /// <summary>
//    /// Api Exception filter attribute for Api controller methods
//    /// </summary>
//    public class ApiException : ActionFilterAttribute
//    {
//        #region Private

//        // ReSharper disable InconsistentNaming
//        private static ILogger caresLogger;
//        // ReSharper restore InconsistentNaming
//        /// <summary>
//        /// Get Configured logger
//        /// </summary>
//        // ReSharper disable InconsistentNaming
//        private static ILogger CaresLogger
//        // ReSharper restore InconsistentNaming
//        {
//            get
//            {
//                if (caresLogger != null) return caresLogger;
//                caresLogger = (UnityConfig.GetConfiguredContainer()).Resolve<ILogger>();
//                return caresLogger;
//            }
//        }
//        /// <summary>
//        /// Set status code and contents of the Application exception
//        /// </summary>
//        private void SetApplicationResponse(HttpActionExecutedContext filterContext)
//        {
//            CaresExceptionContent contents = new CaresExceptionContent
//            {
//                Message = filterContext.Exception.Message    
//            };
//            filterContext.Response = new HttpResponseMessage
//            {
//                StatusCode = System.Net.HttpStatusCode.BadRequest,
//                Content = new StringContent(JsonConvert.SerializeObject(contents))                
//            };
//        }
//        /// <summary>
//        /// Set General Exception
//        /// </summary>
//        private void SetGeneralExceptionApplicationResponse(HttpActionExecutedContext filterContext)
//        {
//            CaresExceptionContent contents = new CaresExceptionContent
//            {
//                Message = Resources.GeneralErrors.ErrorPerformingOperation
//            };
//            filterContext.Response = new HttpResponseMessage
//            {
//                StatusCode = System.Net.HttpStatusCode.BadRequest,
//                Content = new StringContent(JsonConvert.SerializeObject(contents))
//            };
//        }
//        #endregion
//        #region Public
//        /// <summary>
//        /// Exception Handler for api calls; apply this attribute for all the Api calls
//        /// </summary>
//        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
//        {
//            if (filterContext.Exception == null)
//            {
//                return;
//            }
//            if (filterContext.Exception is CaresException)
//            {
//                SetApplicationResponse(filterContext);
//                CaresLogger.Write(filterContext.Exception, LoggerCategories.Error, -1, 0, TraceEventType.Warning, "Web Api Exception - CaReS Exception", null);
//            }
//            else
//            {
//                SetGeneralExceptionApplicationResponse(filterContext);
//                CaresLogger.Write(filterContext.Exception, LoggerCategories.Error, -1, 0, TraceEventType.Warning, "Web Api Exception - General", null);
//            }
//        }

//        #endregion
//    }
//}
