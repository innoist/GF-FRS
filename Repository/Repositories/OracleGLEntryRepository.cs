using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FRS.Interfaces.Repository;
using FRS.Models.Common;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class OracleGLEntryRepository : BaseRepository<OracleGLEntry>, IOracleGLEntryRepository
    {
        private readonly Dictionary<OrderByOracleGLEntry, Func<OracleGLEntry, object>> orderClause =

            new Dictionary<OrderByOracleGLEntry, Func<OracleGLEntry, object>>
            {
                {OrderByOracleGLEntry.Id, c => c.OracleGLEntryId},
                {OrderByOracleGLEntry.AccountLoad, c => c.OracleGLLoadId},
                {OrderByOracleGLEntry.UniqueRef, c => c.UniqueReferenceKey},
                {OrderByOracleGLEntry.Ac, c => c.AccountedCr},
            };
        public OracleGLEntryRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<OracleGLEntry> DbSet
        {
            get { return db.OracleGLEntries; }
        }


        public SearchTemplateResponse<OracleGLEntry> GetOracleGLEntrySearchResponse(OracleGLEntrySearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<OracleGLEntry, bool>> query =
                s =>
                    (
                    (searchRequest.OracleGLLoadId == 0 || searchRequest.OracleGLLoadId == (s.OracleGLLoadId))
                    );

            IEnumerable<OracleGLEntry> data = searchRequest.IsAsc
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
            return new SearchTemplateResponse<OracleGLEntry> { Data = data, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }
    }
}
