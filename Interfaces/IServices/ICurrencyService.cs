using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface ICurrencyService
    {
        IEnumerable<Currency> GetCurrencies();
    }
}