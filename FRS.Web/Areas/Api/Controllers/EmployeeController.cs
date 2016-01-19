using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Web.ModelMappers;
using FRS.Interfaces.IServices;
using FRS.Web.Models;


namespace FRS.Web.Areas.Api.Controllers
{
    public class EmployeeController : ApiController
    {
        #region Private
        private readonly IEmployeeService employeeService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        #endregion

        #region Public
        public EmployeeSearchRequestResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            if (employeeService != null)
            {
                var ret = new EmployeeSearchRequestResponse
                {
                    Employees = employeeService.LoadAll().Select(x=>x.CreateFromServerToClient())
                };
                return ret;
            }
            return null;
        }

        public bool Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            if (employeeService != null)
            {
                try
                {
                    var employeeToSave = employee.CreateFromClientToServer();
                    if (employeeService.SaveEmployee(employeeToSave))
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool Delete(long employeeId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            if (employeeService != null)
            {
                try
                {
                    employeeService.DeleteEmployee(employeeId);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        #endregion
    }
}