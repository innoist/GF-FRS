using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;

namespace FRS.Implementation.Services
{
    public class FiscalYearService : IFiscalYearService
    {
        private readonly IFiscalYearRepository fiscalYearRepository;

        public FiscalYearService(IFiscalYearRepository fiscalYearRepository)
        {
            this.fiscalYearRepository = fiscalYearRepository;
        }


        public IEnumerable<FiscalYear> GetFiscalYears()
        {
            return fiscalYearRepository.GetAll().ToList();
        }

        public FiscalYear GetFiscalYear(int Id)
        {
            return fiscalYearRepository.Find(Id);
        }

        public bool SaveFiscalYear(FiscalYear currency)
        {
            if (currency.Value == 0)
            {
                fiscalYearRepository.Add(currency);
            }
            else
            {
                fiscalYearRepository.Update(currency);
            }
            fiscalYearRepository.SaveChanges();
            return true;
        }
    }
}
