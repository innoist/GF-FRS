using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IMT940BalanceRepository : IBaseRepository<MT940Balance, long>
    {
        SearchTemplateResponse<MT940Balance> GetMt940BalanceSearchResponse(MT940BalanceSearchRequest searchRequest);
    }
}
