using System;
using System.Collections.Generic;
using System.Security.Claims;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.Common;
using FRS.Models.DomainModels;
using FRS.Models.IdentityModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

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

        private void UpdateProperties(LoadMetaData metaData, LoadMetaData dbVersion, AspNetUser user)
        {
            dbVersion.ModifiedBy = user.Id;
            dbVersion.ModifiedOn = DateTime.Now;
            dbVersion.LoadTypeId = metaData.LoadTypeId;
            dbVersion.SourceId = metaData.SourceId;
            dbVersion.Header = metaData.Header;
            dbVersion.Footer = metaData.Footer;
            dbVersion.Name = metaData.Name;
            dbVersion.CurrencyId = metaData.CurrencyId;
            dbVersion.Description = metaData.Description;
            dbVersion.StatusId = metaData.StatusId;
        }
        private void SetProperties(LoadMetaData metaData, LoadMetaData dbVersion, AspNetUser user)
        {
            dbVersion.CreatedBy = user.Id;
            dbVersion.CreatedOn = DateTime.Now;
            dbVersion.ModifiedBy = user.Id;
            dbVersion.ModifiedOn = DateTime.Now;
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

        public LoadMetaData SaveMetaData(LoadMetaData loadMetaData)
        {
            var user = userRepository.GetLoggedInUser();
            LoadMetaData dbVersion = loadMetaDataRepository.Find(loadMetaData.LoadMetaDataId);
            if (dbVersion != null)
            {
                UpdateProperties(loadMetaData, dbVersion, user);
                loadMetaDataRepository.Update(dbVersion);
            }
            else
            {
                dbVersion = new LoadMetaData();
                SetProperties(loadMetaData, dbVersion, user);
                loadMetaDataRepository.Add(dbVersion);
            }
            loadMetaDataRepository.SaveChanges();
            return dbVersion;
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
