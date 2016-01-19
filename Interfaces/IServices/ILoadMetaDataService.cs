using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface ILoadMetaDataService
    {
        IEnumerable<LoadMetaData> GetAll();
        bool SaveMetaData(LoadMetaData loadMetaData);
        bool UpdateMetaData(LoadMetaData loadMetaData);
        void DeleteMetaData(LoadMetaData loadMetaData);
    }
}
