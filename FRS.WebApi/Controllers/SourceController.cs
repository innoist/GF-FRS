using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models.Source;
using FRS.WebApi.ViewModels.Source;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    public class SourceController : ApiController
    {
        #region Private

        private readonly ISourceService sourceService ;

        public SourceController(ISourceService sourceService)
        {
            this.sourceService = sourceService;
        }

        #endregion
        
        #region Public

        #region Get
        [HttpGet]
        public IEnumerable<SourceModel> Get()
        {
            var sources =
                sourceService.GetSources().Select(x => x.MapFromServerToClient()).ToList();
            
            return sources;
        }
        
        [HttpGet]
        public SourceViewModel Get(int? Id)
        {
            var baseData = sourceService.GetSourceById(Id);
            var viewModel = new SourceViewModel
            {
                Source = baseData.Source != null ? baseData.Source.MapFromServerToClient() : null,
                Statuses = baseData.Statuses.Select(x=>x.MapFromServerToClient()).ToList()
            };
            
            return viewModel;
        }
        #endregion

        #region Post

        [HttpPost]
        [Authorize]
        [ApiException]
        public IHttpActionResult Post(SourceModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = false;
            try
            {
                result =  sourceService.SaveSource(model.MapFromClientToServer());
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
