using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models;
using FRS.WebApi.ViewModels.LoadStatus;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    [Authorize]
    public class LoadStatusController : ApiController
    {
        private readonly ILoadStatusService loadStatusService;

        #region Private


        public LoadStatusController(ILoadStatusService loadStatusService)
        {
            this.loadStatusService = loadStatusService;
        }

        #endregion
        
        #region Public

        #region Get
        [HttpGet]
        public IEnumerable<LoadStatusModel> Get()
        {
            var years =
                loadStatusService.GeLoadStatuses()
                    .Select(x => x.MapFromServerToClient()).ToList();
            
            return years;
        }
        
        [HttpGet]
        public LoadStatusViewModel Get(int ? Id)
        {
            var response = loadStatusService.GetLoadStatus(Id);
            LoadStatusViewModel model = new LoadStatusViewModel
            {
                Statuses = response.Statuses.ToList().Select(x=>x.MapFromServerToClient()).ToList(),
                LoadStatus = response.LoadStatus != null ? response.LoadStatus.MapFromServerToClient() : null
            };
            return model;
        }
        #endregion

        #region Post

        [HttpPost]
        [Authorize]
        [ApiException]
        public IHttpActionResult Post(LoadStatusModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = false;
            try
            {
                result =  loadStatusService.SaveLoadStatus(model.MapFromClientToServer());
            }
            catch (Exception e)
            {
                InternalServerError(e);
            }
            return Json(result);
        }
        #endregion

        #region Delete
        #endregion



        #endregion


    }
}