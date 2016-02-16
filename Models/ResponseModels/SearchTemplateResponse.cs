using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class SearchTemplateResponse<T>
    {
        
        public SearchTemplateResponse()
        {
            Data = new List<T>();
        }

        public IEnumerable<T> Data { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}
