using System.Collections.Generic;
using FRS.Models.IdentityModels;

namespace FRS.Models.DomainModels
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }

        public virtual ICollection<AspNetUser> Users { get; set; }
    }
}
