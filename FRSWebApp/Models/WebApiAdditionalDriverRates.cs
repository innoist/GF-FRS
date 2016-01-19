
namespace Cares.WebApp.Models
{
    /// <summary>
    /// Additionla Driver rates
    /// </summary>
    public class WebApiAdditionalDriverRates
    {
        /// <summary>
        /// Tariff Type Code
        /// </summary>
        public string TariffTypeCode { get; set; }

        /// <summary>
        /// Additional Driver rate
        /// </summary>
        public double Rate { get; set; }
    }
}