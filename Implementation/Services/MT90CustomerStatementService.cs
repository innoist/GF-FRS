using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class MT940CustomerStatementService:IMT940CustomerStatementService
    {
        private readonly IMT940CustomerStatementRepository mt940CustomerStatementRepository;

        public MT940CustomerStatementService(IMT940CustomerStatementRepository mt940CustomerStatementRepository)
        {
            this.mt940CustomerStatementRepository = mt940CustomerStatementRepository;
        }

        public SearchTemplateResponse<MT940CustomerStatement> GetMt940SearchResponse(MT940CustomerStatementSearchrequest searchRequest)
        {
            return mt940CustomerStatementRepository.GetMt940SearchResponse(searchRequest);
        }
    }
}
