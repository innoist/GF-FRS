using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public partial class FiscalYear
    {
        public short Value { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OracleGLEntry> OracleGLEntries { get; set; }
    }
}
