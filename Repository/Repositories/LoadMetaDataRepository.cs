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

        public IEnumerable<LoadMetaDataWithFileType> LoadMetadataWithFileTypes()
        {
            return DbSet.Select(x => new LoadMetaDataWithFileType
            {
                LoadMetaDataId = x.LoadMetaDataId,
                IsSourceFileType = x.Source.Name == "File" || x.Source.Name == "file"
            });

        }

        #endregion
    }
}
