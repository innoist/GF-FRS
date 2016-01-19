using Cares.Models.IdentityModels;
using Cares.Models.IdentityModels.ViewModels;
using FRS.Implementation.Identity;
using FRS.Models.IdentityModels;
using FRS.Web.Models;
using FRS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DomainModels=Cares.Models.DomainModels;
using Cares.Commons;
using Microsoft.Practices.Unity;

namespace Cares.Web.Controllers
{

    /// <summary>
    /// User Management Controller 
    /// </summary>
    [SiteAuthorize( PermissionKey = "**ThisMakesNoSenseIKnow**")]
    public class UsersAdminController : Controller
    {
        public UsersAdminController()
        {
        }
        private ApplicationUserManager _userManager;

        //[Dependency]
        //public IEmployeeService employeeService
        //{
        //    get;
        //    set;

        //} 
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        
        /// <summary>
        /// Get All Users
        /// </summary>
        private IEnumerable<AspNetUser> GetAllUsers()
        {
            var domainKeyClaim = ClaimHelper.GetClaimToString(CaresUserClaims.UserDomainKey);
            if (domainKeyClaim == null)
            {
                throw new InvalidOperationException("Domain-Key claim not found!");
            }
            var domainkey = System.Convert.ToInt64(domainKeyClaim.Value);
            return UserManager.Users.ToList();
            //return UserManager.Users.Where(user => user.UserDomainKey == domainkey).ToList();
        } 

        /// <summary>
        /// List downs all users for domain key
        /// </summary>
        public ActionResult Index()
        {
            var allUsers = GetAllUsers().ToList();
            //var allEmployees = employeeService.GetAllForUser(new List<long?>()).Select(emp => emp.CreateFrom());
            var model = allUsers.Select(user => new UserManagement
            {
                DomainKey = user.UserDomainKey,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UserEmail = user.Email,
                UserRole = user.AspNetRoles.FirstOrDefault() != null ? user.AspNetRoles.FirstOrDefault().Name : string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
            //ViewBag.Employees = allEmployees;
            return View(model);
        }

        /// <summary>
        /// Creates new user model 
        /// </summary>
        public ActionResult CreateUser()
        {
            var roles = RoleManager.Roles.Where(role => role.Name != "SystemAdministrator").ToList();
            var allUsers = GetAllUsers().ToList();
            //var allEmployees = employeeService.GetAllForUser(allUsers.Select(user => user.EmployeeId).ToList()).Select(emp => emp.CreateFrom());
            ViewBag.UserRoles = roles;
            //ViewBag.Employees = allEmployees;
            return View(new UserManagement());
        }

        /// <summary>
        /// Deletes user for User id
        /// </summary>
        public ActionResult DeleteUser(string Id)
        {
            var user = UserManager.FindById(Id);
            if (user == null)
                throw new InvalidOperationException("User does not exists!");
            UserManager.Delete(user);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Adds new user 
        /// </summary>
        [HttpPost]
        public ActionResult CreateUser(UserManagement model)
        {
            if (model == null)
                throw new InvalidOperationException("User Does not exists!");
            var domainKeyClaim = ClaimHelper.GetClaimToString(CaresUserClaims.UserDomainKey);
            if (domainKeyClaim == null)
            {
                throw new InvalidOperationException("Domain-Key claim not found!");
            }
            var domainkey = System.Convert.ToInt64(domainKeyClaim.Value);
            // Creating employee 
            //DomainModels.Employee emp = employeeService.CreateEmployeeWithUser(model.UserEmail);

            var user = new AspNetUser
            {
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserEmail,
                Email = model.UserEmail,
                UserDomainKey = domainkey,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var status = AddUserToUserManager(user, model);
            if (status == null)
                return RedirectToAction("Index");

            var roles = RoleManager.Roles.Where(role => role.Name != "SystemAdministrator").ToList();
            var allUsers = GetAllUsers().ToList();
            //var allEmployees = employeeService.GetAllForUser(allUsers.Select(usr => usr.EmployeeId).ToList()).Select(employee => employee.CreateFrom());
            ViewBag.UserRoles = roles;
            //ViewBag.Employees = allEmployees;
            ViewBag.UserError = status;
            return View(new UserManagement());
        }

        /// <summary>
        /// Add User 
        /// </summary>
        private string AddUserToUserManager(AspNetUser user, UserManagement model)
        {
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                var addUserToRoleResult = UserManager.AddToRole(user.Id, model.UserRole);
                if (!addUserToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Format("Failed to add user to role {0}",
                        model.UserRole));
                }
            }
            return result.Errors.FirstOrDefault();
        }

        /// <summary>
        /// Edits user for user id 
        /// </summary>
        public ActionResult EditUser(string id)
        {
            if (id == null)
                return View();
            var allUsers = GetAllUsers().ToList();
            var roles = RoleManager.Roles.Where(role => role.Name != "SystemAdministrator").ToList();
            var user = UserManager.FindById(id);
            List<long?> employeeIds = allUsers.Select(usr => usr.EmployeeId).ToList();
            if (employeeIds.Count > 0 && user.EmployeeId.HasValue)
            {
                employeeIds.Remove(user.EmployeeId);
            }

            employeeIds.Remove(user.EmployeeId);
            
            var userRole = user.AspNetRoles.FirstOrDefault();
            return View(new UserModelForEditUser
            {
                Roles = roles,
                SelectedRole = userRole != null ? userRole.Id : string.Empty,
                UserEmail = user.Email,
                id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        /// <summary>
        /// Updates edited user
        /// </summary>
        [HttpPost]
        public ActionResult EditUser(UserModelForEditUser model)
        {
            var selectedRole = RoleManager.Roles.FirstOrDefault(role => role.Id == model.SelectedRole).Name;
            var user = UserManager.FindById(model.id);
            //user.EmployeeId = model.EmployeeId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var userRole = user.AspNetRoles.FirstOrDefault();
            if (userRole != null)
            {
                UserManager.RemoveFromRole(model.id, userRole.Name);
            }
            UserManager.AddToRole(model.id, selectedRole);
            UserManager.Update(user);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// View user details
        /// </summary>
        public ActionResult ViewUserDetail(string id)
        {
            var user = UserManager.FindById(id);
            if (user == null)
                throw new InvalidOperationException("User Not found!");
            var userModel = new UserModelForEditUser
            {
                id = user.Id,
                UserEmail = user.Email,
                PhoneNumber = user.PhoneNumber,
                SelectedRole = user.AspNetRoles.FirstOrDefault().Name
            };
            return View(userModel);
        }
    }
}
