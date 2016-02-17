using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FRS.WebApi.ViewModels.Log
{
    public class LogViewModel
    {
        public LogViewModel()
        {
            LogDatas = new List<FRS.WebApi.Models.Log.LogModel>();
        }

        public IEnumerable<FRS.WebApi.Models.Log.LogModel> LogDatas { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}