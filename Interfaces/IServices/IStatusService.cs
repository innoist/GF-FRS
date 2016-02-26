using System.CodeDom.Compiler;
using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.IServices
{
    public interface IStatusService
    {
        IEnumerable<Status> GetStatuses();
        bool SaveStatus(Status status);
        Status GetStatus(int Id);

    }
}