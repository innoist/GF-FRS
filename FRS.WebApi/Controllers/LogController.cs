using FRS.Implementation.Services;
using FRS.Interfaces.IServices;
using FRS.Models.LoggerModels;
using FRS.Models.RequestModels;
using FRS.WebApi.ViewModels.Log;
using FRS.WebBase.Mvc;
using FRS.WebBase.UnityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using FRS.WebApi.ModelMappers.Log;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogController : ApiController
    {
        private readonly ILogger loggerService = UnityWebActivator.Container.Resolve<ILogger>();

        [HttpGet]
        //[Authorize]
        [ApiException]
        public LogViewModel Get([FromUri]LogSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = loggerService.SearchLogs(searchRequest);
            LogViewModel listViewModel = new LogViewModel
            {
                LogDatas = response.Data.Select(x => x.CreateFromServerToClient()).ToList(),
                FilteredCount = response.FilteredCount,
                TotalCount = response.TotalCount
            };

            return listViewModel;
        }
    }
}