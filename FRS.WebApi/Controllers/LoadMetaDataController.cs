using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.MetaData;
using FRS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace FRS.WebApi.Controllers
{
    public class LoadMetaDataController : ApiController
    {
        #region Private

        private readonly ILoadMetaDataService loadMetaDataService;
        #endregion

        #region Public

        public LoadMetaDataController(ILoadMetaDataService loadMetaDataService)
        {
            this.loadMetaDataService = loadMetaDataService;
        }

        #region Get
        [ApiException]
        [Authorize]
        public BaseDataLoadMetaData Get(long? id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            BaseDataLoadMetaDataResponse response = loadMetaDataService.GetBaseDataResponse(id);
            BaseDataLoadMetaData baseData = new BaseDataLoadMetaData
            {
                LoadTypes = response.LoadTypes,
                Sources = response.Sources,
                Currencies = response.Currencies,
                Statuses = response.Statuses
                
            };
            if (response.MetaData != null)
            {
                baseData.MetaData = response.MetaData.CreateFromServerToClient();
            }
            return baseData;
        }

        [HttpGet]
        [Authorize]
        [ApiException]
        public LoadMetaDataListViewModel Get([FromUri]LoadMetaDataSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = loadMetaDataService.SearchLoadMetaData(searchRequest);
            LoadMetaDataListViewModel listViewModel = new LoadMetaDataListViewModel
            {
                LoadMetaDatas = response.LoadMetaDatas.Select(x => x.CreateFromServerToClient()).ToList(),//.OrderBy(x=>x.Status).ThenBy(x=>x.ModifiedOn).ToList(),
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
            if (loadMetaData == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (loadMetaDataService != null)
            {
                try
                {
                    loadMetaData.CreatedBy = User.Identity.GetUserId();
                    loadMetaData.ModifiedBy = User.Identity.GetUserId();
                    loadMetaData.CreatedOn = DateTime.UtcNow;
                    loadMetaData.ModifiedOn = DateTime.Now;
                    var temp = loadMetaData.CreateFromClientToServer();
                    return Json(loadMetaDataService.SaveMetaData(temp));//.CreateFromServerToClient();
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            return Json(true);
        }

        #endregion

        #region Delete
        [ApiException]
        public bool Delete(long loadMetaDataId)
        {
            if (loadMetaDataService != null)
            {
                try
                {
                    loadMetaDataService.DeleteMetaData(loadMetaDataId);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        #endregion

        #endregion
    }
}
