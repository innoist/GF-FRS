using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class ReconciledMappingService : IReconciledMappingService
    {
        #region Private

        private readonly IReconciledMappingRepository rcRepository;
        //private readonly ILoadMetaDataRepository loadMetaDataRepository;

        #endregion

        #region Constructor

        public ReconciledMappingService(IReconciledMappingRepository mcRepository)
        {
            this.rcRepository = mcRepository;
        }

        #endregion

        public IEnumerable<ReconciledMapping> GetAll()
        {
            return rcRepository.GetAll();
        }



        public bool SaveReconciledMapping(ReconciledMapping reconciledMapping)
        {
            rcRepository.Add(reconciledMapping);
            rcRepository.SaveChanges();
            return true;
        }

        public void DeleteReconciledMapping(long reconciledMappingId)
        {
            var reconciledMapping = rcRepository.Find(reconciledMappingId);
            if (reconciledMapping != null)
            {
                rcRepository.Delete(reconciledMapping);
                rcRepository.SaveChanges();
            }
        }

        //public MT940LoadDetailResponse GetMt940LoadDetail(long mt940LoadId)
        //{
        //    MT940LoadDetailResponse response = new MT940LoadDetailResponse
        //    {
        //        Load = loadRepository.GetLoad(mt940LoadId),
        //        Mt940Load = mt940LoadRepository.Find(mt940LoadId)
                
        //    };
        //    response.LoadMetaData = loadMetaDataRepository.GetMetaData(response.Load.LoadMetaDataId);

        //    return response;
        //}

        public ReconciledMappingResponse GetReconciledMappingResponse(long ReconciledMappingResponseId)
        {
            throw new System.NotImplementedException();
        }

        public SearchTemplateResponse<ReconciledMapping> GetReconciledMappingSearchResponse(ReconciledMappingSearchRequest searchRequest)
        {
            return rcRepository.GetReconciledMappingSearchResponse(searchRequest);
        }
    }
}
