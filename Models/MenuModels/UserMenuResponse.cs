using System.Collections.Generic;
using FRS.Models.IdentityModels;

namespace FRS.Models.MenuModels
{
    /// <summary>
    /// User Menu Response
    /// </summary>
    public class UserMenuResponse
    {
        public IList<MenuRight> MenuRights { get; set; }        

        public IList<Menu> Menus { get; set; }

        public IList<UserRole> Roles { get; set; }
    }
}
