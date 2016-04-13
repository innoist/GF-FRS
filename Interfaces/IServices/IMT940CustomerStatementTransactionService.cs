using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface  IMT940CustomerStatementTransactionService
    {
        SearchTemplateResponse<MT940CustomerStatementTransaction> GetMt940SearchResponse(MT940CustomerStatementTransactionSearchrequest searchRequest);
        MT940CustomerStatementTransaction GetTransaction(long id);
    }
}
