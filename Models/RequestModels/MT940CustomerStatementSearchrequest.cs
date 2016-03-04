using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class MT940CustomerStatementSearchrequest : GetPagedListRequest
    {
        public string AccountNumber { get; set; }

        public OrderByCustomerStatement OrderByColumn
        {
            get
            {
                return (OrderByCustomerStatement)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
