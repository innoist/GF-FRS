using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.Models.DomainModels
{
    public class MT940Load
    {
        public long MT940LoadId { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte StatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? CustomerStatementCount { get; set; }
        public long FileContentId { get; set; }

        public virtual FileContent FileContent { get; set; }
        public virtual ICollection<Load> Loads { get; set; }
        public virtual ICollection<MT940CustomerStatement> MT940CustomerStatement { get; set; }
        public virtual Status Status { get; set; }
    }
}
