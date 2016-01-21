using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.Models.DomainModels
{
    public class FileContent
    {
        public long FileContentId { get; set; }
        public byte[] FileContent1 { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MT940Load> MT940Load { get; set; }
    }
}
