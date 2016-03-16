using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface IFiscalYearService
    {
        IEnumerable<FiscalYear> GetFiscalYears();
        FiscalYear GetFiscalYear(int Id);
        bool SaveFiscalYear(FiscalYear currency);
    }
}