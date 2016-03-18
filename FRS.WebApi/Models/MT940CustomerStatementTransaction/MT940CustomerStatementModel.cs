using System;

namespace FRS.WebApi.Models.MT940CustomerStatementTransaction
{
    public class MT940CustomerStatementTransactionModel
    {
        public long MT940CustomerStatementTransactionId { get; set; }
        public long MT940CustomerStatementId { get; set; }
        public byte Sequence { get; set; }
        public bool ReadOnly { get; set; }
        public decimal Amount { get; set; }
        public string DebitOrCredit { get; set; }
        public string Description { get; set; }
        public string EntryDate { get; set; }
        public string FundsCode { get; set; }
        public string Reference { get; set; }
        public string TransactionType { get; set; }
        public string Value { get; set; }
        public string ValueDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
