using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using FRS.Interfaces.Repository;
using FRS.Models.Common;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class LoadMetaDataRepository : BaseRepository<LoadMetaData>, ILoadMetaDataRepository
    {
        private readonly Dictionary<OrderByLoadMetaData, Func<LoadMetaData, object>> orderClause =

            new Dictionary<OrderByLoadMetaData, Func<LoadMetaData, object>>
            {
                {OrderByLoadMetaData.Name, c => c.Name},
                {OrderByLoadMetaData.Id, c => c.LoadMetaDataId},
                {OrderByLoadMetaData.LoadType, c => c.LoadType.Name},
                {OrderByLoadMetaData.Source, c => c.Source.Name},
                {OrderByLoadMetaData.Currency, c => c.Currency},
                {OrderByLoadMetaData.CreatedOn, c => c.CreatedOn},
            };


        public LoadMetaDataRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<LoadMetaData> DbSet
        {
            get { return db.LoadMetaDatas; }
        }

        #region Public
        public IEnumerable<DropDownModel> LoadMetadataDropDown()
        {
            return DbSet.Select(x => new DropDownModel
            {
                Id = x.LoadMetaDataId,
                Name = x.Name
            });
        }

        public LoadMetaDataForLoad IsLoadTypeMT940(long loadMetaDataId)
        {
            string loadType = ConfigurationManager.AppSettings["LoadTypeFile"];
            var metaData = DbSet.FirstOrDefault(x => x.LoadMetaDataId == loadMetaDataId);
            if (metaData != null && metaData.Source != null)
            {
                return new LoadMetaDataForLoad
                {
                    IsLoadTypeMT940 = metaData.LoadType.Name == loadType,
                    LoadType = metaData.LoadType.Name,
                    SourceName = metaData.Source.Name,
                    Currency = metaData.Currency.Sign,
                    Header = metaData.Header,
                    Trailer = metaData.Footer
                    //LastModified = metaData.ModifiedOn.ToString("dd/MM/yyyy", new CultureInfo("en"))
                };
            }
            return null;
        }

        public SearchLoadMetaDataResponse SearchLoadMetaData(LoadMetaDataSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<LoadMetaData, bool>> query =
                s =>
                    (
                    (searchRequest.LoadMetaDataId == 0 || searchRequest.LoadMetaDataId.Equals(s.LoadMetaDataId)) &&
                    (searchRequest.CreatedDate == null || DbFunctions.TruncateTime(searchRequest.CreatedDate) == DbFunctions.TruncateTime(s.CreatedOn)) &&
                    (searchRequest.Name == null || s.Name.Contains(searchRequest.Name)) && 
                    (searchRequest.LoadTypeId == 0 || searchRequest.LoadTypeId == s.LoadTypeId)
                    );

            IEnumerable<LoadMetaData> loadMetaDatas = searchRequest.IsAsc
                ? DbSet
                    .Where(query)
                    .OrderBy(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet
                    .Where(query)
                    .OrderByDescending(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new SearchLoadMetaDataResponse { LoadMetaDatas = loadMetaDatas, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }

        #endregion
    }
}
