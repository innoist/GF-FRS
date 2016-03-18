using System.Collections.Generic;
using FRS.WebApi.Models.OracleGLLoad;

namespace FRS.WebApi.ViewModels.OracleGlLoad
{
    public class OracleGLLoadLVModel
    {
        public OracleGLLoadLVModel()
        {
            OracleGlLoads = new List<OracleGLLoadModel>();
        }

        public IEnumerable<OracleGLLoadModel> OracleGlLoads { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}