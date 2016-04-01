using FRS.Interfaces.IServices;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;
using FRS.Models.RequestModels;
using FRS.Interfaces.Repository;

namespace FRS.Implementation.Services
{
    /// <summary>
    /// Logger class that manages log entries 
    /// </summary>
    public sealed class ServiceLogService : IServiceLogService
    {
        private readonly IServiceLogRepository serviceLogRepository;

        public ServiceLogService(IServiceLogRepository serviceLogRepository)
        {
            this.serviceLogRepository = serviceLogRepository;
        }

        public SearchTemplateResponse<ServiceLog> SearchLogs(ServiceLogSearchRequest searchRequest)
        {
            return serviceLogRepository.SearchLogs(searchRequest);
        }

    }
}
