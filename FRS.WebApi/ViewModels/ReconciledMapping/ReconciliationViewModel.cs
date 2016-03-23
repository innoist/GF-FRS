using System.Collections;
using System.Collections.Generic;

namespace FRS.WebApi.ViewModels.ReconciledMapping
{
    public class ReconciliationViewModel
    {
        public int OracleGlEntryId { get; set; }
        public IEnumerable<int> TransactionIds { get; set; }
    }
}