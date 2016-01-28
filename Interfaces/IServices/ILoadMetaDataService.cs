using System.Collections.Generic;
using FRS.Models.DomainModels;
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
    }
}
