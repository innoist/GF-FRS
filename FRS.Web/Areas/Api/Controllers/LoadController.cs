using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Web.ModelMappers;
using FRS.Web.Models;

namespace FRS.Web.Areas.Api.Controllers
{
    public class LoadController : ApiController
    {

        #region Private

        private readonly ILoadService loadService;

        #endregion

        #region Constructor

        public LoadController(ILoadService loadService)
        {
            this.loadService = loadService;
        }

        #endregion

        #region Public

        #region Get

        public LoadSearchRequestResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var response = new LoadSearchRequestResponse
            {
                Loads = loadService.GetAll().Select(x => x.CreateFromServerToClient())
            };
            return response;
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
                    var loadToSave = load.CreateFromClientToServer();
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

        #endregion
    }
}