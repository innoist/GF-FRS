using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class MT940CustomerStatementRepository : BaseRepository<MT940CustomerStatement>, IMT940CustomerStatementRepository
    {
        public MT940CustomerStatementRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MT940CustomerStatement> DbSet
        {
            get { return db.MT940CustomerStatements; }
        }
    }
}
