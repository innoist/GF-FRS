using System;

namespace FRS.WebApi.Models.MetaData
{
    public class LoadMetaData
    {
        public byte LoadMetaDataId { get; set; }
        public byte LoadTypeId { get; set; }
        public byte SourceId { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Name { get; set; }
        public byte CurrencyId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public byte StatusId { get; set; }
        public string Currency { get; set; }
    }
}