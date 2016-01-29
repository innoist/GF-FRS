using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using FRS.Interfaces.Repository;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    public class LoadMetaDataRepository : BaseRepository<LoadMetaData>, ILoadMetaDataRepository
    {
        public LoadMetaDataRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<LoadMetaData> DbSet
        {
            get { return db.LoadMetaDatas; }
        }

        #region Public
        public IEnumerable<DropDownModel> LoadMetadataDropDown()
        {
            return DbSet.Select(x => new DropDownModel
            {
                Id = x.LoadMetaDataId,
                Name = x.Name
            });
        }

        public bool IsSourceFileType(long loadMetaDataId)
        {
            string loadType = ConfigurationManager.AppSettings["LoadTypeFile"];
            var metaData = DbSet.FirstOrDefault(x => x.LoadMetaDataId == loadMetaDataId);
            if (metaData != null && metaData.Source != null)
            {
                return metaData.LoadType.Name == loadType || metaData.LoadType.Name == loadType;
            }
            return false;
        }

        #endregion
    }
}
