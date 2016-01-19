using System.Collections.Generic;

namespace Cares.WebApp.Models
{
    /// <summary>
    /// Additional Services Requst Response for view 
    /// </summary>
    public class AdditionalServicesRequstResponse
    {
        public IEnumerable<WebApiAvailableInsurancesRates> WebApiAvailableInsurancesRates { get; set; }
        public IEnumerable<WebApiAdditionalDriverRates> WebApiAdditionalDriverRates { get; set; }
        public IEnumerable<WebApiAvailableChuffersRates> WebApiAvailableChuffersRates { get; set; }
    }
}