using System.Collections.Generic;
using FRS.WebApi.Models;
using FRS.WebApi.Models.Status;

namespace FRS.WebApi.ViewModels.LoadStatus
{
    public class LoadStatusViewModel
    {

        public LoadStatusModel LoadStatus { get; set; }
        public List<StatusModel> Statuses { get; set; }
    }
}