using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class MT940CustomerStatementTransactionSearchrequest : GetPagedListRequest
    {
        public string DebitOrCredit { get; set; }
        public long MT940CustomerStatementId { get; set; }

        public OrderByCustomerStatementTransaction OrderByColumn
        {
            get
            {
                return (OrderByCustomerStatementTransaction)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
