using FRS.Web.Models;

namespace FRS.Web.ModelMappers
{
    public static class LoadMetaDataMapper
    {
        public static LoadMetaData CreateFromServerToClient(this FRS.Models.DomainModels.LoadMetaData source)
        {
            return new LoadMetaData
            {
                LoadMetaDataId = source.LoadMetaDataId,
                LoadTypeId = source.LoadTypeId,
                SourceId = source.SourceId,
                Header = source.Header,
                Footer = source.Footer,
                CurrencyId = source.CurrencyId,
                Description = source.Description,
                Name = source.Name,
                StatusId = source.StatusId,
                Currency = source.Currency.Name
            };
        }

        public static FRS.Models.DomainModels.LoadMetaData CreateFromClientToServer(this LoadMetaData source)
        {
            return new FRS.Models.DomainModels.LoadMetaData
            {
                LoadMetaDataId = source.LoadMetaDataId,
                LoadTypeId = source.LoadTypeId,
                SourceId = source.SourceId,
                Header = source.Header,
                Footer = source.Footer,
                CurrencyId = source.CurrencyId,
                Description = source.Description,
                Name = source.Name,
                StatusId = source.StatusId,
            };
        }
    }
}