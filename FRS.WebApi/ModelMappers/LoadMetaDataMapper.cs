namespace FRS.WebApi.ModelMappers
{
    public static class LoadMetaDataMapper
    {
        public static Models.MetaData.LoadMetaData CreateFromServerToClient(this FRS.Models.DomainModels.LoadMetaData source)
        {
            return new Models.MetaData.LoadMetaData
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
                Currency = source.Currency != null ? source.Currency.Name : "",
                LoadType = source.LoadType.Name,
                Source = source.Source.Name,
                Status = source.Status.Name
            };
        }

        public static FRS.Models.DomainModels.LoadMetaData CreateFromClientToServer(this Models.MetaData.LoadMetaData source)
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
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn
                
            };
        }
    }
}