﻿using System.Collections.Generic;
using FRS.Models.Common;
using FRS.Models.Common.DropDown;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.Repository
{
    public interface ILoadMetaDataRepository : IBaseRepository<LoadMetaData, long>
    {
        IEnumerable<DropDownModel> LoadMetadataDropDown();
        LoadMetaDataForLoad IsLoadTypeMT940(long loadMetaDataId);
    }
}
