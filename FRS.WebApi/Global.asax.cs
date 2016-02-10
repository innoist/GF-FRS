using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using FRS.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;

namespace FRS.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IUnityContainer container;

        /// <summary>
        /// Create the unity container
        /// </summary>
        private static IUnityContainer CreateUnityContainer()
        {
            container = UnityWebActivator.Container;
            RegisterTypes();
            return container;
        }

        protected void Application_Start()
        {
            RegisterIoC();
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterTypes(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Register types with the IoC
        /// </summary>
        private static void RegisterTypes()
        {
            WebBase.TypeRegistrations.RegisterTypes(container);
            Implementation.TypeRegistrations.RegisterType(container);
        }
        /// <summary>
        /// Register unity 
        /// </summary>
        private static void RegisterIoC()
        {
            if (container == null)
            {
                container = CreateUnityContainer();
            }
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                System.Web.HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return System.Web.HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath != null && 
                System.Web.HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/Api");
        }
    }
}
