using System.Collections.Generic;
using FRS.WebApi.Models.MT940Load;

namespace FRS.WebApi.ViewModels.MT940Load
{
    public class MT940LoadListViewModel
    {
        public MT940LoadListViewModel()
        {
            Mt940Loads = new List<MT940LoadModel>();
        }

        public IEnumerable<MT940LoadModel> Mt940Loads { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}