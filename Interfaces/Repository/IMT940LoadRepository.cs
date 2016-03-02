using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface IMT940LoadRepository : IBaseRepository<MT940Load, long>
    {

        SearchTemplateResponse<MT940Load> GetMt940SearchResponse(MT940LoadSearchRequest searchRequest); 


    }
}
