using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class OracleGLLoadService : IOracleGLLoadService
    {
        
        #region Private

        private readonly IOracleGLLoadRepository oracleGlLoadRepository;

        #endregion

        #region Constructor

        public OracleGLLoadService(IOracleGLLoadRepository oracleGlLoadRepository)
        {
            this.oracleGlLoadRepository = oracleGlLoadRepository;
        }

        #endregion

        public SearchTemplateResponse<OracleGLLoad> GetOracleGLSearchResponse(OracleGLLoadSearchRequest searchRequest)
        {
            return oracleGlLoadRepository.GetOracleGLSearchResponse(searchRequest);
        }
    }
}
