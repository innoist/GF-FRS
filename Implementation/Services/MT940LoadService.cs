using System;
using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.IdentityModels;

namespace FRS.Implementation.Services
{
    public class MT940LoadService : IMT940LoadService
    {
        #region Private

        private readonly IUserRepository userRepository;
        private readonly IMT940LoadRepository mt940LoadRepository;

        private void UpdateMT940LoadProperties(MT940Load mT940Load, MT940Load dbVersion, AspNetUser user)
        {
            dbVersion.ModifiedBy = user.Id;
            dbVersion.ModifiedOn = DateTime.Now;
            dbVersion.MT940LoadId = mT940Load.MT940LoadId;
            dbVersion.Path = mT940Load.Path;
            dbVersion.FileName = mT940Load.FileName;
            dbVersion.FileExtension = mT940Load.FileExtension;
            dbVersion.StatusId = mT940Load.StatusId;
            dbVersion.FileContentId = mT940Load.FileContentId;
        }
        private void SetMT940LoadProperties(MT940Load mT940Load, MT940Load dbVersion, AspNetUser user)
        {
            dbVersion.CreatedBy = user.Id;
            dbVersion.CreatedOn = DateTime.Now;
            dbVersion.ModifiedBy = user.Id;
            dbVersion.ModifiedOn = DateTime.Now;
            dbVersion.MT940LoadId = mT940Load.MT940LoadId;
            dbVersion.Path = mT940Load.Path;
            dbVersion.FileName = mT940Load.FileName;
            dbVersion.FileExtension = mT940Load.FileExtension;
            dbVersion.StatusId = mT940Load.StatusId;
            dbVersion.FileContentId = mT940Load.FileContentId;
        }
        #endregion

        #region Constructor

        public MT940LoadService(IUserRepository userRepository, IMT940LoadRepository mt940LoadRepository)
        {
            this.userRepository = userRepository;
            this.mt940LoadRepository = mt940LoadRepository;
        }

        #endregion

        public IEnumerable<MT940Load> GetAll()
        {
            return mt940LoadRepository.GetAll();
        }

        public bool SaveMT940Load(MT940Load mt940Load)
        {
            var user = userRepository.GetLoggedInUser();
            var dbVersion = mt940LoadRepository.Find(mt940Load.MT940LoadId);
            if (dbVersion != null)
            {
                UpdateMT940LoadProperties(mt940Load, dbVersion, user);
                mt940LoadRepository.Update(dbVersion);
            }
            else
            {
                dbVersion = new MT940Load();
                SetMT940LoadProperties(mt940Load, dbVersion, user);
                mt940LoadRepository.Add(mt940Load);
            }
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
