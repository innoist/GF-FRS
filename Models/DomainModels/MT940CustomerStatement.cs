using System;
using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public class MT940CustomerStatement
    {
        public long MT940CustomerStatementId { get; set; }
        public long MT940LoadId { get; set; }
        public byte Sequence { get; set; }
        public bool ReadOnly { get; set; }
        public string AccountNumber { get; set; }
        public long ClosingAvailableBalanceId { get; set; }
        public long? ClosingBalanceId { get; set; }
        public string Description { get; set; }
        public long? ForwardAvailableBalanceId { get; set; }
        public long? OpeningBalanceId { get; set; }
        public string ReleatedMessage { get; set; }
        public int SequenceNumber { get; set; }
        public int StatementNumber { get; set; }
        public string TransactionReference { get; set; }
        public int TransactionCount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MT940Balance MT940Balance { get; set; }
        public virtual MT940Balance MT940Balance1 { get; set; }
        public virtual MT940Balance MT940Balance2 { get; set; }
        public virtual MT940Balance MT940Balance3 { get; set; }
        public virtual MT940Load MT940Load { get; set; }
        public virtual ICollection<MT940CustomerStatementTransaction> MT940CustomerStatementTransaction { get; set; }
    }
}
