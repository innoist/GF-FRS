using System.Collections.Generic;

namespace Cares.WebApp.Models
{
    public class GetAdditionalDriverRatesResults
    {
        /// <summary>
        /// List of Additional Driver Rates
        /// </summary>
        public IEnumerable<WebApiAdditionalDriverRates> WebApiAdditionalDriverRates { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }
    }
}