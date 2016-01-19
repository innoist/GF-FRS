using System.Collections.Generic;
using FRS.Models.IdentityModels;
using FRS.Web.Models;

namespace FRS.Web.ViewModels.RightsManagement
{
    /// <summary>
    /// Rights Management
    /// </summary>
    public class RightsManagementViewModel
    {
        public List<MenuRight> Rights { get; set; }
        
        public string SelectedRoleId { get; set; }

        public List<UserRole> Roles { get; set; }
    }
}