using FRS.WebApi.Models.Load;
using FRS.WebApi.Models.MetaData;
using FRS.WebApi.Models.MT940Load;

namespace FRS.WebApi.ViewModels.MT940Load
{
    public class MT940LoadDetail
    {
        public LoadModel Load { get; set; }
        public MT940LoadModel Mt940LoadModel { get; set; }
        public LoadMetaData LoadMetaData { get; set; }
    }
}