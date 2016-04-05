using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class OracleGLEntryService : IOracleGLEntryService
    {
        
        #region Private

        private readonly IOracleGLEntryRepository oracleGlEntryRepository;
        private readonly ILoadRepository loadRepository;

        #endregion

        #region Constructor

        public OracleGLEntryService(ILoadRepository loadRepository, IOracleGLEntryRepository oracleGlEntryRepository)
        {
            this.loadRepository = loadRepository;
            this.oracleGlEntryRepository = oracleGlEntryRepository;
        }

        #endregion

        public SearchTemplateResponse<OracleGLEntry> GetOracleGLEntrySearchResponse(OracleGLEntrySearchRequest searchRequest)
        {
            return oracleGlEntryRepository.GetOracleGLEntrySearchResponse(searchRequest);
        }

        public OracleGLEntry GetOracleGlEntry(long id)
        {
            return oracleGlEntryRepository.Find(id);
        }
    }
}
