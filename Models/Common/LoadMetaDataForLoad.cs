﻿namespace FRS.Models.Common
{
    public class LoadMetaDataForLoad
    {
        public bool IsLoadTypeMT940 { get; set; }
        public string LoadType { get; set; }
        public string SourceName { get; set; }
        public string LastModified { get; set; }
    }
}
