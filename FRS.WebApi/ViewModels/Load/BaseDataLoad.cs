using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.WebApi.Models.Load;

namespace FRS.WebApi.ViewModels.Load
{
    public class BaseDataLoad
    {
        public List<LoadModel> Loads { get; set; }
        public List<DropDownModel> LoadMetadataDropDown { get; set; }
    }
}