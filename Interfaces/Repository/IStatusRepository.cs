using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.Repository
{
    public interface IStatusRepository : IBaseRepository<Status, long>
    {
        IEnumerable<DropDownModel> GetStatusesDropDown();
    }
}
