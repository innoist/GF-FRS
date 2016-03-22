using System;
using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public class MT940CustomerStatementTransaction
    {
        public long MT940CustomerStatementTransactionId { get; set; }
        public long MT940CustomerStatementId { get; set; }
        public byte Sequence { get; set; }
        public bool ReadOnly { get; set; }
        public decimal Amount { get; set; }
        public string DebitOrCredit { get; set; }
        public string Description { get; set; }
        public DateTime? EntryDate { get; set; }
        public string FundsCode { get; set; }
        public string Reference { get; set; }
        public string TransactionType { get; set; }
        public string Value { get; set; }
        public DateTime ValueDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MT940CustomerStatement MT940CustomerStatement { get; set; }
        public virtual ICollection<ReconciledMapping> ReconciledMappings { get; set; }
    }
}
