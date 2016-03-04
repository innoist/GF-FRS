using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.MT940CustomerStatement;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    public class CustomerStatementController : ApiController
    {
        #region Private

        private readonly IMT940CustomerStatementService mt940CustomerStatementService;
        #endregion

        #region Public

        public CustomerStatementController(IMT940CustomerStatementService mt940CustomerStatementService)
        {
            this.mt940CustomerStatementService = mt940CustomerStatementService;
        }

        #region Get

        [HttpGet]
        [Authorize]
        [ApiException]
        public MT940CustomerStatementListViewModel Get([FromUri]MT940CustomerStatementSearchrequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = mt940CustomerStatementService.GetMt940SearchResponse(searchRequest);
            MT940CustomerStatementListViewModel listViewModel = new MT940CustomerStatementListViewModel
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
