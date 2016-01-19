using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FRS.Implementation.Identity;
using FRS.Interfaces.IServices;
using FRS.Models.IdentityModels;
using FRS.Models.MenuModels;
using FRS.Web.ViewModels.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FRS.Web.Controllers
{
    /// <summary>
    /// Menu Controller to load menu items
    /// </summary>
    public class MenuController : Controller
    {
        private readonly IMenuRightsService menuRightService;

        /// <summary>
        /// Menu Controller Constructure
        /// </summary>
        /// <param name="menuRightService"></param>
        public MenuController(IMenuRightsService menuRightService)
        {
            this.menuRightService = menuRightService;
        }

        ///// <summary>
        ///// Load Menu items with respect to roles
        ///// </summary>
        //[ChildActionOnly]
        //public ActionResult LoadMenu()
        //{

        //    var menuVm = new MenuViewModel();
        //    if (Session["CaresSideMenuModel"] == null)
        //    {
        //        //return View(new MenuViewModel());
        //        User user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByEmailAsync(User.Identity.Name).Result;
        //        IList<MenuRight> menuItems;
        //        if (user == null || user.Roles == null || (user.Roles != null && user.Roles.Count < 1))
        //        {
        //            return View(new MenuViewModel());
        //        }
        //        //  ReSharper disable PossibleNullReferenceException
        //        if (user.Roles.Any(roles => roles.Name == CaresApplicationRoles.SystemAdministrator))
        //        {
        //            menuItems = user.Roles.FirstOrDefault(roles => roles.Name == CaresApplicationRoles.SystemAdministrator).MenuRights.ToList();
        //            //menuItems = menuItems.OrderBy(x => x.Menu.SortOrder).ToList();
        //            //menuItems = user.Roles.FirstOrDefault(roles => roles.Name == CaresApplicationRoles.SystemAdministrator).MenuRights.OrderBy(menu => menu.Menu.SortOrder).ToList();
        //        }
        //        else if (user.Roles.Any(roles => roles.Name == CaresApplicationRoles.Admin))
        //        {
        //            menuItems = user.Roles.FirstOrDefault(roles => roles.Name == CaresApplicationRoles.Admin).MenuRights.OrderBy(menu => menu.Menu.SortOrder).ToList();
        //        }
        //        else
        //        {
        //            menuItems = user.Roles.FirstOrDefault().MenuRights.OrderBy(menu => menu.Menu.SortOrder).ToList();
        //        }
        //        // ReSharper disable once InconsistentNaming
        //        var menuList = menuItems.Where(menu => menu.Menu != null);
        //        menuVm = new MenuViewModel
        //        {
        //            MenuRights = menuItems,
        //            MenuHeaders = menuList.Where(menu => menu.Menu.IsRootItem).ToList()
        //        };

        //        Session["CaresSideMenuModel"] = menuVm;
        //        return View(menuVm);
        //    }
        //    var menuFromSession = Session["CaresSideMenuModel"] as MenuViewModel;
        //    return View(menuFromSession);
        //}

        /// <summary>
        /// User Manger for logged in user credientals
        /// </summary>
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //[ChildActionOnly]
        public ActionResult LoadMenu()
        {
            MenuViewModel menuVM = new MenuViewModel();
            string userName = HttpContext.User.Identity.Name;
            if (!String.IsNullOrEmpty(userName))
            {
                AspNetUser userResult = UserManager.FindByName(userName);
                if (userResult != null)
                {
                    var roles = userResult.AspNetRoles.ToList();
                    if (roles.Count > 0)
                    {
                        IList<MenuRight> menuItems = menuRightService.FindMenuItemsByRoleId(roles[0].Id).ToList();

                        //save menu permissions in session
                        string[] userPermissions = menuItems.Select(user => user.Menu.PermissionKey).ToArray();
                        Session["UserPermissionSet"] = userPermissions;

                        menuVM = new MenuViewModel
                        {
                            MenuRights = menuItems,
                            MenuHeaders = menuItems.Where(x => x.Menu.IsRootItem)
                        };
                    }
                }
            }
            return View(menuVM);
        }
    }
}