using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;

namespace FRS.Implementation.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Private
        private readonly IEmployeeRepository employeeRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        #endregion

        #region Public
        public IEnumerable<Employee> LoadAll()
        {
            return employeeRepository.GetAll();
        }

        public bool SaveEmployee(Employee employee)
        {
            if (employee.EmployeeId > 0)
            {
                employeeRepository.Update(employee);
                employeeRepository.SaveChanges();
                return true;
            }
            employeeRepository.Add(employee);
            employeeRepository.SaveChanges();
            return true;
        }

        public void DeleteEmployee(long employeeId)
        {
            var dbRecord = employeeRepository.Find(employeeId);
            if (dbRecord != null)
            {
                employeeRepository.Delete(dbRecord);
                employeeRepository.SaveChanges();
            }
        }

        #endregion
    }
}
