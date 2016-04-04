using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface IServiceLogService
    {
        SearchTemplateResponse<ServiceLog> SearchLogs(ServiceLogSearchRequest searchRequest);

        ServiceLog GetLog(int id);
    }
}
