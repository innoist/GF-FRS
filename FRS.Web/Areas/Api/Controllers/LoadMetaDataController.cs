using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Web.ModelMappers;
using FRS.Web.Models;

namespace FRS.Web.Areas.Api.Controllers
{
    public class LoadMetaDataController : ApiController
    {
        #region Private

        private readonly ILoadMetaDataService loadMetaDataService;

        #endregion

        #region Constructor

        public LoadMetaDataController(ILoadMetaDataService loadMetaDataService)
        {
            this.loadMetaDataService = loadMetaDataService;
        }

        #endregion

        #region Public

        #region Get
        #endregion


        #region Post

        public LoadMetaDataResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var response = new LoadMetaDataResponse
            {
                LoadMetaDatas = loadMetaDataService.GetAll().Select(x => x.CreateFromServerToClient())
            };
            return response;
        }
        #endregion

        #endregion

    }
}