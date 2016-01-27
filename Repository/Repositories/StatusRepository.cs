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
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        #region Constructor
        public StatusRepository(IUnityContainer container) : base(container)
        {
        }
        #endregion

        #region Protected
        protected override IDbSet<Status> DbSet
        {
            get { return db.Statuses; }
        }
        #endregion

        #region Public
        public IEnumerable<DropDownModel> GetStatusesDropDown()
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
