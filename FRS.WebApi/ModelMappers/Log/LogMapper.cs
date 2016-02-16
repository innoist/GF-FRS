using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FRS.WebApi.ModelMappers.Log
{
    public static class LogMapper
    {
        public static Models.Log.LogModel CreateFromServerToClient(this FRS.Models.LoggerModels.Log source)
        {
            return new Models.Log.LogModel
            {
                LogId = source.LogId,
                EventId = source.EventId,
                Priority = source.Priority,
                Severity = source.Severity,
                Title = source.Title,
                Timestamp = source.Timestamp,
                MachineName = source.MachineName,
                AppDomainName = source.AppDomainName,
                ProcessId = source.ProcessId,
                ProcessName = source.ProcessName,                
                Message = source.Message
            };
        }

   
    }
}