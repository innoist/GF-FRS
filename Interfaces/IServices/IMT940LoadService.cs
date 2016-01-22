using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface  IMT940LoadService
    {
        IEnumerable<MT940Load> GetAll();
        bool SaveLoad(MT940Load load);
        bool UpdateLoad(MT940Load load);
        void DeleteLoad(MT940Load load);
    }
}
