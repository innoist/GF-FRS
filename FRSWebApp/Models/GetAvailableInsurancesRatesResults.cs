using System.Collections.Generic;

namespace Cares.WebApp.Models
{
    /// <summary>
    /// Available Insurances with Rates
    /// </summary>
    public class GetAvailableInsurancesRatesResults
    {
        /// <summary>
        /// List of Available Insurances with Rates
        /// </summary>
        public IEnumerable<WebApiAvailableInsurancesRates> ApiAvailableInsurances { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }
    }
}