namespace FRS.Web.Models
{
    public class FileContent
    {
        public long FileContentId { get; set; }
        public byte[] FileContent1 { get; set; }
        public string Description { get; set; }
        public string FileContentBase64 { get; set; }
    }
}