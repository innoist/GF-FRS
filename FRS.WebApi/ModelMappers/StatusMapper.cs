using FRS.Models.DomainModels;
using FRS.WebApi.Models.Status;

namespace FRS.WebApi.ModelMappers
{
    public static class StatusMapper
    {
        public static StatusModel MapFromServerToClient(this Status source)
        {
            return new StatusModel
            {
                Name = source.Name,
                Value = source.Value
            };
        }
        public static Status MapFromClientToServer(this StatusModel source)
        {
            return new Status
            {
                Name = source.Name,
                Value = source.Value
            };
        }
    }
}