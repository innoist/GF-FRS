using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models.MT940CustomerStatementTransaction;
using FRS.WebApi.ViewModels.MT940CustomerStatementTransaction;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    public class MT940CustomerStatementTransactionController : ApiController
    {
        #region Private

        private readonly IMT940CustomerStatementTransactionService mt940CustomerStatementTransactionService;

        public MT940CustomerStatementTransactionController(IMT940CustomerStatementTransactionService mt940CustomerStatementTransactionService)
        {
            this.mt940CustomerStatementTransactionService = mt940CustomerStatementTransactionService;
        }

        #endregion

        #region Public

        

        #region Get
        [ApiException]
        [HttpGet]
        public MT940CustomerStatementTransactionModel Get(long id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var detail = mt940CustomerStatementTransactionService.GetTransaction(id).MapFromServerToClient();
            return detail;
        }

        [HttpGet]
        [Authorize]
        [ApiException]
        public MT940CustomerStatementTransactionLV Get([FromUri]MT940CustomerStatementTransactionSearchrequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = mt940CustomerStatementTransactionService.GetMt940SearchResponse(searchRequest);
            MT940CustomerStatementTransactionLV listViewModel = new MT940CustomerStatementTransactionLV
            {
                Data = response.Data.Select(x => x.MapFromServerToClient()).ToList(),
                FilteredCount = response.FilteredCount,
                TotalCount = response.TotalCount
            };

            return listViewModel;
        }

        #endregion

        #region Post

        

        #endregion

        #endregion
    }
}
