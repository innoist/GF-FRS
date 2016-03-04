using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.Common;
using FRS.Models.ResponseModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.Load;
using FRS.WebBase.UnityConfiguration;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Areas.Load.Controllers
{
    public class LoadController : ApiController
    {
        #region Private

        private readonly ILoadService loadService = UnityWebActivator.Container.Resolve<ILoadService>();
        private readonly ILoadMetaDataService metaDataService = UnityWebActivator.Container.Resolve<ILoadMetaDataService>();
        #endregion

        #region Public

        #region Get
        [HttpGet]
        public BaseDataLoad Get()
        {
            MT940LoadBaseDataResponse response = loadService.GetBaseDataResponse();
            BaseDataLoad baseData = new BaseDataLoad
            {
                Loads = response.Loads.Select(x=> x.CreateFromServerToClient()).ToList(),
                LoadMetadataDropDown = response.LoadMetadataDropDown.ToList()
            };
            return baseData;
        }

        #endregion

        #region Get File Type

        public LoadMetaDataForLoad Get(long metaDataId)
        {
            return metaDataService.IsLoadTypeMT940(metaDataId);
        }
        #endregion

        #region Post
        [HttpPost]
        public IHttpActionResult Post(Models.Load.LoadModel load)
        {
            if (load == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = false;
            if (loadService != null)
            {
                try
                {
                    result = loadService.SaveLoad(load.CreateFromClientToServer(User.Identity.GetUserId()));
                }
                catch (Exception e)
                {
                    return BadRequest("Invalid Request");
                }
            }
            return Json(result);
        }

        #endregion

        #endregion
    }
}