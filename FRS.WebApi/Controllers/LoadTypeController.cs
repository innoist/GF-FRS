using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models.LoadType;
using FRS.WebApi.ViewModels.LoadType;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    [Authorize]
    public class LoadTypeController : ApiController
    {
        #region Private

        private readonly ILoadTypeService loadTypeService;

        public LoadTypeController(ILoadTypeService loadTypeService)
        {
            this.loadTypeService = loadTypeService;
        }

        #endregion

        #region Get
        [HttpGet]
        public IEnumerable<LoadTypeModel> Get()
        {
            var loadTypes = loadTypeService.GetLoadTypes().Select(x=>x.MapFromServerToClient()).ToList();

            return loadTypes;
        }
        
        [HttpGet]
        public LoadTypeViewModel Get(int? Id)
        {
            var response = loadTypeService.GetLoadTypeBaseData(Id);
            var viewModel = new LoadTypeViewModel();
            if (response.LoadType != null)
                viewModel.LoadType = response.LoadType.MapFromServerToClient();

            viewModel.Statuses = response.Statuses.Select(x => x.MapFromServerToClient()).ToList();
            
            return viewModel;
        }
        #endregion

        #region Post

        [HttpPost]
        [ApiException]
        public IHttpActionResult Post(LoadTypeModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = false;
            try
            {
                result =  loadTypeService.SaveLoadType(model.MapFromClientToserver());
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
    }
}