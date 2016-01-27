using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public class Currency
    {
        public byte Value { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }

        public virtual ICollection<MT940Balance> MT940Balance { get; set; }
        public virtual ICollection<LoadMetaData> LoadMetaDatas { get; set; }
    }
}
