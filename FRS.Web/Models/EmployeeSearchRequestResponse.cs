using System.Collections.Generic;

namespace FRS.Web.Models
{
    public class EmployeeSearchRequestResponse
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}