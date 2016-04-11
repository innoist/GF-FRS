using System.Collections.Generic;
using FRS.WebApi.Models.ReconciledMapping;

namespace FRS.WebApi.ViewModels.ReconciledMapping
{
    public class ReconciledMappingViewModel
    {
        public ReconciledMappingViewModel()
        {
            ReconciledMappingModels = new List<ReconciledMappingModel>();
        }

        public IEnumerable<ReconciledMappingModel> ReconciledMappingModels { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}