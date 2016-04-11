using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class ReconciledMappingResponse
    {
        public ReconciledMapping ReconciledMapping { get; set; }
        public OracleGLEntry OracleGlEntry { get; set; }
        public IEnumerable<MT940CustomerStatementTransaction> Transactions { get; set; }
    }
}
