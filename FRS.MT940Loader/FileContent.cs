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
    
    public partial class FileContent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileContent()
        {
            this.MT940Load = new HashSet<MT940Load>();
        }
    
        public long FileContentId { get; set; }
        public byte[] FileContent1 { get; set; }
        public string Description { get; set; }
        public string FileContentBase64 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MT940Load> MT940Load { get; set; }
    }
}
