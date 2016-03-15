using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class MT940LoadDetailResponse
    {
        public Load Load { get; set; }
        public MT940Load Mt940Load { get; set; }
        public LoadMetaData LoadMetaData { get; set; }
    }
}
