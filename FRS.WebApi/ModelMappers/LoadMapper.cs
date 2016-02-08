namespace FRS.WebApi.ModelMappers
{
    public static class LoadMapper
    {
        public static Models.Load.LoadModel CreateFromServerToClient(this FRS.Models.DomainModels.Load source)
        {
            return new Models.Load.LoadModel
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

        public static FRS.Models.DomainModels.Load CreateFromClientToServer(this Models.Load.LoadModel source)
        {
            return new FRS.Models.DomainModels.Load
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