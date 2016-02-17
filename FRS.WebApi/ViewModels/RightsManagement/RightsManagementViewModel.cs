using System.Collections.Generic;
using FRS.WebApi.Models;

namespace FRS.WebApi.ViewModels.RightsManagement
{
    /// <summary>
    /// Rights Management
    /// </summary>
    public class RightsManagementViewModel
    {
        public List<MenuRight> Rights { get; set; }
        
        public string SelectedRoleId { get; set; }

        public List<Models.UserRole> Roles { get; set; }
    }
}