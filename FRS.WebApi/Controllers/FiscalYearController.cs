using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models;
using FRS.WebBase.Mvc;

namespace FRS.WebApi.Controllers
{
    [Authorize]
    public class FiscalYearController : ApiController
    {
        private readonly IFiscalYearService fiscalYearService;

        #region Private


        public FiscalYearController(IFiscalYearService fiscalYearService)
        {
            this.fiscalYearService = fiscalYearService;
        }

        #endregion
        
        #region Public

        #region Get
        [HttpGet]
        public IEnumerable<FiscalYearModel> Get()
        {
            var years =
                fiscalYearService.GetFiscalYears()
                    .Select(x => x.MapFromServerToClient()).ToList();
            
            return years;
        }
        
        [HttpGet]
        public FiscalYearModel Get(int Id)
        {
            var year = fiscalYearService.GetFiscalYear(Id).MapFromServerToClient();
            return year;
        }
        #endregion

        #region Post

        [HttpPost]
        [Authorize]
        [ApiException]
        public IHttpActionResult Post(FiscalYearModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = false;
            try
            {
                result =  fiscalYearService.SaveFiscalYear(model.MapFromClientToServer());
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