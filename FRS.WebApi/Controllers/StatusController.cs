using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models.Status;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    [Authorize]
    public class StatusController : ApiController
    {
        #region Private

        private readonly IStatusService statusService;

        public StatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        #endregion
        
        #region Public

        #region Get
        [HttpGet]
        public IEnumerable<StatusModel> Get()
        {
            var statuses =
                statusService.GetStatuses()
                    .Select(x => x.MapFromServerToClient()).ToList();
            
            return statuses;
        }
        
        [HttpGet]
        public StatusModel Get(int Id)
        {
            var status = statusService.GetStatus(Id).MapFromServerToClient();
            return status;
        }
        #endregion

        #region Post

        [HttpPost]
        [ApiException]
        public IHttpActionResult Post(StatusModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = false;
            try
            {
                result =  statusService.SaveStatus(model.MapFromClientToServer());
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
