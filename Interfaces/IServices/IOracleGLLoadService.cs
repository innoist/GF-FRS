using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface IOracleGLLoadService
    {
        //IEnumerable<OracleGLLoad> GetAll();
        SearchTemplateResponse<OracleGLLoad> GetOracleGLSearchResponse(OracleGLLoadSearchRequest searchRequest);
    }
}
