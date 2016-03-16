using FRS.WebApi.Models.Load;
using FRS.WebApi.Models.MetaData;
using FRS.WebApi.Models.OracleGLLoad;

namespace FRS.WebApi.ViewModels.OracleGlLoad
{
    public class OracleGLLoadDetail
    {
        public LoadModel Load { get; set; }
        public OracleGLLoadModel OracleGlLoad { get; set; }
        public LoadMetaData LoadMetaData { get; set; }
    }
}