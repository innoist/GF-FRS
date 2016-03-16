using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class FiscalYearRepository : BaseRepository<FiscalYear>, IFiscalYearRepository
    {
        #region Constructor
        public FiscalYearRepository(IUnityContainer container)
            : base(container)
        {
        }
        #endregion

        #region Protected
        protected override IDbSet<FiscalYear> DbSet
        {
            get { return db.FiscalYears; }
        }
        #endregion

        #region Public
        
        #endregion
    }
}
