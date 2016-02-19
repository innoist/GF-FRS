﻿
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

using FRS.Models.LoggerModels;

namespace FRS.Repository.Repositories
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {


        private readonly Dictionary<OrderByLogs, Func<Log, object>> orderClause =

          new Dictionary<OrderByLogs, Func<Log, object>>
          {
                
                {OrderByLogs.Severity, c => c.Severity},
                {OrderByLogs.Timestamp, c => c.Timestamp},
                {OrderByLogs.Message, c => c.Message}
               
          };

        public LogRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Log> DbSet
        {
            get { return db.Logs; }
        }


        public SearchTemplateResponse<Log> SearchLogs(LogSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<Log, bool>> query =
                s =>
                    (
                    (String.IsNullOrEmpty(searchRequest.Severity)|| s.Severity.Contains(searchRequest.Severity)) &&
                    (searchRequest.Timestamp == null || DbFunctions.TruncateTime(searchRequest.Timestamp) == DbFunctions.TruncateTime(s.Timestamp)) &&
                     (String.IsNullOrEmpty(searchRequest.Message) || s.Message.Contains(searchRequest.Message))
                    );

            IEnumerable<Log> logData = searchRequest.IsAsc
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
            return new SearchTemplateResponse<Log> { Data = logData, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }



    }
}
