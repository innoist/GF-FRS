using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models.Currency;
using FRS.WebBase.Mvc;

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
                    .Select(x => x.MapFromServerToClient()).ToList();
            
            return currencies;
        }
        
        [HttpGet]
        public CurrencyModel Get(int Id)
        {
            var currency = currencyService.GetCurrency(Id).MapFromServerToClient();
            return currency;
        }
        #endregion

        #region Post

        [HttpPost]
        [Authorize]
        [ApiException]
        public IHttpActionResult Post(CurrencyModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = false;
            try
            {
                result =  currencyService.SaveCurrency(model.MapFromClientToServer());
            }
            catch (Exception e)
            {
                InternalServerError(e);
            }
            return Json(result);
        }
        #endregion

        #region Delete
        #endregion



        #endregion


    }
}