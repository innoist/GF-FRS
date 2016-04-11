using System.Collections.Generic;
using FRS.WebApi.Models.MT940CustomerStatementTransaction;
using FRS.WebApi.Models.OracleGLEntry;
using FRS.WebApi.Models.ReconciledMapping;

namespace FRS.WebApi.ViewModels.ReconciledMapping
{
    public class ReconciliationViewModel
    {
        public int OracleGlEntryId { get; set; }
        public IEnumerable<int> TransactionIds { get; set; }
    }
    
    public class ReconciliationDetailViewModel
    {
        public OracleGLEntryModel OracleGlEntry { get; set; }
        public ReconciledMappingModel ReconciledMapping { get; set; }
        public List<MT940CustomerStatementTransactionModel> Transactions { get; set; }
    }

}