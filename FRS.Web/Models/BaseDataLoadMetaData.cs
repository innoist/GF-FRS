using System.Collections.Generic;
using FRS.Models.Common.DropDown;

namespace FRS.Web.Models
{
    public class BaseDataLoadMetaData
    {
        public IEnumerable<DropDownModel> LoadTypes { get; set; }
        public IEnumerable<DropDownModel> Sources { get; set; }
        public IEnumerable<DropDownModel> Currencies { get; set; }
        public IEnumerable<DropDownModel> Statuses { get; set; }
        public IList<LoadMetaData> LoadMetaDatas { get; set; }
    }
}