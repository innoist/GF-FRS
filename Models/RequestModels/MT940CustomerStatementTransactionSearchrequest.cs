using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class MT940CustomerStatementSearchrequest : GetPagedListRequest
    {
        public string AccountNumber { get; set; }
        public long MT940LoadId { get; set; }

        public OrderByMT940Load OrderByColumn
        {
            get
            {
                return (OrderByMT940Load)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
