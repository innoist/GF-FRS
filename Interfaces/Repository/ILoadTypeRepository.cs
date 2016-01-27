using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.Repository
{
    public interface ILoadTypeRepository : IBaseRepository<LoadType, long>
    {
        IEnumerable<DropDownModel> GetLoadTypesDropDown();
    }
}
