using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class LoadStatusRepository : BaseRepository<LoadStatu>, ILoadStatusRepository
    {
        #region Constructor
        public LoadStatusRepository(IUnityContainer container)
            : base(container)
        {
        }
        #endregion

        #region Protected
        protected override IDbSet<LoadStatu> DbSet
        {
            get { return db.LoadStatus; }
        }
        #endregion

        #region Public
        
        #endregion
    }
}
