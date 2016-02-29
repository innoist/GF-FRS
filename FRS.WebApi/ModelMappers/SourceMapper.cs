using FRS.Models.DomainModels;
using FRS.WebApi.Models.Source;

namespace FRS.WebApi.ModelMappers
{
    public static class SourceMapper
    {
        public static SourceModel MapFromServerToClient(this Source source)
        {
            return new SourceModel
            {
                Name = source.Name,
                Value = source.Value,
                StatusName = source.Status.Name,
                StatusId = source.StatusId
            };
        }
        public static Source MapFromClientToServer(this SourceModel source)
        {
            return new Source
            {
                Name = source.Name,
                Value = source.Value,
                StatusId = source.StatusId
            };
        }
    }
}