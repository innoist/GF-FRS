using System.Collections.Generic;
using FRS.WebApi.Models.MT940CustomerStatementTransaction;

namespace FRS.WebApi.ViewModels.MT940CustomerStatementTransaction
{
    public class MT940CustomerStatementTransactionLV
    {
        public MT940CustomerStatementTransactionLV()
        {
            Data = new List<MT940CustomerStatementTransactionModel>();
        }

        public IEnumerable<MT940CustomerStatementTransactionModel> Data { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}