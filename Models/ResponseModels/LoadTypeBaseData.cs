using System.Collections.Generic;
using FRS.Models.DomainModels;

namespace FRS.Models.ResponseModels
{
    public class LoadTypeBaseData
    {
        public LoadType LoadType { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
    }
}