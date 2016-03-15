using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class MT940LoadService : IMT940LoadService
    {
        #region Private

        private readonly IMT940LoadRepository mt940LoadRepository;
        private readonly ILoadRepository loadRepository;
        private readonly ILoadMetaDataRepository loadMetaDataRepository;

        #endregion

        #region Constructor

        public MT940LoadService(IMT940LoadRepository mt940LoadRepository, ILoadMetaDataRepository loadMetaDataRepository, ILoadRepository loadRepository)
        {
            this.mt940LoadRepository = mt940LoadRepository;
            this.loadMetaDataRepository = loadMetaDataRepository;
            this.loadRepository = loadRepository;
        }

        #endregion

        public IEnumerable<MT940Load> GetAll()
        {
            return mt940LoadRepository.GetAll();
        }

        public SearchTemplateResponse<MT940Load> GetMt940SearchResponse(MT940LoadSearchRequest searchRequest)
        {
            return mt940LoadRepository.GetMt940SearchResponse(searchRequest);
        }

        public bool SaveMT940Load(MT940Load mt940Load)
        {
            mt940LoadRepository.Add(mt940Load);
            mt940LoadRepository.SaveChanges();
            return true;
        }

        public void DeleteMT940Load(long mt940LoadId)
        {
            var mt940Load = mt940LoadRepository.Find(mt940LoadId);
            if (mt940Load != null)
            {
                mt940LoadRepository.Delete(mt940Load);
                mt940LoadRepository.SaveChanges();
            }
        }

        public MT940LoadDetailResponse GetMt940LoadDetail(long mt940LoadId)
        {
            MT940LoadDetailResponse response = new MT940LoadDetailResponse
            {
                Load = loadRepository.GetLoad(mt940LoadId),
                Mt940Load = mt940LoadRepository.Find(mt940LoadId)
                
            };
            response.LoadMetaData = loadMetaDataRepository.GetMetaData(response.Load.LoadMetaDataId);

            return response;
        }
    }
}
