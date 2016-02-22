using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.Common;
using FRS.Models.DomainModels;
using FRS.Models.IdentityModels;
using FRS.Models.RequestModels;
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
        private readonly IUserRepository userRepository;

        private void UpdateProperties(LoadMetaData metaData, LoadMetaData dbVersion)
        {
            dbVersion.ModifiedBy = metaData.ModifiedBy;
            dbVersion.ModifiedOn = metaData.ModifiedOn;
            dbVersion.LoadTypeId = metaData.LoadTypeId;
            dbVersion.SourceId = metaData.SourceId;
            dbVersion.Header = metaData.Header;
            dbVersion.Footer = metaData.Footer;
            dbVersion.Name = metaData.Name;
            dbVersion.CurrencyId = metaData.CurrencyId;
            dbVersion.Description = metaData.Description;
            dbVersion.StatusId = metaData.StatusId;
        }
        private void SetProperties(LoadMetaData metaData, LoadMetaData dbVersion)
        {
            dbVersion.CreatedBy = metaData.CreatedBy;
            dbVersion.CreatedOn = metaData.CreatedOn;
            dbVersion.ModifiedBy = metaData.ModifiedBy;
            dbVersion.ModifiedOn = metaData.ModifiedOn;
            dbVersion.LoadTypeId = metaData.LoadTypeId;
            dbVersion.SourceId = metaData.SourceId;
            dbVersion.Header = metaData.Header;
            dbVersion.Footer = metaData.Footer;
            dbVersion.Name = metaData.Name;
            dbVersion.CurrencyId = metaData.CurrencyId;
            dbVersion.Description = metaData.Description;
            dbVersion.StatusId = metaData.StatusId;
        }
        #endregion

        #region Constructor

        public LoadMetaDetaService(ILoadMetaDataRepository loadMetaDataRepository, ILoadTypeRepository loadTypeRepository, ISourceRepository sourceRepository, ICurrencyRepository currencyRepository, IStatusRepository statusRepository, IUserRepository userRepository)
        {
            this.loadTypeRepository = loadTypeRepository;
            this.sourceRepository = sourceRepository;
            this.currencyRepository = currencyRepository;
            this.statusRepository = statusRepository;
            this.userRepository = userRepository;
            this.loadMetaDataRepository = loadMetaDataRepository;
        }

        #endregion

        #region Public

        public IEnumerable<LoadMetaData> GetAll()
        {
            return loadMetaDataRepository.GetAll();
        }

        public LoadMetaData FindById(long loadMetadataId)
        {
            return loadMetaDataRepository.Find(loadMetadataId);
        }

        public bool AddMetaData(LoadMetaData loadMetaData)
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

        public BaseDataLoadMetaDataResponse GetBaseDataResponse(long ? id)
        {
            var toReturn = new BaseDataLoadMetaDataResponse
            {
                LoadTypes = loadTypeRepository.GetLoadTypesDropDown(),
                Sources = sourceRepository.GetSourcesDropDown(),
                Currencies = currencyRepository.GetCurrenciesDropDown(),
                Statuses = statusRepository.GetStatusesDropDown()
            };
            if (id != null)
            {
                toReturn.MetaData = loadMetaDataRepository.Find((long) id);
            }
            return toReturn;
        }

        public bool SaveMetaData(LoadMetaData loadMetaData)
        {
            //var user = userRepository.GetLoggedInUser();
            LoadMetaData dbVersion = loadMetaDataRepository.Find(loadMetaData.LoadMetaDataId);
            if (dbVersion != null)
            {
                UpdateProperties(loadMetaData, dbVersion);
                loadMetaDataRepository.Update(dbVersion);
            }
            else
            {
                dbVersion = new LoadMetaData();
                SetProperties(loadMetaData, dbVersion);
                loadMetaDataRepository.Add(dbVersion);
            }
            loadMetaDataRepository.SaveChanges();
            return true;
        }

        public LoadMetaDataForLoad IsLoadTypeMT940(long loadMetaDataId)
        {
            return loadMetaDataRepository.IsLoadTypeMT940(loadMetaDataId);
        }

        public SearchLoadMetaDataResponse SearchLoadMetaData(LoadMetaDataSearchRequest searchRequest)
        {
            return loadMetaDataRepository.SearchLoadMetaData(searchRequest);
        }

        #endregion
    }
}
