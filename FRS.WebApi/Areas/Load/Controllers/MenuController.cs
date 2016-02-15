using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using FRS.Interfaces.IServices;
using FRS.Models.MenuModels;
using FRS.WebBase.Mvc;
using FRS.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Areas.Load.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MenuController : ApiController
    {
        #region Private

        private readonly IMenuRightsService menuRightsService = UnityWebActivator.Container.Resolve<IMenuRightsService>();

        #endregion

        #region Constructor
        #endregion

        #region Public
        
        //[Authorize]
        [ApiException]
        public IEnumerable<MenuView> Get()
        {
            return menuRightsService.GetForRole();
        }
        
        #endregion
    }
}
