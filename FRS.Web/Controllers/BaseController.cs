using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;
using Cares.Commons;
using FRS.Interfaces.IServices;
using Microsoft.Practices.Unity;

namespace FRS.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Private

        [Dependency]
        public IMenuRightsService MenuRightService { get; set; }

        #endregion
        #region Protected

        // GET: Base
        protected override async void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            SetUserDetail();
        }

        #endregion
        #region Public

        /// <summary>
        /// Set User Detail In Session
        /// </summary>
        private void SetUserDetail()
        {
            Claim domainKeyClaim = ClaimHelper.GetClaimToString(CaresUserClaims.UserDomainKey);
            if (domainKeyClaim != null)
            {
                return;
            }
        }

        #endregion
    }
}