using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface  IMT940LoadService
    {
        IEnumerable<MT940Load> GetAll();
        bool SaveMT940Load(MT940Load mt940Load);
        void DeleteMT940Load(long mt940LoadId);
    }
}
