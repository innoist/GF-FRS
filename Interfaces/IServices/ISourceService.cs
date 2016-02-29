using System.Collections.Generic;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface ISourceService
    {
        IEnumerable<Source> GetSources();
        SourceBaseData GetSourceById(int? Id);
        bool SaveSource(Source source);
    }
}