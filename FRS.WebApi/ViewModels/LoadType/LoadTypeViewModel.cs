using System.Collections.Generic;
using FRS.WebApi.Models.LoadType;
using FRS.WebApi.Models.Status;

namespace FRS.WebApi.ViewModels.LoadType
{
    public class LoadTypeViewModel
    {
        public LoadTypeViewModel()
        {
            Statuses = new List<StatusModel>();
        }
        public LoadTypeModel LoadType { get; set; }
        public IEnumerable<StatusModel> Statuses { get; set; }
    }
}