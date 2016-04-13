using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class MT940CustomerStatementTransactionService:IMT940CustomerStatementTransactionService
    {
        private readonly IMT940CustomerStatementTransactionRepository mt940CustomerStatementTransactionRepository;

        public MT940CustomerStatementTransactionService(IMT940CustomerStatementTransactionRepository mt940CustomerStatementTransactionRepository)
        {
            this.mt940CustomerStatementTransactionRepository = mt940CustomerStatementTransactionRepository;
        }


        public SearchTemplateResponse<Models.DomainModels.MT940CustomerStatementTransaction> GetMt940SearchResponse(MT940CustomerStatementTransactionSearchrequest searchRequest)
        {
            return mt940CustomerStatementTransactionRepository.GetMt940SearchResponse(searchRequest);
        }

        public MT940CustomerStatementTransaction GetTransaction(long id)
        {
            return mt940CustomerStatementTransactionRepository.Find(id);
        }
    }
}
