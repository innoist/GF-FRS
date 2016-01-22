using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface  IMT940CustomerStatementTransactionService
    {
        IEnumerable<Load> GetAll();
        bool SaveCustomerStatementTransaction(MT940CustomerStatementTransaction customerStatementTransaction);
        bool UpdateCustomerStatementTransaction(MT940CustomerStatementTransaction customerStatementTransaction);
        void DeleteCustomerStatementTransaction(MT940CustomerStatementTransaction customerStatementTransaction);
    }
}
