using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class BaseDataLoadMetaDataResponse
    {
        public IEnumerable<DropDownModel> LoadTypes { get; set; }
        public IEnumerable<DropDownModel> Sources { get; set; }
        public IEnumerable<DropDownModel> Currencies { get; set; }
        public IEnumerable<DropDownModel> Statuses { get; set; }
        public IEnumerable<LoadMetaData> LoadMetaDatas { get; set; } 
    }
}
