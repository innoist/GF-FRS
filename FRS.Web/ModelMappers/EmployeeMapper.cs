using FRS.Web.Models;

namespace FRS.Web.ModelMappers
{
    public static class EmployeeMapper
    {
        public static Employee CreateFromServerToClient(this FRS.Models.DomainModels.Employee source)
        {
            return new Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeName = source.EmployeeName,
                Designation = source.Designation
            };
        }
        public static FRS.Models.DomainModels.Employee CreateFromClientToServer(this Employee source)
        {
            return new FRS.Models.DomainModels.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeName = source.EmployeeName,
                Designation = source.Designation
            };
        }
    }
}