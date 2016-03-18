using FRS.Models.DomainModels;
using FRS.WebApi.Models.MT940CustomerStatement;
using FRS.WebApi.Models.MT940CustomerStatementTransaction;

namespace FRS.WebApi.ModelMappers
{
    public static class CustomerStatementMapper
    {
        public static MT940CustomerStatementModel MapFromServerToClient(this MT940CustomerStatement source)
        {
            return new MT940CustomerStatementModel()
            {
                AccountNumber = source.AccountNumber,
                Description = source.Description,
                ReleatedMessage = source.ReleatedMessage,
                TransactionReference = source.TransactionReference,
                MT940CustomerStatementId = source.MT940CustomerStatementId,
                Sequence = source.Sequence,
                SequenceNumber = source.SequenceNumber,
                StatementNumber = source.StatementNumber,
                TransactionCount = source.TransactionCount,
                ReadOnly = source.ReadOnly                
            };
        }
        
        public static MT940CustomerStatementTransactionModel MapFromServerToClient(this MT940CustomerStatementTransaction source)
        {
            return new MT940CustomerStatementTransactionModel()
            {
                Description = source.Description,
                DebitOrCredit = source.DebitOrCredit == "C" ? "Credit" : "Debit",
                TransactionType = source.TransactionType,
                Amount = source.Amount,
                Reference = source.Reference,
                MT940CustomerStatementTransactionId = source.MT940CustomerStatementTransactionId,
                MT940CustomerStatementId = source.MT940CustomerStatementId
            };
        }
    }
}