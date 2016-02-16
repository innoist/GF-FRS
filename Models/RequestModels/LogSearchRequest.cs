using System;
using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class LogSearchRequest : GetPagedListRequest
    {
        public string Severity { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
        

        public OrderByLogs OrderByColumn
        {
            get
            {
                return (OrderByLogs)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
