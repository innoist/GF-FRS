using System;
using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class ServiceLogSearchRequest : GetPagedListRequest
    {
        public string Url { get; set; }
        public string Message { get; set; }
        public DateTime? Logged { get; set; }
        

        public OrderByServiceLogs OrderByColumn
        {
            get
            {
                return (OrderByServiceLogs)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
