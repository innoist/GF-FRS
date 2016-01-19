using System.Collections.Generic;

namespace Cares.WebApp.Models
{
    /// <summary>
    /// Available Chuauffers with Rates
    /// </summary>
    public class GetAvailableChauffersRatesResults
    {
        /// <summary>
        /// List of chauffrs and their rates
        /// </summary>
        public IEnumerable<WebApiAvailableChuffersRates> ApiAvailableChuffersRates { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }
    }
}