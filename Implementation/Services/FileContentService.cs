using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;

namespace FRS.Implementation.Services
{
    public class FileContentService : IFileContentService
    {
        #region Private

        private readonly IFileContentRepository fileContentRepository;

        private void SetProperties(FileContent metaData, FileContent dbVersion)
        {
            dbVersion.FileContentId = metaData.FileContentId;
            dbVersion.FileContentBase64 = metaData.FileContentBase64;
            dbVersion.Description = metaData.Description;
        }
        #endregion

        #region Constructor

        public FileContentService(IFileContentRepository fileContentRepository)
        {
            this.fileContentRepository = fileContentRepository;
        }

        #endregion

        #region Public

        public IEnumerable<FileContent> GetAll()
        {
            return fileContentRepository.GetAll();
        }

        public bool SaveFileContent(FileContent fileContent)
        {
            FileContent dbVersion = fileContentRepository.Find(fileContent.FileContentId);
            if (dbVersion != null)
            {
                SetProperties(fileContent, dbVersion);
                fileContentRepository.Update(dbVersion);
            }
            else
            {
                dbVersion = new FileContent();
                SetProperties(fileContent, dbVersion);
                fileContentRepository.Add(dbVersion);
            }
            fileContentRepository.SaveChanges();
            return true;
        }

        public void DeleteFileContent(long fileContentId)
        {
            var fileContent = fileContentRepository.Find(fileContentId);
            if (fileContent != null)
            {
                fileContentRepository.Delete(fileContent);
                fileContentRepository.SaveChanges();
            }
        }
        #endregion
    }
}
