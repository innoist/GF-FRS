using Cares.Interfaces.Security;
using Cares.WebBase.Mvc;
using Microsoft.Practices.Unity;

namespace Cares.WebApi
{
    /// <summary>
    /// Register types WebApi Project
    /// </summary>
    public static class TypeRegistration
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IWebApiAuthenticationChecker, WebApiAuthenticationChecker>();
        }
    }
}