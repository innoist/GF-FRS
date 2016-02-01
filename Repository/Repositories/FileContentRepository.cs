using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class FileContentRepository : BaseRepository<FileContent>, IFileContentRepository
    {
        public FileContentRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<FileContent> DbSet
        {
            get { return db.FileContents; }
        }
    }
}
