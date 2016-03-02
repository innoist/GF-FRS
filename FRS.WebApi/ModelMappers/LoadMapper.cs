using FRS.Models.DomainModels;
using FRS.WebApi.Models.Load;
using FRS.WebApi.Models.MT940Load;

namespace FRS.WebApi.ModelMappers
{
    public static class LoadMapper
    {
        public static LoadModel CreateFromServerToClient(this Load source)
        {
            return new LoadModel
            {
                LoadMetaDataId = source.LoadMetaDataId,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                Finish = source.Finish,
                InProgress = source.InProgress,
                LoadId = source.LoadId,
                MT940LoadId = source.MT940LoadId,
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn,
                ReadOnly = source.ReadOnly,
                Start = source.Start
            };
        }

        public static Load CreateFromClientToServer(this LoadModel source)
        {
            return new Load
            {
                LoadMetaDataId = source.LoadMetaDataId,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                Finish = source.Finish,
                InProgress = source.InProgress,
                LoadId = source.LoadId,
                MT940LoadId = source.MT940LoadId,
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn,
                ReadOnly = source.ReadOnly,
                Start = source.Start
            };
        }

        public static MT940LoadModel CreateFromServerToClient(this MT940Load source)
        {
            return new MT940LoadModel()
            {
                CreatedOnString = source.CreatedOn.ToString("yy-MMM-dd"),
                StatusId = source.StatusId,
                Status = source.Status.Name,
                CreatedBy = source.CreatedBy,
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn,
                CustomerStatementCount = source.CustomerStatementCount,
                FileContentId = source.FileContentId,
                FileExtension = source.FileExtension,
                FileName = source.FileName,
                MT940LoadId = source.MT940LoadId,
                Path = source.Path
            };
        }

        public static MT940Load CreateFromClientToServer(this MT940LoadModel source)
        {
            return new MT940Load()
            {
                CreatedOn = source.CreatedOn,
                StatusId = source.StatusId,
                CreatedBy = source.CreatedBy,
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn,
                CustomerStatementCount = source.CustomerStatementCount,
                FileContentId = source.FileContentId,
                FileExtension = source.FileExtension,
                FileName = source.FileName,
                MT940LoadId = source.MT940LoadId,
                Path = source.Path
            };
        }
    }
}