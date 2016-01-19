using System;
using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public class LoadMetaData
    {
        public byte LoadMetaDataId { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Name { get; set; }
        public byte CurrencyId { get; set; }
        public string Description { get; set; }
        public byte StatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<Load> Loads { get; set; }
        public virtual Status Status { get; set; }
    }
}
