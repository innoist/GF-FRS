using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;

namespace FRS.Implementation.Services
{
    public class LoadMetaDetaService : ILoadMetaDataService
    {

        #region Private

        private readonly ILoadMetaDataRepository loadMetaDataRepository;

        #endregion

        #region Constructor

        public LoadMetaDetaService(ILoadMetaDataRepository loadMetaDataRepository)
        {
            this.loadMetaDataRepository = loadMetaDataRepository;
        }

        #endregion

        #region Public

        public IEnumerable<LoadMetaData> GetAll()
        {
            return loadMetaDataRepository.GetAll();
        }

        public bool SaveMetaData(LoadMetaData loadMetaData)
        {
            loadMetaDataRepository.Add(loadMetaData);
            loadMetaDataRepository.SaveChanges();
            return true;
        }

        public bool UpdateMetaData(LoadMetaData loadMetaData)
        {
            loadMetaDataRepository.Update(loadMetaData);
            loadMetaDataRepository.SaveChanges();
            return true;
        }

        public void DeleteMetaData(LoadMetaData loadMetaData)
        {
            loadMetaDataRepository.Delete(loadMetaData);
            loadMetaDataRepository.SaveChanges();
        }

        #endregion
    }
}
