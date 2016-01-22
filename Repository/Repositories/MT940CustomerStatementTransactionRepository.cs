using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class MT940CustomerStatementTransactionRepository : BaseRepository<MT940CustomerStatementTransaction>, IMT940CustomerStatementTransactionRepository
    {
        public MT940CustomerStatementTransactionRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MT940CustomerStatementTransaction> DbSet
        {
            get { return db.MT940CustomerStatementTransactions; }
        }
    }
}
