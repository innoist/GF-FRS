using System;
using System.Linq;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.ResponseModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.MetaData;
using FRS.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Areas.Load.Controllers
{
    public class LoadMetaDataController : ApiController
    {
        #region Private

        private readonly ILoadMetaDataService loadMetaDataService = UnityWebActivator.Container.Resolve<ILoadMetaDataService>();
        #endregion

        #region Public

        #region Get

        public BaseDataLoadMetaData Get()
        {
            BaseDataLoadMetaDataResponse response = loadMetaDataService.GetBaseDataResponse();
            BaseDataLoadMetaData baseData = new BaseDataLoadMetaData
            {
                LoadMetaDatas = response.LoadMetaDatas.Select(x => x.CreateFromServerToClient()).ToList(),
                LoadTypes = response.LoadTypes,
                Sources = response.Sources,
                Currencies = response.Currencies,
                Statuses = response.Statuses
            };
            return baseData;
        }

        #endregion

        #region Post
        //public LoadMetaData Post(LoadMetaData loadMetaData)
        //{
        //    if (loadMetaData == null || !ModelState.IsValid)
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        //    }
        //    if (loadMetaDataService != null)
        //    {
        //        try
        //        {
        //            return loadMetaDataService.SaveMetaData(loadMetaData.CreateFromClientToServer()).CreateFromServerToClient();
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
