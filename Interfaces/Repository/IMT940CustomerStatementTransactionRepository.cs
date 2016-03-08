using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IMT940CustomerStatementTransactionRepository : IBaseRepository<MT940CustomerStatementTransaction, long>
    {
        SearchTemplateResponse<MT940CustomerStatementTransaction> GetMt940SearchResponse(
            MT940CustomerStatementTransactionSearchrequest searchRequest);
    }
}
