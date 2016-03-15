using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class SourceBaseData
    {
        public Source Source { get; set; }
        public IEnumerable<Status> Statuses { get; set; }

    }
}
