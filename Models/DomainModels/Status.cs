﻿using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
    public class Status
    {
        public byte Value { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LoadMetaData> LoadMetaDatas { get; set; }
        public virtual ICollection<MT940Load> MT940Load { get; set; }
        public virtual ICollection<Source> Sources { get; set; }
        public virtual ICollection<LoadType> LoadTypes { get; set; }
        public virtual ICollection<OracleGLEntry> OracleGLEntries { get; set; }
        public virtual ICollection<OracleGLLoad> OracleGLLoads { get; set; }
        public virtual ICollection<LoadStatu> LoadStatus { get; set; }
    }
}
