using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;

namespace FRS.Implementation.Services
{
    public class LoadService : ILoadService
    {
        #region Private

        private readonly ILoadRepository loadRepository;

        #endregion

        #region Constructor

        public LoadService(ILoadRepository loadRepository)
        {
            this.loadRepository = loadRepository;
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
        public bool SaveLoad(Load load)
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
        /// <param name="load"></param>
        public void DeleteLoad(Load load)
        {
            loadRepository.Delete(load);
            loadRepository.SaveChanges();
        }
        
        #endregion
    }
}
