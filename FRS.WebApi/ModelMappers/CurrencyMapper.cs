using FRS.Models.DomainModels;
using FRS.WebApi.Models;
using FRS.WebApi.Models.Currency;

namespace FRS.WebApi.ModelMappers
{
    public static class CurrencyMapper
    {
        public static Currency MapFromClientToServer(this CurrencyModel source)
        {
            return new Currency
            {
                Name = source.Name,
                Sign = source.Sign,
                Value = source.Value
            };
        }
        public static CurrencyModel MapFromServerToClient(this Currency source)
        {
            return new CurrencyModel
            {
                Name = source.Name,
                Sign = source.Sign,
                Value = source.Value
            };
        }
        public static FiscalYear MapFromClientToServer(this FiscalYearModel source)
        {
            return new FiscalYear
            {
                Name = source.Name,
                Value = source.Value
            };
        }
        public static FiscalYearModel MapFromServerToClient(this FiscalYear source)
        {
            return new FiscalYearModel
            {
                Name = source.Name,
                Value = source.Value
            };
        }
    }
}