using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.MT940Load;
using FRS.WebApi.ViewModels.ReconciledMapping;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    public class ReconciledMappingController : ApiController
    {
        #region Private

        private readonly IReconciledMappingService reconciledMappingService;
        #endregion

        #region Public

        public ReconciledMappingController(IReconciledMappingService reconciledMappingService)
        {
            this.reconciledMappingService = reconciledMappingService;
        }



        [HttpGet]
        [Authorize]
        [ApiException]
        public ReconciledMappingViewModel Get([FromUri]ReconciledMappingSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = reconciledMappingService.GetReconciledMappingSearchResponse(searchRequest);
            ReconciledMappingViewModel listViewModel = new ReconciledMappingViewModel
            {
                ReconciledMappingModels = response.Data.Select(x => x.MapFromServerToClient()).ToList(),
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

        
    }
}
