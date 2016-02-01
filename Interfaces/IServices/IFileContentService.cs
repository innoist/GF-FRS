using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface IFileContentService
    {
        IEnumerable<FileContent> LoadAll();
        bool SaveFileContent(FileContent fileContent);
        void DeleteFileContent(long fileContentId);
    }
}
