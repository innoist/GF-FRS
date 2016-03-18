using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IMT940CustomerStatementRepository : IBaseRepository<MT940CustomerStatement, long>
    {
        SearchTemplateResponse<MT940CustomerStatement> GetMt940SearchResponse(MT940CustomerStatementSearchrequest searchRequest);

        MT940CustomerStatement GetCustomerStatement(long mt940CustomerStatementId);
    }
}
