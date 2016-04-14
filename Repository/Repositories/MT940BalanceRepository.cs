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
    public class MT940BalanceRepository : BaseRepository<MT940Balance>, IMT940BalanceRepository
    {

        private readonly Dictionary<OrderByMT940Balance, Func<MT940Balance, object>> orderClause =

            new Dictionary<OrderByMT940Balance, Func<MT940Balance, object>>
            {
                {OrderByMT940Balance.Id, c => c.MT940BalanceId},
                {OrderByMT940Balance.Type, c => c.DebitOrCredit},
                {OrderByMT940Balance.Entry, c => c.EntryDate},
                {OrderByMT940Balance.Value, c => c.Value},
            };

        public MT940BalanceRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MT940Balance> DbSet
        {
            get { return db.MT940Balances; }
        }

        public SearchTemplateResponse<MT940Balance> GetMt940BalanceSearchResponse(MT940BalanceSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<MT940Balance, bool>> query =
                s =>
                    (
                    (searchRequest.CurrencyId == 0 || searchRequest.CurrencyId == s.CurrencyId)
                    );

            IEnumerable<MT940Balance> list = searchRequest.IsAsc
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
            return new SearchTemplateResponse<MT940Balance> { Data = list, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }
    }
}
