using System;
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
        private readonly IFileContentService fileContentService;
        private readonly IMT940LoadService mt940LoadService;

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

        public LoadService(ILoadRepository loadRepository, ILoadMetaDataRepository loadMetaDataRepository, IFileContentService fileContentService, IMT940LoadService mt940LoadService)
        {
            this.loadRepository = loadRepository;
            this.loadMetaDataRepository = loadMetaDataRepository;
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
            // save file in FileContent
            //if (fileContentService.SaveFileContent(load.MT940Load.FileContent))
            //{
            //    // add MT940Load
            //    //MT940Load mt940Load = new MT940Load
            //    //{
            //    //    CreatedBy = load.CreatedBy,
            //    //    ModifiedBy = load.ModifiedBy,
            //    //    FileName = load.MT940Load.FileName,
            //    //    FileExtension = load.MT940Load.FileExtension
            //    //};
            //    if (mt940LoadService.SaveMT940Load(load.MT940Load))
            //    {
            //        loadRepository.Add(load);
            //        loadRepository.SaveChanges();
            //        return true;
            //    }
            //}
            
            try
            {
                loadRepository.Add(load);
                loadRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return true;
        }

        #endregion
    }
}
