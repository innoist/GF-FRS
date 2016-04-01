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
    public class ServiceLogRepository : BaseRepository<ServiceLog>, IServiceLogRepository
    {
        private readonly Dictionary<OrderByServiceLogs, Func<ServiceLog, object>> orderClause =

          new Dictionary<OrderByServiceLogs, Func<ServiceLog, object>>
          {
                {OrderByServiceLogs.Message, c => c.Message}
               
          };

        public ServiceLogRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ServiceLog> DbSet
        {
            get { return db.ServiceLogs; }
        }


        public SearchTemplateResponse<ServiceLog> SearchLogs(ServiceLogSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<ServiceLog, bool>> query =
                s =>
                    (
                     (String.IsNullOrEmpty(searchRequest.Message) || s.Message.Contains(searchRequest.Message))
                    );

            IEnumerable<ServiceLog> logData = searchRequest.IsAsc
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
            return new SearchTemplateResponse<ServiceLog> { Data = logData, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }
    }
}
