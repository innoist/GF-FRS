using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.ResponseModels;
using FRS.Web.ModelMappers;
using FRS.Web.Models;
using FRS.Web.Models.MT940Load;

namespace FRS.Web.Areas.Api.Controllers
{
    public class MT940LoadController : ApiController
    {

        #region Private

        private readonly ILoadService loadService;
        private readonly ILoadMetaDataService metaDataService;

        #endregion

        #region Constructor

        public MT940LoadController(ILoadService loadService, ILoadMetaDataService metaDataService)
        {
            this.loadService = loadService;
            this.metaDataService = metaDataService;
        }

        #endregion

        #region Public

        #region Get
        [HttpGet]
        public BaseDataMT940Load Get()
        {
            MT940LoadBaseDataResponse response = loadService.GetBaseDataResponse();
            BaseDataMT940Load baseData = new BaseDataMT940Load
            {
                Loads = response.Loads.Select(x => x.CreateFromServerToClient()).ToList(),
                LoadMetadataDropDown = response.LoadMetadataDropDown
            };
            return baseData;
        }

        #endregion

        #region Post
        [HttpPost]
        public bool Post(Load load)
        {
            //if (!ModelState.IsValid)
            //{
            //    throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            //}
            if (loadService != null)
            {
                try
                {
                    File.WriteAllBytes(@"D:\Ammar\Office Projects\GF-FRS\FRS.Web\Files\" + load.FileName , load.ImageUrlBytes);
                    return true;
                    //var loadToSave = load.CreateFromClientToServer();
                    //if (loadService.SaveLoad(loadToSave))
                    //{
                    //    return true;
                    //}
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        #endregion

        #region Delete
        public bool Delete(long loadId)
        {
            if (loadService != null)
            {
                try
                {
                    loadService.DeleteLoad(loadId);
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

        #region Get File Type

        public bool Get(long metaDataId)
        {
            return metaDataService.IsSourceFileType(metaDataId);
        }
        #endregion

        #endregion
    }
}