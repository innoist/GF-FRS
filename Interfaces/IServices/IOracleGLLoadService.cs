using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface IOracleGLLoadService
    {
        OracleGLLoadDetailResponse GetOracleGlLoadDetailResponse(long oracleGlLoadId);
        SearchTemplateResponse<OracleGLLoad> GetOracleGLSearchResponse(OracleGLLoadSearchRequest searchRequest);
    }
}
