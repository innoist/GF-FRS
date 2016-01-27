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
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        #region Constructor
        public CurrencyRepository(IUnityContainer container) : base(container)
        {
        }
        #endregion

        #region Protected
        protected override IDbSet<Currency> DbSet
        {
            get { return db.Currencies; }
        }
        #endregion

        #region Public
        public IEnumerable<DropDownModel> GetCurrenciesDropDown()
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
