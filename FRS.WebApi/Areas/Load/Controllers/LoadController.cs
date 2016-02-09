using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using FRS.Interfaces.IServices;
using FRS.Models.Common;
using FRS.Models.ResponseModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.Load;
using FRS.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Areas.Load.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        //[HttpPost]
        //public Models.MetaData.LoadMetaData Post(Models.Load.LoadModel load)
        //{
        //    if (loadMetaData == null || !ModelState.IsValid)
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        //    }
        //    if (loadService != null)
        //    {
        //        try
        //        {
        //            return loadService.SaveLoad(load.CreateFromClientToServer()).CreateFromServerToClient();
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //    return null;
        //}

        #endregion

        #region Delete
        //[HttpDelete]
        //public bool Delete(long loadMetaDataId)
        //{
        //    if (loadMetaDataService != null)
        //    {
        //        try
        //        {
        //            loadMetaDataService.DeleteMetaData(loadMetaDataId);
        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        #endregion

        #endregion
    }
}