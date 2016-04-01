using System.Collections.Generic;
using FRS.WebApi.Models;

namespace FRS.WebApi.ViewModels.Log
{
    public class ServiceLogViewModel
    {
        public ServiceLogViewModel()
        {
            Data = new List<ServiceLogModel>();
        }

        public IEnumerable<ServiceLogModel> Data { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}