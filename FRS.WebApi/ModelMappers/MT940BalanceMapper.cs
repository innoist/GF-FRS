using FRS.Models.DomainModels;
using FRS.WebApi.Models.MT940Balance;

namespace FRS.WebApi.ModelMappers
{
    public static class MT940BalanceMapper
    {
        public static MT940BalanceModel MapFromServerToClient(this MT940Balance source)
        {
            return new MT940BalanceModel
            {
                Value = source.Value,
                ModifiedBy = source.ModifiedBy,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                ModifiedOn = source.ModifiedOn,
                DebitOrCredit = source.DebitOrCredit == "C" ? "Credit" : source.DebitOrCredit == "D" ? "Debit": "N/A",
                Currency = source.Currency.Sign + "(" + source.Currency.Name + ")",
                CurrencyId = source.CurrencyId,
                EntryDate = source.EntryDate.ToString("dd-MMM-yy"),
                MT940BalanceId = source.MT940BalanceId
            };
        }
    }
}