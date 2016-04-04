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
                ModifiedOnStr = source.ModifiedOn.ToString("dd-MMM-yy HH:mm:ss"),
                ReadOnly = source.ReadOnly,
                Start = source.Start,
                StartString = source.Start.ToString("dd-MMM-yy HH:mm:ss"),
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
                InProgress = false,
                LoadId = source.LoadId,
                MT940LoadId = source.MT940LoadId,
                ModifiedBy = userId,
                ModifiedOn = source.ModifiedOn,
                ReadOnly = false,
                Start = DateTime.UtcNow,
                Name = source.Name,
                LoadStatusId = 0
                
            };

            if (source.LoadType.Contains("MT940"))
            {
                load.MT940Load = new MT940Load
                {
                    FileContent = new FileContent
                    {
                        FileContentBase64 = source.FileBase64Content.Split(',')[1],
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

                };
            }
            else
            {
                load.OracleGLLoad = new OracleGLLoad()
                {
                    FileContent = new FileContent
                    {
                        FileContentBase64 = source.FileBase64Content.Split(',')[1],
                        CreatedBy = userId,
                        ModifiedBy = userId

                    },
                    FileName = source.FileName,
                    FileExtension = source.FileExtension,
                    CreatedBy = userId,
                    ModifiedBy = userId,
                    Path = "C:/",
                    OracleGLEntryCount = 0,
                    StatusId = 2

                };
            }

            return load;
        }

        public static MT940LoadModel CreateFromServerToClient(this MT940Load source)
        {
            var toReturn =  new MT940LoadModel()
            {
                CreatedOnString = source.CreatedOn.ToString("dd-MMM-yy HH:mm:ss"),
                ModifiedOnString = source.ModifiedOn.ToString("dd-MMM-yy HH:mm:ss"),
                StatusId = source.StatusId,
                CustomerStatementCount = source.CustomerStatementCount,
                FileName = source.FileName + "." + source.FileExtension,
                MT940LoadId = source.MT940LoadId,
                Status = source.Status.Name
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