using System.Linq;
using FRS.Models.DomainModels;
using FRS.WebApi.Models.MT940CustomerStatement;

namespace FRS.WebApi.ModelMappers
{
    public static class CustomerStatementMapper
    {
        public static MT940CustomerStatementModel MapFromServerToClient(this MT940CustomerStatement source)
        {
            return new MT940CustomerStatementModel()
            {
                AccountNumber = source.AccountNumber,
                Description = source.Description,
                ReleatedMessage = source.ReleatedMessage,
                TransactionReference = source.TransactionReference,
            };
        }
    }
}