using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface IMT940CustomerStatementService
    {
        IEnumerable<MT940CustomerStatement> GetAll();
        bool SaveCustomerStatement(MT940CustomerStatement customerStatement);
        bool UpdateCustomerStatement(MT940CustomerStatement customerStatement);
        void DeleteCustomerStatement(MT940CustomerStatement customerStatement);
    }
}
