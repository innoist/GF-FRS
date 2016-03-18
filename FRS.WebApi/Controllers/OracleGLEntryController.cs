using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.OracleGlLoad;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    public class OracleGLEntryController : ApiController
    {
        #region Private

        private readonly IOracleGLEntryService oracleGlEntryService;

        #endregion

        #region Public

        public OracleGLEntryController(IOracleGLEntryService oracleGlEntryService)
        {
            this.oracleGlEntryService = oracleGlEntryService;
        }

        #region Get
        //[ApiException]
        //[HttpGet]
        //[Authorize]
        //public OracleGLLoadDetail Get(long id)
        //{
        //    if (id <= 0)
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        //    }
        //    var detail = oracleGlLoadService.GetOracleGlLoadDetailResponse(id);
        //    OracleGLLoadDetail model = new OracleGLLoadDetail
        //    {
        //        Load = detail.Load.CreateFromServerToClient(),
        //        OracleGlLoad = detail.OracleGlLoad.CreateFromServerToClient()
        //    };

        //    return model;
        //}

        [HttpGet]
        [Authorize]
        [ApiException]
        public OracleGLEntryLVModel Get([FromUri]OracleGLEntrySearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = oracleGlEntryService.GetOracleGLEntrySearchResponse(searchRequest);
            OracleGLEntryLVModel listViewModel = new OracleGLEntryLVModel
            {
                OracleGlEntries = response.Data.Select(x => x.CreateFromServerToClient()).ToList(),
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
