using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class MT940LoadDetailRepository : BaseRepository<MT940LoadDetail>, IMT940LoadDetailRepository
    {
        public MT940LoadDetailRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MT940LoadDetail> DbSet
        {
            get { return db.MT940LoadDetail; }
        }
    }
}
