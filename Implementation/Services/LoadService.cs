using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class LoadService : ILoadService
    {
        #region Private

        private readonly ILoadRepository loadRepository;
        private readonly ILoadMetaDataRepository loadMetaDataRepository;

        #endregion

        #region Constructor

        public LoadService(ILoadRepository loadRepository, ILoadMetaDataRepository loadMetaDataRepository)
        {
            this.loadRepository = loadRepository;
            this.loadMetaDataRepository = loadMetaDataRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Loads
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Load> GetAll()
        {
            return loadRepository.GetAll();
        }

        /// <summary>
        /// Save New
        /// </summary>
        /// <param name="load"></param>
        /// <returns></returns>
        public bool AddLoad(Load load)
        {
            loadRepository.Add(load);
            loadRepository.SaveChanges();
            return true;
        }

        /// <summary>
        /// Update Existing
        /// </summary>
        /// <param name="load"></param>
        /// <returns></returns>
        public bool UpdateLoad(Load load)
        {
            loadRepository.Update(load);
            loadRepository.SaveChanges();
            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public void DeleteLoad(long loadId)
        {
            var load = loadRepository.Find(loadId);
            if (load != null)
            {
                loadRepository.Delete(load);
                loadRepository.SaveChanges();
            }
        }

        public MT940LoadBaseDataResponse GetBaseDataResponse()
        {
            MT940LoadBaseDataResponse response = new MT940LoadBaseDataResponse
            {
                Loads = loadRepository.GetAll().ToList(),
                LoadMetadataDropDown = loadMetaDataRepository.LoadMetadataDropDown()
            };
            return response;
        }

        public bool SaveLoad(Load load)
        {
            if (load.LoadId > 0)
            {
                loadRepository.Update(load);
                loadRepository.SaveChanges();
                return true;
            }
            loadRepository.Add(load);
            loadRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
