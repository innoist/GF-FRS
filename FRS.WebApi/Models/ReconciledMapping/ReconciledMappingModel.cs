using System;

namespace FRS.WebApi.Models.ReconciledMapping
{
    public class ReconciledMappingModel
    {
        public long ReconciledMappingId { get; set; }
        public long OracleGLEntryId { get; set; }
        public long OracleGLLoadId { get; set; }
        public long StatementId { get; set; }
        public long MT940CustomerStatementTransactionId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsManual { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string AccountNumber { get; set; }

        public string AccountDate { get; set; }
        public string TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string DebitOrCredit { get; set; }
        
            
            
            
            
        
      
    }
}