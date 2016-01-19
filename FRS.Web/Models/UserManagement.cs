using System.ComponentModel.DataAnnotations;

namespace FRS.Web.Models
{
    /// <summary>
    /// User mgmt model in user mangement section
    /// </summary>
    public class UserManagement
    {
        [Display(Name = "User Email"), Required]
        public string UserEmail { get; set; }

        [Display(Name = "User Role"), Required]
        public string UserRole { get; set; }

        public long DomainKey { get; set; }
        public string Id { get; set; }

       [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}