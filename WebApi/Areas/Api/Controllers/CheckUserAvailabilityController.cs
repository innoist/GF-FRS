using Cares.Implementation.Identity;
using Cares.Interfaces.IServices;
using Cares.Models.IdentityModels;
using Cares.Models.IdentityModels.ViewModels;
using Cares.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cares.WebApi.Areas.Api.Controllers
{    
    /// <summary>
    /// Register user controller
    /// </summary>
    [System.Web.Http.Authorize]
    public class CheckUserAvailabilityController : ApiController
    {
        #region Private
        private IRegisterUserService registerUserService;
        private ApplicationUserManager _userManager;
        /// <summary>
        /// User Manager 
        /// </summary>
        private ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            // ReSharper disable once UnusedMember.Local
            set { _userManager = value; }
        }
        /// <summary>
        /// Add User 
        /// </summary>
        private User AddUser(User user, RegisterViewModel model, out string error)
        {
            error = string.Empty;
            var result = UserManager.Create(user, model.ConfirmPassword);
            if (result.Succeeded)
            {
                var addUserToRoleResult = UserManager.AddToRole(user.Id, model.SelectedRole);
                if (!addUserToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Format("Failed to add user to role {0}",
                        model.SelectedRole));
                }
                return user;
            }
            error = result.Errors.FirstOrDefault();
            return null;
        }

        /// <summary>
        /// Send Email to User for confirmation
        /// </summary>
        private string SendEmailToUser(User user, RegisterViewModel model)
        {
            string tempCode = UserManager.GenerateEmailConfirmationTokenAsync(user.Id).Result;
            tempCode = HttpUtility.UrlEncode(tempCode);
            //UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            //string action = url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = tempCode });
            string action = @"/Account/ConfirmEmail?userId=" + user.Id + "&code=" + tempCode;

            //    action = action.Replace("/CaresWebApi", "");
            string completeAddress = ConfigurationManager.AppSettings["CaresSiteAddress"] + action;
            //email template text = ReplacementText
            string emailBody = GetEmailTemplate();
            //completeAddress = HttpUtility.UrlEncode(completeAddress);
            emailBody = emailBody.Replace("ReplacementText", completeAddress);

            UserManager.SendEmailSendGrid(model.Email, "Confirm your CaReS subscription!", emailBody);

            return "Success";
        }

        private string GetEmailTemplate()
        {            
            string emailHtml = File.ReadAllText(HttpContext.Current.Server.MapPath(@"\Content\emailTemplate.html"));
            return emailHtml;
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CheckUserAvailabilityController(IRegisterUserService registerUserService)
        {
            if (registerUserService == null)
            {
                throw new ArgumentNullException("registerUserService");
            }
            this.registerUserService = registerUserService;
        }
        #endregion
        #region Public
       
       


        //[ApiException]
        //[System.Web.Http.HttpPost]
        //public bool Get(GeneralRequest request)
        //{
        //    bool value= registerUserService.IsCompanyUrlAvailable(request.URL);
        //    return value;
        //}
      
        #endregion
    }
}