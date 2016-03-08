using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.MT940Balance;
using FRS.WebApi.ViewModels.MT940CustomerStatementTransaction;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    public class MT940BalanceController : ApiController
    {
        #region Private

        private readonly IMT940BalanceService mt940BalanceService;

        public MT940BalanceController(IMT940BalanceService mt940BalanceService)
        {
            this.mt940BalanceService = mt940BalanceService;
        }

        #endregion

        #region Public

        #region Get

        [HttpGet]
        [Authorize]
        [ApiException]
        public MT940BalanceListViewModel Get([FromUri]MT940BalanceSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = mt940BalanceService.GetMt940BalanceSearchResponse(searchRequest);
            MT940BalanceListViewModel listViewModel = new MT940BalanceListViewModel
            {
                Data = response.Data.Select(x => x.MapFromServerToClient()).ToList(),
                FilteredCount = response.FilteredCount,
                TotalCount = response.TotalCount,
                Currencies = response.DropDown.ToList()
            };

            return listViewModel;
        }

        #endregion

        #region Post

        

        #endregion

        #endregion
    }
}
