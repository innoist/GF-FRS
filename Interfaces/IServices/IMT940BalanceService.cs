using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface IMT940BalanceService
    {
        IEnumerable<MT940Balance> GetAll();
        bool SaveloadBalance(MT940Balance loadBalance);
        bool UpdateloadBalance(MT940Balance loadBalance);
        void DeleteloadBalance(MT940Balance loadBalance);
    }
}
