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
    public class OracleGLLoadRepository : BaseRepository<OracleGLLoad>, IOracleGLLoadRepository
    {
        private readonly Dictionary<OrderByOracleGLLoad, Func<OracleGLLoad, object>> orderClause =

            new Dictionary<OrderByOracleGLLoad, Func<OracleGLLoad, object>>
            {
                {OrderByOracleGLLoad.Id, c => c.OracleGLLoadId},
            };
        public OracleGLLoadRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<OracleGLLoad> DbSet
        {
            get { return db.OracleGLLoads; }
        }


        public SearchTemplateResponse<OracleGLLoad> GetOracleGLSearchResponse(OracleGLLoadSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<OracleGLLoad, bool>> query =
                s =>
                    (searchRequest.OracleGLLoadId == 0 || searchRequest.OracleGLLoadId == s.OracleGLLoadId) &&
                    (searchRequest.StatusId == 0 || searchRequest.StatusId == s.StatusId);

            IEnumerable<OracleGLLoad> data = searchRequest.IsAsc
                ? DbSet
                    .Include(x=>x.Loads)
                    .Where(query)
                    .OrderBy(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet
                    .Include(x => x.Loads)
                    .Where(query)
                    .OrderByDescending(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new SearchTemplateResponse<OracleGLLoad> { Data = data, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }
    }
}
