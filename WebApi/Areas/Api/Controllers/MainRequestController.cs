using Cares.Interfaces.IServices;
using Cares.Models.ResponseModels;
using Cares.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Cares.WebApi.Areas.Api.Controllers
{
    public class MainRequestController : ApiController
    {
        #region Private

        private IWebApiAvailableRentalService WebApiAvailableRentalService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MainRequestController(IWebApiAvailableRentalService webApiAvailableRentalService)
        {
            if (webApiAvailableRentalService == null)
            {
                throw new ArgumentNullException("webApiAvailableRentalService");
            }
            WebApiAvailableRentalService = webApiAvailableRentalService;
        }
        #endregion
        #region Public
        /// <summary>
        /// Get Available Services with their price
        /// </summary>        
        public IEnumerable<WebApiAdditionalDriver> Post(BookingRequuest x)
        {
            GetAvailableServicesRequest request = new GetAvailableServicesRequest();
            return WebApiAvailableRentalService.GetAdditionalDriverWithRates(request.DomainKey, request.TarrifTypeCode);
        }
        #endregion
    }
}