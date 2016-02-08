using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class SearchLoadMetaDataResponse
    {
        
        public SearchLoadMetaDataResponse()
        {
            LoadMetaDatas = new List<LoadMetaData>();
        }

        public IEnumerable<LoadMetaData> LoadMetaDatas { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}
