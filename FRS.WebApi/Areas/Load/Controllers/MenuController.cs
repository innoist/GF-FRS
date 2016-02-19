using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.MenuModels;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Areas.Load.Controllers
{
    public class MenuController : ApiController
    {
        #region Private

        private readonly IMenuRightsService menuRightsService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuController(IMenuRightsService menuRightsService)
        {
            if (menuRightsService == null)
            {
                throw new ArgumentNullException("menuRightsService");
            }

            this.menuRightsService = menuRightsService;
        }

        #endregion

        #region Public
        
        [Authorize]
        [ApiException]
        public IEnumerable<MenuView> Get()
        {
            if (HttpContext.Current.Cache["Menu"] == null)
            {
                HttpContext.Current.Cache["Menu"] = menuRightsService.GetForRole();
            }

            return HttpContext.Current.Cache["Menu"] as List<MenuView>;
        }
        
        #endregion
    }
}
