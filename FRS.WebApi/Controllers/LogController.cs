﻿using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ViewModels.Log;
using FRS.WebBase.Mvc;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.WebApi.ModelMappers.Log;

namespace FRS.WebApi.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILogger loggerService;

        public LogController(ILogger loggerService)
        {
            this.loggerService = loggerService;
        }

        [HttpGet]
        [Authorize]
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