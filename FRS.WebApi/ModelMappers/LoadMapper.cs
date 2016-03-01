using FRS.Models.DomainModels;
using FRS.WebApi.Models.Load;

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
    }
}