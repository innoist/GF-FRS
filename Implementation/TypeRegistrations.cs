using Cares.Models.IdentityModels;
using FRS.Implementation.Identity;
using FRS.Implementation.Services;
using FRS.Interfaces.IServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace FRS.Implementation
{
    /// <summary>
    /// Type Registration for Implemention 
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Implementation
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            UnityConfig.UnityContainer = unityContainer;
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<IMenuRightsService, MenuRightsService>();
            unityContainer.RegisterType<ILogger, LoggerService>();
            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            unityContainer.RegisterType<IWebApiAuthenticationService, WebApiAuthenticationService>();
            unityContainer.RegisterType<IRegisterUserService, RegisterUserService>();
            unityContainer.RegisterType<IClaimsSecurityService, ClaimsSecurityService>();
            unityContainer.RegisterType<IEmployeeService, EmployeeService>();
            unityContainer.RegisterType<ILoadService, LoadService>();
            unityContainer.RegisterType<ILoadMetaDataService, LoadMetaDetaService>();
            unityContainer.RegisterType<IFileContentService, FileContentService>();
            unityContainer.RegisterType<IMT940LoadService, MT940LoadService>();
            unityContainer.RegisterType<IUsersService, UsersService>();
            unityContainer.RegisterType<ILoadTypeService, LoadTypeService>();
            unityContainer.RegisterType<ICurrencyService, CurrencyService>();
            unityContainer.RegisterType<IStatusService, StatusService>();
            unityContainer.RegisterType<ISourceService, SourceService>();
        }
    }
}