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
    public class LoadMetaDataBaseController : ApiController
    {
        #region Private

        private readonly ILoadMetaDataService loadMetaDataService = UnityWebActivator.Container.Resolve<ILoadMetaDataService>();
        #endregion

        #region Public
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
    }
}
