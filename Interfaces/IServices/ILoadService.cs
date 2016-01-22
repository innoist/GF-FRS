using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface ILoadService
    {
        IEnumerable<Load> GetAll();
        bool AddLoad(Load load);
        bool UpdateLoad(Load load);
        void DeleteLoad(Load load);
        MT940LoadBaseDataResponse GetBaseDataResponse();
        bool SaveLoad(Load load);
    }
}
