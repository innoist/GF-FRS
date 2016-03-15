namespace FRS.WebApi.Models.OracleGLLoad
{
    public class OracleGLLoadModel
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
        public string ModifiedOnString { get; set; }
        public byte StatusId { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
    }
}
