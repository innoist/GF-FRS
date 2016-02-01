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
        private readonly IMT940LoadService mt940LoadService;

        private void UpdateProperties(Load load, Load dbVersion, AspNetUser user)
        {
            dbVersion.ModifiedBy = user.Id;
            dbVersion.ModifiedOn = DateTime.Now;
            dbVersion.LoadId = load.LoadId;
            dbVersion.LoadMetaDataId = load.LoadMetaDataId;
            dbVersion.MT940LoadId = load.MT940LoadId;
            dbVersion.Start = DateTime.Now;
            dbVersion.InProgress = true;
            dbVersion.ReadOnly = true;
        }
        private void SetProperties(Load load, Load dbVersion, AspNetUser user)
        {
            dbVersion.CreatedBy = user.Id;
            dbVersion.CreatedOn = DateTime.Now;
            dbVersion.ModifiedBy = user.Id;
            dbVersion.ModifiedOn = DateTime.Now;
            dbVersion.LoadId = load.LoadId;
            dbVersion.LoadMetaDataId = load.LoadMetaDataId;
            dbVersion.MT940LoadId = load.MT940LoadId;
            dbVersion.Start = DateTime.Now;
            dbVersion.InProgress = true;
            dbVersion.ReadOnly = true;
        }
        private MT940Load SetMT940LoadProperties(Load load, long fileContentId)
        {
            return new MT940Load
            {
                FileName = load.MT940Load.FileName,
                FileExtension = load.MT940Load.FileExtension,
                FileContentId = fileContentId,
            };
        }
        #endregion

        #region Constructor

        public LoadService(ILoadRepository loadRepository, ILoadMetaDataRepository loadMetaDataRepository, IUserRepository userRepository, IFileContentService fileContentService, IMT940LoadService mt940LoadService)
        {
            this.loadRepository = loadRepository;
            this.loadMetaDataRepository = loadMetaDataRepository;
            this.userRepository = userRepository;
            this.fileContentService = fileContentService;
            this.mt940LoadService = mt940LoadService;
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
                if (fileContentService.SaveFileContent(fileContent))
                {
                    // add MT940Load
                    MT940Load mt940Load = SetMT940LoadProperties(load, fileContent.FileContentId);
                    if (mt940LoadService.SaveMT940Load(mt940Load))
                    {
                        load.MT940LoadId = mt940Load.MT940LoadId;
                        SetProperties(load, dbVersion, user);
                        loadRepository.Add(dbVersion);
                        loadRepository.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
