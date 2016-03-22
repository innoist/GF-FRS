using System.Collections;
using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IReconciledMappingRepository : IBaseRepository<ReconciledMapping, long>
    {

        SearchTemplateResponse<ReconciledMapping> GetReconciledMappingSearchResponse(ReconciledMappingSearchRequest searchRequest);
        IEnumerable<ReconciledMapping> GetAll();

    }
}
