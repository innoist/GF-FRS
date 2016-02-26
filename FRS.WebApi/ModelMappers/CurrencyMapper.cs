using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FRS.Models.DomainModels;
using FRS.WebApi.Models.Currency;

namespace FRS.WebApi.ModelMappers
{
    public static class CurrencyMapper
    {
        public static Currency MapFromClientToServer(this CurrencyModel souurce)
        {
            return new Currency
            {
                Name = souurce.Name,
                Sign = souurce.Sign,
                Value = souurce.Value
            };
        }
        public static CurrencyModel MapFromServerToClient(this Currency souurce)
        {
            return new CurrencyModel
            {
                Name = souurce.Name,
                Sign = souurce.Sign,
                Value = souurce.Value
            };
        }
    }
}