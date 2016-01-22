using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface IMT940LoadDetailService
    {
        IEnumerable<MT940LoadDetail> GetAll();
        bool SaveLoadDetail(MT940LoadDetail loadDetail);
        bool UpdateLoadDetail(MT940LoadDetail loadDetail);
        void DeleteLoadDetail(MT940LoadDetail loadDetail);
    }
}
