using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> LoadAll();
        bool SaveEmployee(Employee employee);
        void DeleteEmployee(long employeeId);
    }
}
