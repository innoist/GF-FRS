using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class UsersSearchRequest : GetPagedListRequest
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public OrderByUsers OrderByColumn
        {
            get
            {
                return (OrderByUsers)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
