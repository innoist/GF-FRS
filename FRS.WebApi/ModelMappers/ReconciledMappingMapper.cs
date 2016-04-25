using FRS.Models.DomainModels;
using FRS.WebApi.Models.MT940Balance;
using FRS.WebApi.Models.ReconciledMapping;

namespace FRS.WebApi.ModelMappers
{
    public static class ReconciledMappingMapper
    {
        public static ReconciledMappingModel MapFromServerToClient(this ReconciledMapping source)
        {
            return new ReconciledMappingModel
            {
                ReconciledMappingId = source.ReconciledMappingId,
                Identifier = source.Identifier,
                DebitOrCredit = source.OracleGLEntry.AccountedCr.HasValue && source.OracleGLEntry.AccountedCr.Value != 0 ? "Credit" : "Debit",
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                ModifiedOn = source.ModifiedOn,
                OracleGLEntryId = source.OracleGLEntryId,
                OracleGLLoadId = source.OracleGLEntry.OracleGLLoadId,
                MT940CustomerStatementTransactionId = source.MT940CustomerStatementTransactionId,
                IsDeleted = source.IsDeleted,
                IsManual = source.IsManual,
                AccountNumber = source.OracleGLEntry.AccountNumber,
                TransactionsCount = source.TransactionsCount,
                Amount = source.TransactionAmount
            };
        }

        public static ReconciledMapping MapFromServerToClient(this ReconciledMappingModel source)
        {
            return new ReconciledMapping
            {
                MT940CustomerStatementTransactionId = source.MT940CustomerStatementTransactionId,
                OracleGLEntryId = source.OracleGLEntryId,
                IsDeleted = source.IsDeleted,
                IsManual = source.IsManual,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn
            };
        }
    }
}