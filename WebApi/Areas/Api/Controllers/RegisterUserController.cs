using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Cares.ExceptionHandling;
using Cares.Implementation.Identity;
using Cares.Interfaces.IServices;
using Cares.Models.IdentityModels;
using Cares.Models.IdentityModels.ViewModels;
using Cares.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Cares.WebApi.Areas.Api.Controllers
{    
    /// <summary>
    /// Register user controller
    /// </summary>
   [System.Web.Http.Authorize]
    public class RegisterUserController : ApiController
    {
        #region Private
        private readonly IRegisterUserService _registerUserService;
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
            SendEmailTo(model.Email, "Confirm your CaReS subscription!", emailBody);
         //   UserManager.SendEmailAsync(model.Email, "Confirm your CaReS subscription!", emailBody);

            return "Success";
        }


        private string GetEmailTemplate()
        {
            string emailHtml = "";
            try
            {
                string path = ConfigurationManager.AppSettings["ApplicationHostingPath"] + @"\Content\emailTemplate.html";
                if (File.Exists(path))
                {
                     emailHtml = File.ReadAllText(path);
                    return emailHtml;
                }
            }
            catch (Exception)
            {
                emailHtml= "Email Body not found!";
                return emailHtml;
            }
            return emailHtml;
           
        }
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public RegisterUserController(IRegisterUserService registerUserService)
        {
            if (registerUserService == null)
            {
                throw new ArgumentNullException("registerUserService");
            }
            this._registerUserService = registerUserService;
        }
        #endregion
        #region Public
       
       

        /// <summary>
        /// Register user using web api
        /// </summary>
        [ApiException]        
        public string Post(RegisterViewModel model)
        {
            model.SelectedRole = "Admin";
            if (ModelState.IsValid)
            {
                double userDomainKey = _registerUserService.GetMaxUserDomainKey();
                var user = new User
                {
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email, 
                    Email = model.Email, 
                    UserDomainKey = Convert.ToInt64(userDomainKey)+1   //giving the Max+1 domain key
                };
                string errorString;
                User addedUser = AddUser(user, model, out errorString);
                if (addedUser != null)
                {
                    //_registerUserService.AddLicenseDetail(model, user.UserDomainKey);
                    _registerUserService.SaveUserDetails(addedUser, model);
                    return SendEmailToUser(addedUser, model);
                }
                if (!string.IsNullOrEmpty(errorString))
                {
                    throw new CaresException(errorString);
                }                
            }
            throw new CaresException("Failed to register!");
        }

        
        /// <summary>
        /// Confirm User's email address
        /// </summary>
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
               
            }
            // ReSharper disable once UnusedVariable
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return null;
        }


        /// <summary>
        /// Send Email
        /// </summary>
        public string SendEmailTo(string email, string subject, string body)
        {

            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];
            string fromDisplayName = ConfigurationManager.AppSettings["FromDisplayNameA"];
            //string cc = ConfigurationManager.AppSettings["CC"];
            //string bcc = ConfigurationManager.AppSettings["BCC"];

            //Getting the file from config, to send
            var oEmail = new MailMessage
            {
                From = new MailAddress(fromAddress, fromDisplayName),
                Subject = subject,
                IsBodyHtml = true,
                Body = body,
                Priority = MailPriority.High
            };
            oEmail.To.Add(email);
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string enableSsl = ConfigurationManager.AppSettings["EnableSSL"];
            var client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort))
            {
                EnableSsl = enableSsl == "1",
                Credentials = new NetworkCredential(fromAddress, fromPwd)
            };
             client.Send(oEmail);
            return "Success";
        }


        #endregion
    }
}