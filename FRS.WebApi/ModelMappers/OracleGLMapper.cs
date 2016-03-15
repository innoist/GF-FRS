using System.Linq;
using FRS.Models.DomainModels;
using FRS.WebApi.Models.OracleGLLoad;

namespace FRS.WebApi.ModelMappers
{
    public static class OracleGLMapper
    {
        public static OracleGLLoadModel CreateFromServerToClient(this OracleGLLoad source)
        {
            return new OracleGLLoadModel()
            {
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                StatusId = source.StatusId,
                Status = source.Status.Name,
                CreatedOn = source.CreatedOn,
                FileContentId = source.FileContentId,
                ModifiedOnString = source.ModifiedOn.ToString("dd-MMM-yy"),
                FileName = source.FileName + "." + source.FileExtension,
                OracleGLEntryCount = source.OracleGLEntryCount,
                Path = source.Path,
                OracleGLLoadId = source.OracleGLLoadId,
                Name = source.Loads.FirstOrDefault() == null ? "N/A" : source.Loads.FirstOrDefault().Name
            };
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
    }
}