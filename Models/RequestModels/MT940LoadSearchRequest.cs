using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class MT940LoadSearchRequest : GetPagedListRequest
    {
        public long MT940LoadId { get; set; }
        public byte StatusId { get; set; }

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
