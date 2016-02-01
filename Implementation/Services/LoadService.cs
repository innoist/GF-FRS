using System;
using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.IdentityModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class LoadService : ILoadService
    {
        #region Private

        private readonly IUserRepository userRepository;
        private readonly ILoadRepository loadRepository;
        private readonly ILoadMetaDataRepository loadMetaDataRepository;
        private readonly IFileContentService fileContentService;

        private void UpdateProperties(Load metaData, Load dbVersion, AspNetUser user)
        {
            dbVersion.ModifiedBy = user.Id;
            dbVersion.ModifiedOn = DateTime.Now;
            dbVersion.LoadId = metaData.LoadId;
            //dbVersion. = metaData.SourceId;
            //dbVersion.Header = metaData.Header;
            //dbVersion.Footer = metaData.Footer;
            //dbVersion.Name = metaData.Name;
            //dbVersion.CurrencyId = metaData.CurrencyId;
            //dbVersion.Description = metaData.Description;
            //dbVersion.StatusId = metaData.StatusId;
        }
        private void SetProperties(Load metaData, Load dbVersion, AspNetUser user)
        {
            dbVersion.CreatedBy = user.Id;
            dbVersion.CreatedOn = DateTime.Now;
            dbVersion.ModifiedBy = user.Id;
            dbVersion.ModifiedOn = DateTime.Now;
            //dbVersion.LoadTypeId = metaData.LoadTypeId;
            //dbVersion.SourceId = metaData.SourceId;
            //dbVersion.Header = metaData.Header;
            //dbVersion.Footer = metaData.Footer;
            //dbVersion.Name = metaData.Name;
            //dbVersion.CurrencyId = metaData.CurrencyId;
            //dbVersion.Description = metaData.Description;
            //dbVersion.StatusId = metaData.StatusId;
        }
        private MT940Load SetMT940LoadProperties(AspNetUser user, Load load, long fileContentId)
        {
            return new MT940Load
            {
                CreatedBy = user.Id,
                CreatedOn = DateTime.Now,
                ModifiedBy = user.Id,
                ModifiedOn = DateTime.Now,
                MT940LoadId = 0,
                Path = "",
                FileName = load.MT940Load.FileName,
                FileExtension = load.MT940Load.FileExtension,
                StatusId = 0,
                FileContentId = fileContentId,
            };
        }
        #endregion

        #region Constructor

        public LoadService(ILoadRepository loadRepository, ILoadMetaDataRepository loadMetaDataRepository, IUserRepository userRepository, IFileContentService fileContentService)
        {
            this.loadRepository = loadRepository;
            this.loadMetaDataRepository = loadMetaDataRepository;
            this.userRepository = userRepository;
            this.fileContentService = fileContentService;
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
            var user = userRepository.GetLoggedInUser();
            var dbVersion = loadRepository.Find(load.LoadId);
            if (dbVersion != null)
            {
                UpdateProperties(load, dbVersion, user);
                loadRepository.Update(dbVersion);
            }
            else
            {
                dbVersion = new Load();
                // save file in FileContent
                FileContent fileContent = new FileContent
                {
                    FileContentId = load.MT940Load.FileContentId,
                    FileContentBase64 = load.MT940Load.FileContent.FileContentBase64,
                    Description = load.MT940Load.FileContent.Description
                };
                fileContentService.SaveFileContent(fileContent);
                // add MT940Load
                MT940Load mt940Load = SetMT940LoadProperties(user, load, fileContent.FileContentId);

                SetProperties(load, dbVersion, user);
                loadRepository.Add(dbVersion);
            }
            loadRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
