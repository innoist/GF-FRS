using FRS.Models.DomainModels;
using FRS.Models.IdentityModels;

namespace FRS.Models.Resources
{
    public  class ReconciledMappingGroupByModel
    {
        public long ReconciledMappingId { get; set; }
        public long OracleGLEntryId { get; set; }
        
        public long TransactionsCount { get; set; }
        
        public long TransactionsAmount { get; set; }
        public long MT940CustomerStatementTransactionId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsManual { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Identifier { get; set; }

        public virtual AspNetUser CreatedByRef { get; set; }
        public virtual AspNetUser ModifiedByRef { get; set; }
        public virtual MT940CustomerStatementTransaction MT940CustomerStatementTransaction { get; set; }
        public virtual OracleGLEntry OracleGLEntry { get; set; }
    }
}
