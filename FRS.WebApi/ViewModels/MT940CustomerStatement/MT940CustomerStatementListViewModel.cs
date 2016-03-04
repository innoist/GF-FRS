using System.Collections.Generic;
using FRS.WebApi.Models.MT940CustomerStatement;

namespace FRS.WebApi.ViewModels.MT940CustomerStatement
{
    public class MT940CustomerStatementListViewModel
    {
        public MT940CustomerStatementListViewModel()
        {
            Data = new List<MT940CustomerStatementModel>();
        }

        public IEnumerable<MT940CustomerStatementModel> Data { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}