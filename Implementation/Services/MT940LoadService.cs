using System;
using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.IdentityModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class MT940LoadService : IMT940LoadService
    {
        #region Private

        private readonly IMT940LoadRepository mt940LoadRepository;

        #endregion

        #region Constructor

        public MT940LoadService(IMT940LoadRepository mt940LoadRepository)
        {
            this.mt940LoadRepository = mt940LoadRepository;
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
    }
}
