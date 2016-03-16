using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class LoadStatusResponse
    {
        public LoadStatu LoadStatus { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
    }
}
