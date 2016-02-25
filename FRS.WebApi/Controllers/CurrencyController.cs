using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.WebApi.Models.Currency;

namespace FRS.WebApi.Controllers
{
    [Authorize]
    public class CurrencyController : ApiController
    {
        #region Private

        private readonly ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        #endregion
        
        #region Public

        #region Get
        [HttpGet]
        public IEnumerable<CurrencyModel> Get()
        {
            var currencies =
                currencyService.GetCurrencies()
                    .Select(x => new CurrencyModel {Name = x.Name, Sign = x.Sign, Value = x.Value}).ToList();
            
            return currencies;
        }
        #endregion

        #region Post
        #endregion

        #region Delete
        #endregion



        #endregion


    }
}