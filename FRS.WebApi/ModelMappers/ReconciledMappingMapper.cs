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
                DebitOrCredit = source.MT940CustomerStatementTransaction.DebitOrCredit == "C" ? "Credit" : source.MT940CustomerStatementTransaction.DebitOrCredit == "D" ? "Debit" : "N/A",
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                ModifiedOn = source.ModifiedOn,
                OracleGLEntryId = source.OracleGLEntryId,

                MT940CustomerStatementTransactionId = source.MT940CustomerStatementTransactionId,
                IsDeleted = source.IsDeleted,
                IsManual = source.IsManual,
                AccountNumber = source.OracleGLEntry.AccountNumber,

                AccountDate = source.OracleGLEntry.EffectiveDate.HasValue ? source.OracleGLEntry.EffectiveDate.Value.ToString():"",
                TransactionDate = source.MT940CustomerStatementTransaction.EntryDate.HasValue ? source.MT940CustomerStatementTransaction.EntryDate.Value.ToString():"",
                Amount = source.MT940CustomerStatementTransaction.Amount.ToString("C0")
            };
        }
    }
}