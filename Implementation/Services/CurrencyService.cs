using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;

namespace FRS.Implementation.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return currencyRepository.GetAll().ToList();
        }
    }
}
