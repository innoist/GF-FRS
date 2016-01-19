using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FRS.Models.IdentityModels;

namespace FRS.Web.Models
{
    /// <summary>
    /// User Model being used to edit user 
    /// </summary>
    public class UserModelForEditUser
    {
        public string id { get; set; }

        [Display(Name = "User Email"), Required]
        public string UserEmail { get; set; }

        [Display(Name = "User Role"), Required]
        public string SelectedRole { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public IEnumerable<UserRole> Roles { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}