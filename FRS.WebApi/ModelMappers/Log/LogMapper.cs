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

   
    }
}