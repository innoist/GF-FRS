using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface ILoadTypeService
    {
        IEnumerable<LoadType> GetLoadTypes();
        LoadType GetLoadType(int Id);
        LoadTypeBaseData GetLoadTypeBaseData(int ? Id);
        bool SaveLoadType(LoadType loadType);
    }
}
