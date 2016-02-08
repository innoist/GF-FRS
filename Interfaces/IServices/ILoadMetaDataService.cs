using System.Collections.Generic;
using FRS.Models.Common;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface ILoadMetaDataService
    {
        IEnumerable<LoadMetaData> GetAll();
        bool AddMetaData(LoadMetaData loadMetaData);
        bool UpdateMetaData(LoadMetaData loadMetaData);
        void DeleteMetaData(long loadMetaData);
        BaseDataLoadMetaDataResponse GetBaseDataResponse();
        LoadMetaData SaveMetaData(LoadMetaData loadMetaData);
        LoadMetaDataForLoad IsLoadTypeMT940(long loadMetaDataId);

        SearchLoadMetaDataResponse SearchLoadMetaData(LoadMetaDataSearchRequest searchRequest);
    }
}
