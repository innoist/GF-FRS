using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models.MetaData;
using FRS.WebApi.ViewModels.MetaData;
using FRS.WebBase.Mvc;
using FRS.WebBase.UnityConfiguration;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Areas.Load.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoadMetaDataController : ApiController
    {
        #region Private

        private readonly ILoadMetaDataService loadMetaDataService = UnityWebActivator.Container.Resolve<ILoadMetaDataService>();
        #endregion

        #region Public

        #region Get
        [ApiException]
        public LoadMetaData Get(long? id)
        {
            if (id == null || id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }


            var loadMetaData = loadMetaDataService.FindById((long)id);
            return loadMetaData.CreateFromServerToClient();
        }

        [HttpGet]
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
                LoadMetaDatas = response.LoadMetaDatas.Select(x => x.CreateFromServerToClient()).ToList(),
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
        public bool Post(Models.MetaData.LoadMetaData loadMetaData)
        {
            //HttpContext.Current.Session
            if (loadMetaData == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
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
                    return loadMetaDataService.SaveMetaData(temp);//.CreateFromServerToClient();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
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
