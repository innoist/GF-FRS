using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class MT940BalanceService : IMT940BalanceService
    {
        #region Private

        private readonly IMT940BalanceRepository mt940BalanceRepository;
        private readonly ICurrencyRepository currencyRepository;

        

        #endregion

        #region Constructor
        public MT940BalanceService(IMT940BalanceRepository mt940BalanceRepository, ICurrencyRepository currencyRepository)
        {
            this.mt940BalanceRepository = mt940BalanceRepository;
            this.currencyRepository = currencyRepository;
        }

        #endregion

        public SearchTemplateResponse<MT940Balance> GetMt940BalanceSearchResponse(MT940BalanceSearchRequest searchRequest)
        {
            var response =  mt940BalanceRepository.GetMt940BalanceSearchResponse(searchRequest);
            response.DropDown = currencyRepository.GetCurrenciesDropDown();

            return response;
        }
    }
}
