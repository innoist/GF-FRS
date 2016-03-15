using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IOracleGLLoadRepository : IBaseRepository<OracleGLLoad, long>
    {

        SearchTemplateResponse<OracleGLLoad> GetOracleGLSearchResponse(OracleGLLoadSearchRequest searchRequest); 


    }
}
