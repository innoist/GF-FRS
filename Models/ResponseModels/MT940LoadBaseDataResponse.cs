using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class MT940LoadBaseDataResponse
    {
        public IList<Load> Loads { get; set; }
        public IEnumerable<DropDownModel> LoadMetadataDropDown { get; set; }
    }
}
