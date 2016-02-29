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
    public class SourceRepository : BaseRepository<Source>, ISourceRepository
    {
        #region Constructor
        public SourceRepository(IUnityContainer container) : base(container)
        {
        }
        #endregion

        #region Protected
        protected override IDbSet<Source> DbSet
        {
            get { return db.Sources; }
        }
        #endregion

        #region Public
        public IEnumerable<DropDownModel> GetSourcesDropDown()
        {
            return DbSet.Select(x => new DropDownModel
            {
                Id = x.Value,
                Name = x.Name
            });
        }
        
        public IEnumerable<Source> GetSources()
        {
            return DbSet.Include(x => x.Status).Select(x => x);
        }
        #endregion
    }
}
