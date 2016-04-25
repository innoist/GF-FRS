using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.Resources;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IReconciledMappingRepository : IBaseRepository<ReconciledMapping, long>
    {

        SearchTemplateResponse<ReconciledMapping> GetReconciledMappingSearchResponse(ReconciledMappingSearchRequest searchRequest);
        bool SaveMappings(IEnumerable<ReconciledMapping> mappings);

        IEnumerable<MT940CustomerStatementTransaction> GetReconciledMappings(long oracleEntryId);

    }
}
