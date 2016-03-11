using System;

namespace FRS.Models.DomainModels
{
    public class OracleGLEntry
    {
        public long OracleGLEntryId { get; set; }
        public long OracleGLLoadId { get; set; }
        public string UniqueReferenceKey { get; set; }
        public string JournalEntryHeaderNumber { get; set; }
        public string JournalEntryDescription { get; set; }
        public int? LineNumber { get; set; }
        public string LineDescription { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDescription { get; set; }
        public string SubAccountDescription { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string EntrySource { get; set; }
        public decimal? EnteredDr { get; set; }
        public decimal? EnteredCr { get; set; }
        public decimal? AccountedDr { get; set; }
        public decimal? AccountedCr { get; set; }
        public string Currency { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Period { get; set; }
        public short? FiscalYearId { get; set; }
        public DateTime? JECreationDate { get; set; }
        public DateTime? JELastUpdateDate { get; set; }
        public byte StatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual FiscalYear FiscalYear { get; set; }
        public virtual OracleGLLoad OracleGLLoad { get; set; }
        public virtual Status Status { get; set; }
    }
}
