using FRS.Models.DomainModels;
using FRS.WebApi.Models;

namespace FRS.WebApi.ModelMappers.Log
{
    public static class LogMapper
    {
        public static Models.Log.LogModel CreateFromServerToClient(this FRS.Models.LoggerModels.Log source)
        {
            return new Models.Log.LogModel
            {
                LogId = source.LogID,
                EventId = source.EventID,
                Priority = source.Priority,
                Severity = source.Severity,
                Title = source.Title,
                Timestamp = source.Timestamp.ToString("F"),
                MachineName = source.MachineName,
                AppDomainName = source.AppDomainName,
                ProcessId = source.ProcessID,
                ProcessName = source.ProcessName,                
                Message = source.Message
            };
        }
        
        public static ServiceLogModel CreateFromServerToClient(this ServiceLog source)
        {
            return new ServiceLogModel
            {
                MachineName = source.MachineName,              
                Message = source.Message,
                Application = source.Application,
                CallerDetails = source.CallerDetails,
                Callsite = source.Callsite,
                Exception = source.Exception,
                Https = source.Https,
                Level = source.Level,
                Logged = source.Logged.ToString("dd-MMM-yy HH:mm:ss"),
                Logger = source.Logger,
                PartitionKey = source.PartitionKey,
                Port = source.Port,
                Properties = source.Properties,
                RemoteAddress = source.RemoteAddress,
                RequestDump = source.RequestDump,
                ResponseDump = source.ResponseDump,
                ServerAddress = source.ServerAddress,
                ServerName = source.ServerName,
                ServiceLogID = source.ServiceLogID,
                SessionId = source.SessionId,
                SiteName = source.SiteName,
                ThreadId = source.ThreadId,
                Url = source.Url,
                UserName = source.UserName
            };
        }

   
    }
}