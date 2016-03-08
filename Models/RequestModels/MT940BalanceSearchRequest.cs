using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class MT940BalanceSearchRequest : GetPagedListRequest
    {
        public byte CurrencyId { get; set; }

        public OrderByMT940Balance OrderByColumn
        {
            get
            {
                return (OrderByMT940Balance)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
