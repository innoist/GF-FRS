using System;
using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class ReconciledMappingSearchRequest : GetPagedListRequest
    {
        public long ReconciledMappingId { get; set; }
        public string Amount { get; set; }

        public DateTime? AccountDate { get; set; }
        public DateTime? TransactDate { get; set; }

        public OrderByReconciledMapping OrderByColumn
        {
            get
            {
                return (OrderByReconciledMapping)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
