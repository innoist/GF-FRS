using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface IReconciledMappingService
    {
        ReconciledMappingResponse GetReconciledMappingResponse(long ReconciledMappingResponseId);
        SearchTemplateResponse<ReconciledMapping> GetReconciledMappingSearchResponse(ReconciledMappingSearchRequest searchRequest);

        IEnumerable<ReconciledMapping> GetAll();
        bool SaveReconciledMapping(ReconciledMapping reconciledMapping);
        bool SaveReconciledMappings(IEnumerable<ReconciledMapping> reconciledMapping);
        void DeleteReconciledMapping(long reconciledMappingId);
    }
}
