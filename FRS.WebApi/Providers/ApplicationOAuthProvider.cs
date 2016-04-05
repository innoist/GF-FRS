using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using FRS.Implementation.Identity;
using FRS.Models.IdentityModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using FRS.Interfaces.IServices;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Providers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        private readonly IUnityContainer _container;

        public IClaimsSecurityService ClaimsSecurityService { get; set; }

        public ApplicationOAuthProvider(string publicClientId, IUnityContainer container)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _publicClientId = publicClientId;
            _container = container;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {


                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                var userName = context.UserName;
                if (context.UserName.Contains("@"))
                {
                    var aspNetUser = await userManager.FindByEmailAsync(context.UserName);
                    if (aspNetUser == null)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }
                    userName = aspNetUser.UserName;
                }
                AspNetUser user = await userManager.FindAsync(userName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                // Get Form posted values
                // Extract TimeZoneOffset.
                var data = await context.Request.ReadFormAsync();
                var timeZoneCookie = data["userTimeZone"];
                var timeZoneOffSetValue = TimeSpan.FromMinutes(0);
                if (!string.IsNullOrEmpty(timeZoneCookie))
                {

                    double offsetMinutes;
                    if (double.TryParse(timeZoneCookie, out offsetMinutes))
                    {
                        timeZoneOffSetValue = TimeSpan.FromMinutes(offsetMinutes);
                    }
                }

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                    OAuthDefaults.AuthenticationType);

                ClaimsSecurityService = _container.Resolve<IClaimsSecurityService>();
                if (ClaimsSecurityService == null)
                {
                    throw new ArgumentException("ClaimsSecurityService");
                }

                ClaimsSecurityService.AddClaimsToIdentity(user.AspNetRoles.FirstOrDefault().Name, context.UserName,
                    user.Id, timeZoneOffSetValue, oAuthIdentity);

                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", context.ClientId ?? string.Empty
                    },
                    {
                        "userName", user.UserName
                    },
                    {
                        "userId", user.Id
                    },
                    {
                        "UserRole", user.AspNetRoles.FirstOrDefault().Name
                    }
                });


                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, props);
                context.Validated(ticket);
                HttpContext.Current.User = new ClaimsPrincipal(oAuthIdentity);
                // Make sure the Principal's are in sync
                Thread.CurrentPrincipal = HttpContext.Current.User;
                context.Request.Context.Authentication.SignIn(oAuthIdentity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}