using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface IMT940BalanceService
    {
        SearchTemplateResponse<MT940Balance> GetMt940BalanceSearchResponse(MT940BalanceSearchRequest searchRequest);
    }
}
