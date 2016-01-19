using System;

namespace FRS.Models.DomainModels
{
    public class Load
    {
        public long LoadId { get; set; }
        public byte LoadTypeId { get; set; }
        public byte MetaDataId { get; set; }
        public byte MT940DetailId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual LoadMetaData LoadMetaData { get; set; }
        public virtual LoadType LoadType { get; set; }
        public virtual MT940LoadDetail MT940LoadDetail { get; set; }
    }
}
