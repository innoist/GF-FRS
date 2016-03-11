using System;
using System.ComponentModel.DataAnnotations;

namespace FRS.WebApi.Models.MetaData
{
    public class LoadMetaData
    {
        public byte LoadMetaDataId { get; set; }

        [Required(ErrorMessage = "Load Type is required.")]
        public byte LoadTypeId { get; set; }

        [Required(ErrorMessage = "Source is required.")]
        public byte SourceId { get; set; }

        [Required(ErrorMessage = "Header is required.")]
        public string Header { get; set; }

        [Required(ErrorMessage = "Trailer is required.")]
        public string Trailer { get; set; }

        [Required (ErrorMessage = "Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        public byte CurrencyId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; }
        public string ModifiedOnString { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public byte StatusId { get; set; }
        public string Currency { get; set; }
        public string LoadType { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
    }
}