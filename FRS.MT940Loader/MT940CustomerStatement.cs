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
    
    public partial class MT940CustomerStatement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MT940CustomerStatement()
        {
            this.MT940CustomerStatementTransaction = new HashSet<MT940CustomerStatementTransaction>();
        }
    
        public long MT940CustomerStatementId { get; set; }
        public long MT940LoadId { get; set; }
        public byte Sequence { get; set; }
        public bool ReadOnly { get; set; }
        public string AccountNumber { get; set; }
        public long ClosingAvailableBalanceId { get; set; }
        public Nullable<long> ClosingBalanceId { get; set; }
        public string Description { get; set; }
        public Nullable<long> ForwardAvailableBalanceId { get; set; }
        public Nullable<long> OpeningBalanceId { get; set; }
        public string ReleatedMessage { get; set; }
        public int SequenceNumber { get; set; }
        public int StatementNumber { get; set; }
        public string TransactionReference { get; set; }
        public int TransactionCount { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual MT940Balance ClosingAvailableBalance { get; set; }
        public virtual MT940Balance ClosingBalance { get; set; }
        public virtual MT940Balance ForwardAvailableBalance { get; set; }
        public virtual MT940Balance OpeningBalance { get; set; }
        public virtual MT940Load MT940Load { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MT940CustomerStatementTransaction> MT940CustomerStatementTransaction { get; set; }
    }
}
