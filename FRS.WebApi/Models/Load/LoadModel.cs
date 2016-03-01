﻿using System;
using System.Web;

namespace FRS.WebApi.Models.Load
{
    public class LoadModel
    {
        public long LoadId { get; set; }
        public byte LoadMetaDataId { get; set; }
        public long? MT940LoadId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finish { get; set; }
        public bool InProgress { get; set; }
        public bool ReadOnly { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}