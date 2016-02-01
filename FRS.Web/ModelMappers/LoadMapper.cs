using FRS.Models.DomainModels;
using Load = FRS.Web.Models.Load;

namespace FRS.Web.ModelMappers
{
    public static class LoadMapper
    {
        public static Load CreateFromServerToClient(this FRS.Models.DomainModels.Load source)
        {
            return new Load
            {
                LoadId = source.LoadId,
                LoadMetaDataId = source.LoadMetaDataId,
                MT940LoadId = source.MT940LoadId,
                Start = source.Start,
                Finish = source.Finish,
                InProgress = source.InProgress,
                ReadOnly = source.ReadOnly,
                LoadTypeName = source.LoadMetaData.LoadType.Name,
                MetaDataName = source.LoadMetaData.Name
            };
        }

        public static FRS.Models.DomainModels.Load CreateFromClientToServer(this Load source)
        {
            return new FRS.Models.DomainModels.Load
            {
                LoadId = source.LoadId,
                LoadMetaDataId = source.LoadMetaDataId,
                MT940LoadId = source.MT940LoadId,
                Start = source.Start,
                Finish = source.Finish,
                InProgress = source.InProgress,
                ReadOnly = source.ReadOnly,
                MT940Load = new MT940Load
                {
                    FileName = source.FileName,
                    FileExtension = source.FileExtension,
                    FileContent = new FileContent
                    {
                        FileContentBase64 = source.Attachment,
                        Description = "",
                    }
                },
            };
        }

    }
}