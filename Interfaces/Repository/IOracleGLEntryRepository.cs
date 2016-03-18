using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IOracleGLEntryRepository : IBaseRepository<OracleGLEntry, long>
    {

        SearchTemplateResponse<OracleGLEntry> GetOracleGLEntrySearchResponse(OracleGLEntrySearchRequest searchRequest); 


    }
}
