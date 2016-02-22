using System.Linq;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.Common.DropDown;
using FRS.Models.ResponseModels;
using FRS.WebApi.ViewModels.MetaData;

namespace FRS.WebApi.Areas.Load.Controllers
{
    public class LoadMetaDataBaseController : ApiController
    {
        #region Private

        private readonly ILoadTypeService loadTypeService;
        

        public LoadMetaDataBaseController(ILoadTypeService loadTypeService)
        {
            this.loadTypeService = loadTypeService;
        }

        #endregion

        #region Public
        public BaseDataLoadMetaData Get()
        {
            return new BaseDataLoadMetaData
            {
                LoadTypes = loadTypeService.GetLoadTypes().Select(x => new DropDownModel{Id = x.Value, Name = x.Name})
            };
        }
        #endregion
    }
}
