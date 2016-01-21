using FRS.Web.Models;

namespace FRS.Web.ModelMappers
{
    public static class LoadMapper
    {
        public static Load CreateFromServerToClient(this FRS.Models.DomainModels.Load source)
        {
            return new Load
            {
                LoadId = source.LoadId,
                LoadTypeId = source.LoadTypeId,
                MetaDataId = source.MetaDataId,
                MT940DetailId = source.MT940DetailId,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn,
                LoadTypeName = source.LoadType.Name,
                MT940Detail = source.MT940LoadDetail.Name,
                MetaDataName = source.LoadMetaData.Name
            };
        }

        public static FRS.Models.DomainModels.Load CreateFromClientToServer(this Load source)
        {
            return new FRS.Models.DomainModels.Load
            {
                LoadId = source.LoadId,
                LoadTypeId = source.LoadTypeId,
                MetaDataId = source.MetaDataId,
                MT940DetailId = source.MT940DetailId,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn,
            };
        }

    }
}