using System;

namespace FRS.Models.DomainModels
{
    public class Load
    {
        public long LoadId { get; set; }
        public byte LoadMetaDataId { get; set; }
        public long? MT940LoadId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finish { get; set; }
        public bool InProgress { get; set; }
        public bool ReadOnly { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Name { get; set; }
        public long? OracleGLLoadId { get; set; }

        public virtual LoadMetaData LoadMetaData { get; set; }
        public virtual OracleGLLoad OracleGLLoad { get; set; }
        public virtual MT940Load MT940Load { get; set; }
    }
}
