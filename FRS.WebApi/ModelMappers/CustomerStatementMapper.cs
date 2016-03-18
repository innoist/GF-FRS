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
                StatementNumber =  source.StatementNumber,
                TransactionCount =  source.TransactionCount,
                MT940ClosingAvailableBalance = source.MT940Balance!=null?source.MT940Balance.MapFromServerToClient():null,
                MT940ClosingBalance = source.MT940Balance1 != null ? source.MT940Balance1.MapFromServerToClient() : null,
                MT940ForwardAvailableBalanceModel = source.MT940Balance2 != null ? source.MT940Balance2.MapFromServerToClient() : null,
                MT940OpeningBalance = source.MT940Balance3 != null ? source.MT940Balance3.MapFromServerToClient() : null,
                
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
                MT940CustomerStatementId = source.MT940CustomerStatementId,
                
                FundsCode =  string.IsNullOrEmpty(source.FundsCode) ? "-" : source.FundsCode,
                
                Sequence = source.Sequence,
                ReadOnly = source.ReadOnly,
                Value = source.Value
            };
        }
    }
}