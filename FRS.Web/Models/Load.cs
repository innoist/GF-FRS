using System;
using System.Web;

namespace FRS.Web.Models
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
        public string LoadTypeName { get; set; }
        public string MetaDataName { get; set; }
        public string MT940Detail { get; set; }
        public long LoadMetadataId { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
    }
}