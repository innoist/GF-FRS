using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class BaseDataLoadMetaDataResponse
    {
        public IEnumerable<LoadType> LoadTypes { get; set; }
        public IEnumerable<Source> Sources { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<LoadMetaData> LoadMetaDatas { get; set; } 
    }
}
