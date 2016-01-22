using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class MT940BalanceRepository : BaseRepository<MT940Balance>, IMT940BalanceRepository
    {
        public MT940BalanceRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MT940Balance> DbSet
        {
            get { return db.MT940Balances; }
        }
    }
}
