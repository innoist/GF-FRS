using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FRS.Interfaces.Repository;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class LoadTypeRepository : BaseRepository<LoadType>, ILoadTypeRepository
    {
        #region Constructor
        public LoadTypeRepository(IUnityContainer container) : base(container)
        {
        }
        #endregion

        #region Protected
        protected override IDbSet<LoadType> DbSet
        {
            get { return db.LoadTypes; }
        }
        #endregion

        #region Public
        public IEnumerable<DropDownModel> GetLoadTypesDropDown()
        {
            return DbSet.Select(x => new DropDownModel
            {
                Id = x.Value,
                Name = x.Name
            });
        }
        #endregion
    }
}
