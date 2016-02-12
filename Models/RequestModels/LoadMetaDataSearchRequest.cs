using System;
using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class LoadMetaDataSearchRequest : GetPagedListRequest
    {
        public int LoadMetaDataId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int LoadTypeId { get; set; }

        public OrderByLoadMetaData OrderByColumn
        {
            get
            {
                return (OrderByLoadMetaData)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
