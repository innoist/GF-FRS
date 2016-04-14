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
    public class MT940CustomerStatementRepository : BaseRepository<MT940CustomerStatement>, IMT940CustomerStatementRepository
    {
        private readonly Dictionary<OrderByMT940Load, Func<MT940CustomerStatement, object>> orderClause =

            new Dictionary<OrderByMT940Load, Func<MT940CustomerStatement, object>>
            {
                {OrderByMT940Load.Id, c => c.MT940CustomerStatementId},
                {OrderByMT940Load.Sequence, c => c.Sequence},
                {OrderByMT940Load.LoadId, c => c.MT940LoadId},
                {OrderByMT940Load.AC, c => c.AccountNumber},
            };

        public MT940CustomerStatementRepository(IUnityContainer container) : base(container)
        {

        }

        protected override IDbSet<MT940CustomerStatement> DbSet
        {
            get { return db.MT940CustomerStatements; }
        }

        public SearchTemplateResponse<MT940CustomerStatement> GetMt940SearchResponse(MT940CustomerStatementSearchrequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<MT940CustomerStatement, bool>> query =
                s =>
                    (
                    (string.IsNullOrEmpty(searchRequest.AccountNumber) || searchRequest.AccountNumber.Equals(s.AccountNumber)) &&
                    (searchRequest.MT940LoadId == 0 || searchRequest.MT940LoadId == s.MT940LoadId)
                    );

            IEnumerable<MT940CustomerStatement> MT940Loads = searchRequest.IsAsc
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
            return new SearchTemplateResponse<MT940CustomerStatement> { Data = MT940Loads, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }



        public MT940CustomerStatement GetCustomerStatement(long mt940CustomerStatementId)
        {
            return DbSet.FirstOrDefault(x => x.MT940CustomerStatementId == mt940CustomerStatementId);
        }
    }
}
