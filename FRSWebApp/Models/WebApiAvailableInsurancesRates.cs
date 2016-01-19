
namespace Cares.WebApp.Models
{
    /// <summary>
    /// Available Insurances
    /// </summary>
    public class WebApiAvailableInsurancesRates
    {

        /// <summary>
        /// Insurance Type ID
        /// </summary>
        public short InsuranceTypeId { get; set; }

        /// <summary>
        /// Insurance Type Name
        /// </summary>
        public string InsuranceTypeName { get; set; }
        /// <summary>
        /// Insurance Rate
        /// </summary>
        public double InsuranceRate { get; set; }
    }
}