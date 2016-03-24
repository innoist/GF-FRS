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
            var toReturn = new OracleGLLoadModel()
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
            if (source.Loads.FirstOrDefault() != null)
            {
                var load = source.Loads.FirstOrDefault();

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
            return new OracleGLLoad()
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
            return new OracleGLEntryModel()
            {
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                StatusId = source.StatusId,
                CreatedOn = source.CreatedOn.ToString("dd-MMM-yy HH:mm:ss"),
                ModifiedOn = source.ModifiedOn.ToString("dd-MMM-yy HH:mm:ss"),
                OracleGLLoadId = source.OracleGLLoadId,
                AccountDescription = source.AccountDescription,
                AccountNumber = source.AccountNumber,
                AccountedCr = source.AccountedCr,
                AccountedDr = source.AccountedDr,
                Currency = source.Currency,
                EffectiveDate = source.EffectiveDate,
                EnteredCr = source.EnteredCr,
                UniqueReferenceKey = source.UniqueReferenceKey,
                EntrySource = source.EntrySource,
                Period = source.Period,
                OracleGLEntryId = source.OracleGLEntryId,
                //Name = source.OracleGLLoad.Loads.FirstOrDefault().Name,
                Year = source.FiscalYear.Name
                
            };
        }
        public static OracleGLEntry CreateFromClientToServer(this OracleGLEntryModel source)
        {
            return new OracleGLEntry()
            {
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                StatusId = source.StatusId,
                //CreatedOn = source.CreatedOn,
                OracleGLLoadId = source.OracleGLLoadId,
                AccountDescription = source.AccountDescription,
                AccountNumber = source.AccountNumber,
                AccountedCr = source.AccountedCr,
                AccountedDr = source.AccountedDr,
                Currency = source.Currency,
                EffectiveDate = source.EffectiveDate,
                EnteredCr = source.EnteredCr,
                UniqueReferenceKey = source.UniqueReferenceKey,
                EntrySource = source.EntrySource,
                Period = source.Period,
                OracleGLEntryId = source.OracleGLEntryId
            };
        }
    }
}