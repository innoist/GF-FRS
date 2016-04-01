﻿namespace FRS.Models.DomainModels
{
    public class ServiceLog
    {
        public int ServiceLogID { get; set; }
        public string MachineName { get; set; }
        public string SiteName { get; set; }
        public System.DateTime Logged { get; set; }
        public string Level { get; set; }
        public string UserName { get; set; }
        public string Application { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Properties { get; set; }
        public string ServerName { get; set; }
        public string Port { get; set; }
        public string SessionId { get; set; }
        public int? ThreadId { get; set; }
        public string CallerDetails { get; set; }
        public string RequestDump { get; set; }
        public string ResponseDump { get; set; }
        public string Url { get; set; }
        public bool? Https { get; set; }
        public string ServerAddress { get; set; }
        public string RemoteAddress { get; set; }
        public string Callsite { get; set; }
        public byte PartitionKey { get; set; }
        public string Exception { get; set; }
    }
}
