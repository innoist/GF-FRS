using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface IMT940CustomerStatementService
    {
        SearchTemplateResponse<MT940CustomerStatement> GetMt940SearchResponse(MT940CustomerStatementSearchrequest searchRequest);
        MT940CustomerStatement GetMt940CustomerStatementDetail(long mt940CustomerStatementId);
    }
}
