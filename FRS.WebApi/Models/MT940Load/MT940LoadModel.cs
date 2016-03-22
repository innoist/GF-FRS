using System;

namespace FRS.WebApi.Models.MT940Load
{
    public class MT940LoadModel
    {
        public long MT940LoadId { get; set; }
        public string Path { get; set; }
        public string Status { get; set; }
        public string LoadStatus { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte StatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; }
        public string ModifiedOnString { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public string ProgressTitle { get; set; }
        public bool Progress { get; set; }
        public string ModifiedBy { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? CustomerStatementCount { get; set; }
        public long FileContentId { get; set; }
    }
}