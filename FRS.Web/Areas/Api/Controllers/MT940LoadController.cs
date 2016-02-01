using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.Common;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;
using FRS.Web.ModelMappers;
using FRS.Web.Models.MT940Load;
using Load = FRS.Web.Models.Load;

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
        public bool Post(Load load)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            if (loadService != null)
            {
                try
                {
                    //return true;
                    var loadToSave = load.CreateFromClientToServer();
                    loadToSave.MT940Load = new MT940Load
                    {
                        FileName = load.FileName,
                        FileExtension = load.FileExtension,
                        FileContent = new FileContent
                        {
                            FileContentBase64 = load.Attachment,
                            Description = "",
                        }
                    };
                    if (loadService.SaveLoad(loadToSave))
                    {
                        return true;
                    }
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

        public LoadMetaDataForLoad Get(long metaDataId)
        {
            return metaDataService.IsLoadTypeMT940(metaDataId);
        }
        #endregion

        #endregion
    }
}