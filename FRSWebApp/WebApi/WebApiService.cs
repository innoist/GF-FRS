using Cares.WebApp.Models;
using Cares.WebApp.Resources;
using Cares.WebApp.WepApiInterface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cares.WebApp.WebApi
{
    /// <summary>
    /// Web Api Service
    /// </summary>
    public sealed class WebApiService : ApiService, IWebApiService
    {
        #region Uris
        /// <summary>
        ///Uris for different APIs
        /// </summary>
        private string GetOperationWorkplaceListUri
        {
            get
            {
                return ApiResource.WebApiBaseAddress + ApiResource.GetOperationWorkplace;
            }
        }
        private string GetHireGroupListUri
        {
            get
            {
                return ApiResource.WebApiBaseAddress + ApiResource.GetHireGroup;
            }
        }
        private string GetAvailableInsurancesRateUri
        {
            get
            {
                return ApiResource.WebApiBaseAddress + ApiResource.GetAvailableInsuranceRates;
            }
        }
        private string GetAvailableChauffersRatesUri
        {
            get
            {
                return ApiResource.WebApiBaseAddress + ApiResource.GetAvailableChauffersRates;
            }
        }
        private string GetAdditionalDriverChargeUri
        {
            get
            {
                return ApiResource.WebApiBaseAddress + ApiResource.GetAdditionalDriverCharge;
            }
        }
        private string SetBookingMaineUri
        {
            get
            {
                return ApiResource.WebApiBaseAddress + ApiResource.SetBookingMain;
            }
        }
       

        #endregion
        #region Private
        /// <summary>
        /// Get Operation Workplace List 
        /// </summary>
        private async Task<GetOperationWorkplaceResult> GetOperationWorkplaceListAsync(long domainKey)
        {
            GetOperationWorkplaceRequest request = new GetOperationWorkplaceRequest { DomainKey = domainKey };

            string requestContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await PostHttpRequestAsync(requestContents, new Uri(GetOperationWorkplaceListUri)).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                string stringContents = await responseMessage.Content.ReadAsStringAsync();
                return new GetOperationWorkplaceResult
                {
                    OperationWorkplaces = this.CreateResultForOperationWorkplaceListRequest(stringContents)
                };
            }
            {
                string errorString = await responseMessage.Content.ReadAsStringAsync();
                return new GetOperationWorkplaceResult
                {
                    Error = errorString
                };
            }
        }

        /// <summary>
        /// Get Available HireGroup
        /// </summary>
        private async Task<GetHireGroupResult> GetHireGroupAsync(GetHireGroupRequest request)
        {
            string requestContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await PostHttpRequestAsync(requestContents, new Uri(GetHireGroupListUri)).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            { 
                string stringContents = await responseMessage.Content.ReadAsStringAsync();
                return new GetHireGroupResult
                {
                    AvailableHireGroups = CreateResultForHireGroupsListRequest(stringContents)
                };

            }
            else
            {
                string errorString = await responseMessage.Content.ReadAsStringAsync();
                return new GetHireGroupResult
                {
                    Error = errorString
                };
            }
        }


        /// <summary>
        /// Create Results for Operation Workplace
        /// </summary>
        private IList<WebApiOperationWorkplace> CreateResultForOperationWorkplaceListRequest(string stringContents)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<WebApiOperationWorkplace>>(stringContents);
        }

        /// <summary>
        /// Create Results for Hire Group
        /// </summary>
        private IList<WebApiHireGroup> CreateResultForHireGroupsListRequest(string stringContents)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<WebApiHireGroup>>(stringContents);
        }

        #endregion
        #region Public


        /// <summary>
        /// Get Operation Workplace List
        /// </summary>
        public GetOperationWorkplaceResult GetOperationWorkplaceList(long domainKey)
        {
            return GetOperationWorkplaceListAsync(domainKey).Result;
        }

        /// <summary>
        /// Get Hire Group List
        /// </summary>
        public GetHireGroupResult GetHireGroupList(GetHireGroupRequest request)
        {
            return GetHireGroupAsync(request).Result;
        }



        /// <summary>
        /// Get Available Insurances Rates 
        /// </summary> 
        public GetAvailableInsurancesRatesResults GetAvailableInsurancesRates(WebApiRequest webApiRequest)
        {
            return GetInsurancesRatesAsync(webApiRequest).Result;
        }
        private IEnumerable<WebApiAvailableInsurancesRates> CreateResultForInsurancesListRequest(string stringContents)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<WebApiAvailableInsurancesRates>>(stringContents);
        }
        private async Task<GetAvailableInsurancesRatesResults> GetInsurancesRatesAsync(WebApiRequest request)
        {
            string requestContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await PostHttpRequestAsync(requestContents, new Uri(GetAvailableInsurancesRateUri)).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                string stringContents = await responseMessage.Content.ReadAsStringAsync();
                IEnumerable<WebApiAvailableInsurancesRates> webApiAvailableInsurancesRateses = CreateResultForInsurancesListRequest(stringContents);
                return new GetAvailableInsurancesRatesResults
                {
                    ApiAvailableInsurances = webApiAvailableInsurancesRateses
                };
            }
            {
                string errorString = await responseMessage.Content.ReadAsStringAsync();
                return new GetAvailableInsurancesRatesResults
                {
                    Error = errorString
                };
            }
        }

       

        /// <summary>
        /// Get Available Chauffers with chrage rates
        /// </summary>
        public GetAvailableChauffersRatesResults GetAvailableChauffersRates(WebApiRequest webApiRequest)
        {
            return GetChauffersRatesAsync(webApiRequest).Result;
        }
        private async Task<GetAvailableChauffersRatesResults> GetChauffersRatesAsync(WebApiRequest request)
        {

            string orderContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await PostHttpRequestAsync(orderContents, new Uri(GetAvailableChauffersRatesUri)).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                string stringContents = await responseMessage.Content.ReadAsStringAsync();
                return new GetAvailableChauffersRatesResults
                {
                    ApiAvailableChuffersRates = CreateResultForChauffersListRequest(stringContents)
                    //for service rate ,decide to see result of web service
                };

            }
            else
            {
                string errorString = await responseMessage.Content.ReadAsStringAsync();
                return new GetAvailableChauffersRatesResults
                {
                    Error = errorString
                };
            }
        }
        private IEnumerable<WebApiAvailableChuffersRates> CreateResultForChauffersListRequest(string stringContents)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<WebApiAvailableChuffersRates>>(stringContents);
        }




        /// <summary>
        /// Get Additional Driver Rates
        /// </summary>
        public GetAdditionalDriverRatesResults GetAdditionalDriverRates(WebApiRequest webApiRequest)
        {
            return GetAdditioanalDriverRatesAsync(webApiRequest).Result;
        }
        private async Task<GetAdditionalDriverRatesResults> GetAdditioanalDriverRatesAsync(WebApiRequest request)
        {
            string orderContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await PostHttpRequestAsync(orderContents, new Uri(GetAdditionalDriverChargeUri)).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                string stringContents = await responseMessage.Content.ReadAsStringAsync();
                IEnumerable<WebApiAdditionalDriverRates> resultForAdditionalDriverListRequest = CreateResultForAdditionalDriverListRequest(stringContents);
                return new GetAdditionalDriverRatesResults
                {
                    WebApiAdditionalDriverRates = resultForAdditionalDriverListRequest
                };
            }
                string errorString = await responseMessage.Content.ReadAsStringAsync();
                return new GetAdditionalDriverRatesResults
                {
                    Error = errorString
                };
        }
        private IEnumerable<WebApiAdditionalDriverRates> CreateResultForAdditionalDriverListRequest(string stringContents)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<WebApiAdditionalDriverRates>>(stringContents);
        }


        /// <summary>
        /// To add the booking on server
        /// </summary>
        public bool SaveBookingMain(WebApiBookingMainRequest bookingMain)
        {
           return BookingMainAsync(bookingMain).Result;
        }
        private async Task<bool> BookingMainAsync(WebApiBookingMainRequest webApiRequest)
        {
            string orderContents = Newtonsoft.Json.JsonConvert.SerializeObject(webApiRequest);
            HttpResponseMessage responseMessage = await PostHttpRequestAsync(orderContents, new Uri(SetBookingMaineUri)).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                await responseMessage.Content.ReadAsStringAsync();
                return true;
            }
            await responseMessage.Content.ReadAsStringAsync();
            return false;
        }

        #endregion
    }
}
