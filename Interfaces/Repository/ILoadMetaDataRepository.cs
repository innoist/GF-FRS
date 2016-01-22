using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.Repository
{
    public interface ILoadMetaDataRepository : IBaseRepository<LoadMetaData, byte>
    {
        IEnumerable<LoadMetadataDropDown> LoadMetadataDropDown();
    }
}
