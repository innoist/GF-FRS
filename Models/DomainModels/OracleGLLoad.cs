using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public class OracleGLLoad
    {
        public long OracleGLLoadId { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public int OracleGLEntryCount { get; set; }
        public long FileContentId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public byte StatusId { get; set; }

        public virtual FileContent FileContent { get; set; }
        public virtual ICollection<Load> Loads { get; set; }
        public virtual ICollection<OracleGLEntry> OracleGLEntries { get; set; }
        public virtual Status Status { get; set; }
    }
}
