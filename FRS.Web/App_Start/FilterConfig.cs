using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Cares.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IUnityContainer container)
        {
            filters.Add(container.Resolve<IExceptionFilter>());
        }
    }
}
