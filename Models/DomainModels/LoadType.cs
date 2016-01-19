using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.Models.DomainModels
{
    public class LoadType
    {
        public byte Value { get; set; }
        public string Name { get; set; }
        public byte StatusId { get; set; }

        public virtual ICollection<Load> Loads { get; set; }
        public virtual Status Status { get; set; }
    }
}
