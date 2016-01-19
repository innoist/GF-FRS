using System.Threading.Tasks;
using Cares.WebApp.Models;

namespace Cares.WebApp.WepApiInterface
{
    /// <summary>
    /// Web Api Service Interface
    /// </summary>
    public interface IWebApiService
    {

        /// <summary>
        /// Get Operation Workplace List
        /// </summary>
        GetOperationWorkplaceResult GetOperationWorkplaceList(long domainKey);

        /// <summary>
        /// Get Hire Group List
        /// </summary>
        GetHireGroupResult GetHireGroupList(GetHireGroupRequest request);

        /// <summary>
        /// Get Available Insurances Rates 
        /// </summary>
        GetAvailableInsurancesRatesResults GetAvailableInsurancesRates(WebApiRequest webApiRequest);
        GetAvailableChauffersRatesResults  GetAvailableChauffersRates(WebApiRequest webApiRequest);
        GetAdditionalDriverRatesResults    GetAdditionalDriverRates(WebApiRequest webApiRequest);

        /// <summary>
        /// To add the booking on server
        /// </summary>
        bool SaveBookingMain(WebApiBookingMainRequest bookingMain);


    }
}
