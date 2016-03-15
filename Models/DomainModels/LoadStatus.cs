using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public class LoadStatu
    {
    
        public byte Value { get; set; }
        public string Name { get; set; }
        public byte StatusId { get; set; }
    
        public virtual ICollection<Load> Loads { get; set; }
        public virtual Status Status { get; set; }
    }
}
