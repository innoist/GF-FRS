using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class MT940LoadRepository : BaseRepository<MT940Load>, IMT940LoadRepository
    {
        public MT940LoadRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MT940Load> DbSet
        {
            get { return db.MT940Loads; }
        }
    }
}
