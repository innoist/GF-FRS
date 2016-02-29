using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.WebApi.Models.Source;
using FRS.WebApi.Models.Status;

namespace FRS.WebApi.ViewModels.Source
{
    public class SourceViewModel
    {
        public SourceModel Source { get; set; }
        public IEnumerable<StatusModel> Statuses { get; set; }
    }
}