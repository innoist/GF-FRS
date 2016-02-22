using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.WebApi.Models.MetaData;

namespace FRS.WebApi.ViewModels.MetaData
{
    public class BaseDataLoadMetaData
    {
        public IEnumerable<DropDownModel> LoadTypes { get; set; }
        public IEnumerable<DropDownModel> Sources { get; set; }
        public IEnumerable<DropDownModel> Currencies { get; set; }
        public IEnumerable<DropDownModel> Statuses { get; set; }
        public LoadMetaData MetaData { get; set; }
    }
}