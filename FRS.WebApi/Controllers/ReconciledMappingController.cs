using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.ReconciledMapping;
using FRS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace FRS.WebApi.Controllers
{
    public class ReconciledMappingController : ApiController
    {
        #region Private

        private readonly IReconciledMappingService reconciledMappingService;
        #endregion

        #region Public

        public ReconciledMappingController(IReconciledMappingService reconciledMappingService)
        {
            this.reconciledMappingService = reconciledMappingService;
        }



        [HttpGet]
        [Authorize]
        [ApiException]
        public ReconciledMappingViewModel Get([FromUri]ReconciledMappingSearchRequest searchRequest)
        {
          if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = reconciledMappingService.GetReconciledMappingSearchResponse(searchRequest);
            ReconciledMappingViewModel listViewModel = new ReconciledMappingViewModel
            {
                ReconciledMappingModels = response.Data.Select(x => x.MapFromServerToClient()).ToList(),
                FilteredCount = response.FilteredCount,
                TotalCount = response.TotalCount
            };

            return listViewModel;
            
        }

        #endregion

        #region Post

        [HttpPost]
        [Authorize]
        [ApiException]
        public IHttpActionResult Post(ReconciliationViewModel model)
        {
            //HttpContext.Current.Session
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            var mappings = model.TransactionIds.Select(transactionId => new ReconciledMapping
            {
                OracleGLEntryId = model.OracleGlEntryId, 
                MT940CustomerStatementTransactionId = transactionId, 
                IsDeleted = false, 
                IsManual = true, 
                CreatedBy = userId, 
                ModifiedBy = userId, 
                CreatedOn = DateTime.UtcNow, 
                ModifiedOn = DateTime.UtcNow,
            }).ToList();

            var result = reconciledMappingService.SaveReconciledMappings(mappings);

            return Json(result);
        }

        #endregion

        
    }
}
