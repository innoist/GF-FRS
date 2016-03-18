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
        private readonly ILoadRepository loadRepository;

        #endregion

        #region Constructor

        public OracleGLLoadService(IOracleGLLoadRepository oracleGlLoadRepository, ILoadRepository loadRepository)
        {
            this.oracleGlLoadRepository = oracleGlLoadRepository;
            this.loadRepository = loadRepository;
        }

        #endregion

        public OracleGLLoadDetailResponse GetOracleGlLoadDetailResponse(long oracleGlLoadId)
        {
            OracleGLLoadDetailResponse response = new OracleGLLoadDetailResponse()
            {
                OracleGlLoad = oracleGlLoadRepository.Find(oracleGlLoadId),
                Load = loadRepository.GetLoadByOracleGlId(oracleGlLoadId)
            };

            return response;

        }

        public SearchTemplateResponse<OracleGLLoad> GetOracleGLSearchResponse(OracleGLLoadSearchRequest searchRequest)
        {
            return oracleGlLoadRepository.GetOracleGLSearchResponse(searchRequest);
        }
    }
}
