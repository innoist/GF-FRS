//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FRS.MT940Loader
{
    using System;
    using System.Collections.Generic;
    
    public partial class Load
    {
        public long LoadId { get; set; }
        public byte LoadMetaDataId { get; set; }
        public Nullable<long> MT940LoadId { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> Finish { get; set; }
        public bool InProgress { get; set; }
        public bool ReadOnly { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Name { get; set; }
    
        public virtual LoadMetaData LoadMetaData { get; set; }
        public virtual MT940Load MT940Load { get; set; }
    }
}
