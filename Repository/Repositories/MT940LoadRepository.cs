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
    public class MT940LoadRepository : BaseRepository<MT940Load>, IMT940LoadRepository
    {
        private readonly Dictionary<OrderByMT940Load, Func<MT940Load, object>> orderClause =

            new Dictionary<OrderByMT940Load, Func<MT940Load, object>>
            {
                {OrderByMT940Load.Id, c => c.MT940LoadId},
            };
        public MT940LoadRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MT940Load> DbSet
        {
            get { return db.MT940Loads; }
        }


        public SearchTemplateResponse<MT940Load> GetMt940SearchResponse(MT940LoadSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<MT940Load, bool>> query =
                s =>
                    (
                    (searchRequest.MT940LoadId == 0 || searchRequest.MT940LoadId == (s.MT940LoadId)) &&
                    (searchRequest.StatusId == 0 || searchRequest.StatusId == s.StatusId)
                    );

            IEnumerable<MT940Load> MT940Loads = searchRequest.IsAsc
                ? DbSet
                    .Include(x=>x.Loads)
                    .Include(x=>x.Status)
                    .Where(query)
                    .OrderBy(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet
                    .Include(x => x.Loads)
                    .Include(x => x.Status)
                    .Where(query)
                    .OrderByDescending(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new SearchTemplateResponse<MT940Load> { Data = MT940Loads, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }
    }
}
