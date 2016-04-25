using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FRS.Interfaces.Repository;
using FRS.Models.Common;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.Resources;
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
                {OrderByReconciledMapping.AccountNumber, c => c.OracleGLEntry.AccountNumber},
                {OrderByReconciledMapping.TransactionDate, c => c.CreatedOn},
                {OrderByReconciledMapping.Amount, c => c.MT940CustomerStatementTransaction.Amount},
                
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
            decimal value = 0;
             decimal.TryParse(searchRequest.Amount, out value);
            Expression<Func<ReconciledMapping, bool>> query =
                s =>
                    (
                        (searchRequest.TransactDate == null ||
                         DbFunctions.TruncateTime(searchRequest.TransactDate.Value) ==
                         DbFunctions.TruncateTime(s.OracleGLEntry.EffectiveDate)) && 
                         (string.IsNullOrEmpty(searchRequest.Amount) ||value == s.MT940CustomerStatementTransaction.Amount));

            IEnumerable<ReconciledMapping> ReconciledMappings = searchRequest.IsAsc
              ? DbSet
                  .Include(x => x.MT940CustomerStatementTransaction)
                  .Include(x => x.OracleGLEntry)
                  .Where(query)
                  .OrderBy(orderClause[searchRequest.OrderByColumn])
                  .GroupBy(x => new { x.Identifier})
                  .Select(x => new ReconciledMapping
                  {
                      Identifier = x.Key.Identifier,
                      ModifiedOn = x.First().ModifiedOn,
                      OracleGLEntryId = x.First().OracleGLEntryId,
                      IsManual = x.First().IsManual,
                      TransactionsCount = x.Count(),
                      TransactionAmount = x.Sum(y=>y.MT940CustomerStatementTransaction.Amount),
                      ReconciledMappingId = x.First().ReconciledMappingId,
                      CreatedBy = x.First().CreatedByRef.FirstName,
                      CreatedOn = x.First().CreatedOn,
                      IsDeleted = x.First().IsDeleted,
                      OracleGLEntry = x.First().OracleGLEntry
                  })
                  

                  .Skip(fromRow)
                  .Take(toRow)
                  .ToList()
              : DbSet
                  .Include(x => x.MT940CustomerStatementTransaction)
                  .Include(x => x.OracleGLEntry)
                  .Where(query)
                  .OrderByDescending(orderClause[searchRequest.OrderByColumn])
                  .GroupBy(x => new { x.Identifier })
                  .Select(x => new ReconciledMapping
                  {
                      Identifier = x.Key.Identifier,
                      ModifiedOn = x.First().ModifiedOn,
                      OracleGLEntryId = x.First().OracleGLEntryId,
                      IsManual = x.First().IsManual,
                      TransactionsCount = x.Count(),
                      TransactionAmount = x.Sum(y => y.MT940CustomerStatementTransaction.Amount),
                      ReconciledMappingId = x.First().ReconciledMappingId,
                      CreatedBy = x.First().CreatedByRef.FirstName,
                      CreatedOn = x.First().CreatedOn,
                      IsDeleted = x.First().IsDeleted,
                      OracleGLEntry = x.First().OracleGLEntry
                  })
                  
                  .Skip(fromRow)
                  .Take(toRow)
                  .ToList();
            return new SearchTemplateResponse<ReconciledMapping> { Data = ReconciledMappings, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };


          
        }

        public bool SaveMappings(IEnumerable<ReconciledMapping> mappings)
        {
            db.ReconciledMappings.AddRange(mappings);
            db.SaveChanges();
            return true;
        }

        public IEnumerable<MT940CustomerStatementTransaction> GetReconciledMappings(long oracleEntryId)
        {
            IEnumerable<MT940CustomerStatementTransaction> transactions =
                DbSet.Include(x => x.MT940CustomerStatementTransaction)
                    .Where(x => x.OracleGLEntryId == oracleEntryId)
                    .Select(x => x.MT940CustomerStatementTransaction);
            return transactions;
        }
    }
}
