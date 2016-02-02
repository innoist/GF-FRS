using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public class FileContent
    {
        public long FileContentId { get; set; }
        public byte[] FileContent1 { get; set; }
        public string Description { get; set; }
        public string FileContentBase64 { get; set; }

        public virtual ICollection<MT940Load> MT940Load { get; set; }
    }
}
