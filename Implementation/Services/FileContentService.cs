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
            fileContentRepository.Add(fileContent);
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
