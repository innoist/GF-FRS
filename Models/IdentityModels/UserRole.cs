using System.Collections.Generic;
using FRS.Models.MenuModels;

namespace FRS.Models.IdentityModels
{
    /// <summary>
    /// User Role
    /// </summary>
    public partial class UserRole
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        
        public virtual ICollection<MenuRight> MenuRights { get; set; }
    }
}
