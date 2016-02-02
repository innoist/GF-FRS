using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using FRS.Interfaces.Repository;
using FRS.Models.Common;
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

        public LoadMetaDataForLoad IsLoadTypeMT940(long loadMetaDataId)
        {
            string loadType = ConfigurationManager.AppSettings["LoadTypeFile"];
            var metaData = DbSet.FirstOrDefault(x => x.LoadMetaDataId == loadMetaDataId);
            if (metaData != null && metaData.Source != null)
            {
                return new LoadMetaDataForLoad
                {
                    IsLoadTypeMT940 = metaData.LoadType.Name == loadType,
                    LoadType = metaData.LoadType.Name,
                    SourceName = metaData.Source.Name,
                    LastModified = metaData.ModifiedOn.ToString("dd/MM/yyyy", new CultureInfo("en"))
                };
            }
            return null;
        }

        #endregion
    }
}
