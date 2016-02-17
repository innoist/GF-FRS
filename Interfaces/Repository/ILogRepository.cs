using FRS.Models.DomainModels;
using FRS.Models.LoggerModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface ILogRepository : IBaseRepository<Log, long>
    {
         SearchTemplateResponse<Log> SearchLogs(LogSearchRequest searchRequest);
    }
}
