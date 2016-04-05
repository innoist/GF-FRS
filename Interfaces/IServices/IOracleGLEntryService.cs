using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface IOracleGLEntryService
    {
        SearchTemplateResponse<OracleGLEntry> GetOracleGLEntrySearchResponse(OracleGLEntrySearchRequest searchRequest);
        OracleGLEntry GetOracleGlEntry(long id);
    }
}
