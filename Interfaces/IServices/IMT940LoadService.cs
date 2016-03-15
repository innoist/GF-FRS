using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface  IMT940LoadService
    {
        IEnumerable<MT940Load> GetAll();
        SearchTemplateResponse<MT940Load> GetMt940SearchResponse(MT940LoadSearchRequest searchRequest); 
        bool SaveMT940Load(MT940Load mt940Load);
        void DeleteMT940Load(long mt940LoadId);
        MT940LoadDetailResponse GetMt940LoadDetail(long mt940LoadId);
    }
}
