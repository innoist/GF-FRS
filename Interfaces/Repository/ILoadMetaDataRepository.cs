using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.Repository
{
    public interface ILoadMetaDataRepository : IBaseRepository<LoadMetaData, long>
    {
        IEnumerable<DropDownModel> LoadMetadataDropDown();
        bool IsSourceFileType(long loadMetaDataId);
    }
}
