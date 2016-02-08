using System.Collections.Generic;
using FRS.Models.Common;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface ILoadMetaDataRepository : IBaseRepository<LoadMetaData, long>
    {
        IEnumerable<DropDownModel> LoadMetadataDropDown();
        LoadMetaDataForLoad IsLoadTypeMT940(long loadMetaDataId);
        SearchLoadMetaDataResponse SearchLoadMetaData(LoadMetaDataSearchRequest searchRequest);
    }
}
