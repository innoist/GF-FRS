using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface ILoadStatusService
    {
        IEnumerable<LoadStatu> GeLoadStatuses();
        LoadStatusResponse GetLoadStatus(int? Id);
        bool SaveLoadStatus(LoadStatu currency);
    }
}