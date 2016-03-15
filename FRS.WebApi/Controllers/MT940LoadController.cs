using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.MetaData;
using FRS.WebApi.ViewModels.MT940Load;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    public class MT940LoadController : ApiController
    {
        #region Private

        private readonly IMT940LoadService mt940LoadService;
        #endregion

        #region Public

        public MT940LoadController(IMT940LoadService mt940LoadService)
        {
            this.mt940LoadService = mt940LoadService;
        }

        #region Get
        [ApiException]
        [HttpGet]
        [Authorize]
        public MT940LoadDetail Get(long id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var detail = mt940LoadService.GetMt940LoadDetail(id);
            MT940LoadDetail model = new MT940LoadDetail()
            {
                Load = detail.Load.CreateFromServerToClient(),
                LoadMetaData = detail.LoadMetaData.CreateFromServerToClient(),
                Mt940LoadModel = detail.Mt940Load.CreateFromServerToClient()
            };

            return model;
        }

        [HttpGet]
        [Authorize]
        [ApiException]
        public MT940LoadListViewModel Get([FromUri]MT940LoadSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = mt940LoadService.GetMt940SearchResponse(searchRequest);
            MT940LoadListViewModel listViewModel = new MT940LoadListViewModel
            {
                Mt940Loads = response.Data.Select(x => x.CreateFromServerToClient()).ToList(),
                FilteredCount = response.FilteredCount,
                TotalCount = response.TotalCount
            };

            return listViewModel;
        }

        #endregion

        #region Post

        [HttpPost]
        [Authorize]
        [ApiException]
        public IHttpActionResult Post(Models.MetaData.LoadMetaData loadMetaData)
        {
            //HttpContext.Current.Session
            //if (loadMetaData == null || !ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //if (loadMetaDataService != null)
            //{
            //    try
            //    {
            //        loadMetaData.CreatedBy = User.Identity.GetUserId();
            //        loadMetaData.ModifiedBy = User.Identity.GetUserId();
            //        loadMetaData.CreatedOn = DateTime.UtcNow;
            //        loadMetaData.ModifiedOn = DateTime.Now;
            //        var temp = loadMetaData.CreateFromClientToServer();
            //        return Json(loadMetaDataService.SaveMetaData(temp));//.CreateFromServerToClient();
            //    }
            //    catch (Exception e)
            //    {
            //        return InternalServerError(e);
            //    }
            //}
            return Json(true);
        }

        #endregion

        #endregion
    }
}
