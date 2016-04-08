using System.Linq;
using FRS.Models.DomainModels;
using FRS.WebApi.Models.OracleGLEntry;
using FRS.WebApi.Models.OracleGLLoad;

namespace FRS.WebApi.ModelMappers
{
    public static class OracleGLMapper
    {
        public static OracleGLLoadModel CreateFromServerToClient(this OracleGLLoad source)
        {
            var toReturn = new OracleGLLoadModel
            {
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                StatusId = source.StatusId,
                Status = source.Status.Name,
                CreatedOn = source.CreatedOn,
                FileContentId = source.FileContentId,
                ModifiedOnString = source.ModifiedOn.ToString("dd-MMM-yy HH:mm:ss"),
                FileName = source.FileName + "." + source.FileExtension,
                OracleGLEntryCount = source.OracleGLEntryCount,
                Path = source.Path,
                OracleGLLoadId = source.OracleGLLoadId
            };
            var load = source.Loads.FirstOrDefault();
            if (load != null)
            {
                toReturn.Name = load.Name;
                toReturn.Start = load.Start.ToString("dd-MMM-yy HH:mm:ss");
                toReturn.Finish = load.Finish.HasValue ? load.Finish.Value.ToString("dd-MMM-yy HH:mm:ss") : "N/A";
                toReturn.LoadStatus = load.LoadStatu.Name;
                toReturn.Progress = load.InProgress;
                toReturn.ProgressTitle = load.InProgress ? "Yes" : "No";
            }

            return toReturn;
        }
        public static OracleGLLoad CreateFromClientToServer(this OracleGLLoadModel source)
        {
            return new OracleGLLoad
            {
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                StatusId = source.StatusId,
                CreatedOn = source.CreatedOn,
                FileContentId = source.FileContentId,
                FileName = source.FileName + "." + source.FileExtension,
                OracleGLEntryCount = source.OracleGLEntryCount,
                Path = source.Path,
                OracleGLLoadId = source.OracleGLLoadId,
                ModifiedOn = source.ModifiedOn
            };
        }
        
        public static OracleGLEntryModel CreateFromServerToClient(this OracleGLEntry source)
        {
            return new OracleGLEntryModel
            {
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                StatusId = source.StatusId,
                CreatedOn = source.CreatedOn.ToString("dd-MMM-yy HH:mm:ss"),
                ModifiedOn = source.ModifiedOn.ToString("dd-MMM-yy HH:mm:ss"),
                OracleGLLoadId = source.OracleGLLoadId,
                AccountDescription = source.AccountDescription,
                AccountNumber = source.AccountNumber,
                AccountedCr = source.AccountedCr.HasValue ? source.AccountedCr.Value.ToString("N") : "N/A",
                AccountedDr = source.AccountedDr.HasValue ? source.AccountedDr.Value.ToString("N") : "N/A",
                Currency = source.Currency,
                EffectiveDate = source.EffectiveDate.HasValue ? source.EffectiveDate.Value.ToString("dd-MMM-yy HH:mm:ss") : "N/A",
                EnteredCr = source.EnteredCr,
                UniqueReferenceKey = source.UniqueReferenceKey,
                EntrySource = source.EntrySource,
                Period = source.Period,
                OracleGLEntryId = source.OracleGLEntryId,
                EnteredDr = source.EnteredCr,
                ExchangeRate = source.ExchangeRate,
                JECreationDate = source.JECreationDate.HasValue ? source.JECreationDate.Value.ToString("dd-MMM-yy HH:mm:ss") : "N/A",
                JELastUpdateDate = source.JELastUpdateDate.HasValue ? source.JELastUpdateDate.Value.ToString("dd-MMM-yy HH:mm:ss") : "N/A",
                JournalEntryDescription = source.JournalEntryDescription,
                JournalEntryHeaderNumber = source.JournalEntryDescription,
                LineDescription = source.LineDescription,
                LineNumber = source.LineNumber,
                SubAccountDescription = source.SubAccountDescription,
                Year = source.FiscalYear.Value,

                Type = source.AccountedCr.HasValue && source.AccountedCr.Value != 0 ? "Credit" : "Debit",
                Amount = source.AccountedCr.HasValue && source.AccountedCr.Value != 0 ? source.AccountedCr.Value : source.AccountedDr.HasValue && source.AccountedDr.Value != 0 ? source.AccountedDr.Value : 0
            };

            //return toReturn;
        }
        
    }
}