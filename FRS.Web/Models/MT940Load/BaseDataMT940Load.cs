using System.Collections.Generic;
using FRS.Models.Common.DropDown;

namespace FRS.Web.Models.MT940Load
{
    public class BaseDataMT940Load
    {
        public IList<Load> Loads { get; set; }
        public IEnumerable<DropDownModel> LoadMetadataDropDown { get; set; }
    }
}