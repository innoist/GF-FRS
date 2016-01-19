using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface ILoadService
    {
        IEnumerable<Load> GetAll();
        bool SaveLoad(Load load);
        bool UpdateLoad(Load load);
        void DeleteLoad(Load load);
    }
}
