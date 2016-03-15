using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class OracleGLLoadSearchRequest : GetPagedListRequest
    {
        public long OracleGLLoadId { get; set; }
        public byte StatusId { get; set; }

        public OrderByOracleGLLoad OrderByColumn
        {
            get
            {
                return (OrderByOracleGLLoad)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
