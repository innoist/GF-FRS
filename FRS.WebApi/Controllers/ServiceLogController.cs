using FRS.Interfaces.IServices;
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
    [Authorize]
    public class ServiceLogController : ApiController
    {
        private readonly IServiceLogService serviceLogService;

        public ServiceLogController(IServiceLogService serviceLogService)
        {
            this.serviceLogService = serviceLogService;
        }

        [HttpGet]
        [ApiException]
        public ServiceLogViewModel Get([FromUri]ServiceLogSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = serviceLogService.SearchLogs(searchRequest);
            ServiceLogViewModel listViewModel = new ServiceLogViewModel
            {
                Data = response.Data.Select(x => x.CreateFromServerToClient()).ToList(),
                FilteredCount = response.FilteredCount,
                TotalCount = response.TotalCount
            };

            return listViewModel;
        }
    }
}