using System;
using System.Linq;
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
                ModifiedOnStr = source.ModifiedOn.ToString("dd-MMM-yy"),
                ReadOnly = source.ReadOnly,
                Start = source.Start,
                StartString = source.Start.ToString("dd-MMM-yy"),
                Name = source.Name
            };
        }

        public static Load CreateFromClientToServer(this LoadModel source, string userId)
        {
            var load = new Load
            {
                LoadMetaDataId = source.LoadMetaDataId,
                CreatedBy = userId,
                CreatedOn = source.CreatedOn,
                Finish = source.Finish,
                InProgress = source.InProgress,
                LoadId = source.LoadId,
                MT940LoadId = source.MT940LoadId,
                ModifiedBy = userId,
                ModifiedOn = source.ModifiedOn,
                ReadOnly = source.ReadOnly,
                Start = DateTime.UtcNow,
                Name = source.Name,
                MT940Load = new MT940Load
                {
                    FileContent = new FileContent
                    {
                        FileContentBase64 = source.FileBase64Content,
                        CreatedBy = userId,
                        ModifiedBy = userId

                    },
                    FileName = source.FileName,
                    FileExtension = source.FileExtension,
                    CreatedBy = userId,
                    ModifiedBy = userId,
                    Path = "C:/",
                    CustomerStatementCount = 1,
                    StatusId = 2

                },
            };

            return load;
        }

        public static MT940LoadModel CreateFromServerToClient(this MT940Load source)
        {
            return new MT940LoadModel()
            {
                CreatedOnString = source.CreatedOn.ToString("dd-MMM-yy"),
                ModifiedOnString = source.ModifiedOn.ToString("dd-MMM-yy"),
                StatusId = source.StatusId,
                Status = source.Status.Name,
                CustomerStatementCount = source.CustomerStatementCount,
                FileName = source.FileName + "." + source.FileExtension,
                MT940LoadId = source.MT940LoadId,
                Name = source.Loads.FirstOrDefault() != null ? source.Loads.FirstOrDefault().Name : "N/A"
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