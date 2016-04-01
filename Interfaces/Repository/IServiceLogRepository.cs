using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IServiceLogRepository : IBaseRepository<ServiceLog, long>
    {
        SearchTemplateResponse<ServiceLog> SearchLogs(ServiceLogSearchRequest searchRequest);
    }
}