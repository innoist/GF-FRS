using System.Collections.Generic;

namespace FRS.WebApi.ViewModels.MetaData
{
    public class LoadMetaDataListViewModel
    {
        public LoadMetaDataListViewModel()
        {
            LoadMetaDatas = new List<FRS.WebApi.Models.MetaData.LoadMetaData>();
        }

        public IEnumerable<FRS.WebApi.Models.MetaData.LoadMetaData> LoadMetaDatas { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}