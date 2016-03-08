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
    public class MT940CustomerStatementTransactionRepository : BaseRepository<MT940CustomerStatementTransaction>, IMT940CustomerStatementTransactionRepository
    {

        private readonly Dictionary<OrderByCustomerStatementTransaction, Func<MT940CustomerStatementTransaction, object>> orderClause =

            new Dictionary<OrderByCustomerStatementTransaction, Func<MT940CustomerStatementTransaction, object>>
            {
                {OrderByCustomerStatementTransaction.Id, c => c.MT940CustomerStatementTransactionId},
            };

        public MT940CustomerStatementTransactionRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MT940CustomerStatementTransaction> DbSet
        {
            get { return db.MT940CustomerStatementTransactions; }
        }

        public SearchTemplateResponse<MT940CustomerStatementTransaction> GetMt940SearchResponse(MT940CustomerStatementTransactionSearchrequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<MT940CustomerStatementTransaction, bool>> query =
                s =>
                    (
                    (string.IsNullOrEmpty(searchRequest.DebitOrCredit) || searchRequest.DebitOrCredit.Equals(s.DebitOrCredit))
                    );

            IEnumerable<MT940CustomerStatementTransaction> list = searchRequest.IsAsc
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
            return new SearchTemplateResponse<MT940CustomerStatementTransaction> { Data = list, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }
    }
}
