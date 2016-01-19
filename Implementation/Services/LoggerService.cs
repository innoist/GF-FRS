using System;
using System.Collections.Generic;
using System.Diagnostics;
using FRS.Interfaces.IServices;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace FRS.Implementation.Services
{
    /// <summary>
    /// Logger class that manages log entries 
    /// </summary>
    public sealed class LoggerService : ILogger
    {
        /// <summary>
        /// Write Log to database
        /// </summary>
        public void Write(string message, string category, int priority, int eventId, TraceEventType severity, string title)
        {
            try
            {
                Logger.Write(message, category, priority, eventId, severity, title);
            }
            catch (Exception ex)
            {
                string a = ex.Message;

                throw;
            }
        }

        /// <summary>
        /// Write Log to database
        /// </summary>
        public void Write(object message, string category, int priority, int eventId, TraceEventType severity, string title, IDictionary<string, object> properties)
        {
            try
            {
                Logger.Write(message, category, priority, eventId, severity, title, properties);
            }
            catch (Exception ex)
            {
                string a = ex.Message;

                throw;
            }
            
        }
    }
}
