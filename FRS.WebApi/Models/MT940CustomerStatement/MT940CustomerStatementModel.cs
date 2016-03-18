using System;
using FRS.WebApi.Models.MT940Balance;

namespace FRS.WebApi.Models.MT940CustomerStatement
{
    public class MT940CustomerStatementModel
    {
        public long MT940CustomerStatementId { get; set; }
        public long MT940LoadId { get; set; }
        public byte Sequence { get; set; }
        public bool ReadOnly { get; set; }
        public string AccountNumber { get; set; }
        public long ClosingAvailableBalance { get; set; }
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

        public MT940BalanceModel MT940ClosingAvailableBalance { get; set; }
        public MT940BalanceModel MT940ClosingBalance { get; set; }
        public MT940BalanceModel MT940ForwardAvailableBalanceModel { get; set; }
        public MT940BalanceModel MT940OpeningBalance { get; set; }



    }
}
