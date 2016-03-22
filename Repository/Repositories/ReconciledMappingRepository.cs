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
    public class ReconciledMappingRepository : BaseRepository<ReconciledMapping>, IReconciledMappingRepository
    {
        private readonly Dictionary<OrderByReconciledMapping, Func<ReconciledMapping, object>> orderClause =

            new Dictionary<OrderByReconciledMapping, Func<ReconciledMapping, object>>
            {
                {OrderByReconciledMapping.AccountDate, c => c.OracleGLEntry.EffectiveDate},
                {OrderByReconciledMapping.AccountNumber, c => c.OracleGLEntry.AccountNumber},
                {OrderByReconciledMapping.TransactionDate, c => c.MT940CustomerStatementTransaction.EntryDate},
                {OrderByReconciledMapping.Amount, c => c.MT940CustomerStatementTransaction.Amount},
                {OrderByReconciledMapping.DebitOrCredit, c => c.MT940CustomerStatementTransaction.DebitOrCredit}
                
            };
        public ReconciledMappingRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<ReconciledMapping> DbSet
        {
            get { return db.ReconciledMappings; }
        }

        public SearchTemplateResponse<ReconciledMapping> GetReconciledMappingSearchResponse(ReconciledMappingSearchRequest searchRequest)
        {
            
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;

            Expression<Func<ReconciledMapping, bool>> query =
            s =>
                (
                (searchRequest.AccountDate == null || searchRequest.AccountDate.Value == s.OracleGLEntry.EffectiveDate) &&
                (searchRequest.TransactDate == null || searchRequest.TransactDate.Value== s.MT940CustomerStatementTransaction.EntryDate)
                );

            IEnumerable<ReconciledMapping> ReconciledMappings = searchRequest.IsAsc
              ? DbSet
                  .Include(x => x.MT940CustomerStatementTransaction)
                  .Include(x => x.OracleGLEntry)
                  .Where(query)
                  .OrderBy(orderClause[searchRequest.OrderByColumn])
                  .Skip(fromRow)
                  .Take(toRow)
                  .ToList()
              : DbSet
                  .Include(x => x.MT940CustomerStatementTransaction)
                  .Include(x => x.OracleGLEntry)
                  .Where(query)
                  .OrderByDescending(orderClause[searchRequest.OrderByColumn])
                  .Skip(fromRow)
                  .Take(toRow)
                  .ToList();
            return new SearchTemplateResponse<ReconciledMapping> { Data = ReconciledMappings, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };


          
        }
      

       
    }
}
