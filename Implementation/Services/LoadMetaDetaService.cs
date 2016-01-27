using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class LoadMetaDetaService : ILoadMetaDataService
    {

        #region Private

        private readonly ILoadMetaDataRepository loadMetaDataRepository;
        private readonly ILoadTypeRepository loadTypeRepository;
        private readonly ISourceRepository sourceRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly IStatusRepository statusRepository;

        #endregion

        #region Constructor

        public LoadMetaDetaService(ILoadMetaDataRepository loadMetaDataRepository, ILoadTypeRepository loadTypeRepository, ISourceRepository sourceRepository, ICurrencyRepository currencyRepository, IStatusRepository statusRepository)
        {
            this.loadTypeRepository = loadTypeRepository;
            this.sourceRepository = sourceRepository;
            this.currencyRepository = currencyRepository;
            this.statusRepository = statusRepository;
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

        public void DeleteMetaData(long loadMetaDataId)
        {
            var metaData = loadMetaDataRepository.Find(loadMetaDataId);
            if (metaData != null)
            {
                loadMetaDataRepository.Delete(metaData);
                loadMetaDataRepository.SaveChanges();
            }
        }

        public BaseDataLoadMetaDataResponse GetBaseDataResponse()
        {
            return new BaseDataLoadMetaDataResponse
            {
                LoadMetaDatas = loadMetaDataRepository.GetAll(),
                LoadTypes = loadTypeRepository.GetLoadTypesDropDown(),
                Sources = sourceRepository.GetSourcesDropDown(),
                Currencies = currencyRepository.GetCurrenciesDropDown(),
                Statuses = statusRepository.GetStatusesDropDown()
            };
        }

        #endregion
    }
}
