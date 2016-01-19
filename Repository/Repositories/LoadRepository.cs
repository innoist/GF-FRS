using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class LoadRepository : BaseRepository<Load>, ILoadRepository
    {
        public LoadRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Load> DbSet
        {
            get { return db.Loads; }
        }
    }
}
