using FRS.Models.DomainModels;
using FRS.WebApi.Models.LoadType;

namespace FRS.WebApi.ModelMappers
{
    public static class LoadTypeMapper
    {
        public static LoadType MapFromClientToserver(this LoadTypeModel source)
        {

            return new LoadType
            {
                Name = source.Name,
                Value = source.Value,
                StatusId = source.StatusId,
            };
        }

        public static LoadTypeModel MapFromServerToClient(this LoadType source)
        {

            return new LoadTypeModel
            {
                Name = source.Name,
                Value = source.Value,
                StatusId = source.StatusId,
                StatusName = source.Status.Name
            };
        }
    }
}