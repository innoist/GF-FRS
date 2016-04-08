namespace FRS.WebApi.Models.OracleGLEntry
{
    public class OracleGLEntryModel
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
        public string EffectiveDate { get; set; }
        public string EntrySource { get; set; }
        public decimal? EnteredDr { get; set; }
        public decimal? EnteredCr { get; set; }
        public string AccountedDr { get; set; }
        public string AccountedCr { get; set; }
        public string Currency { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Period { get; set; }
        public short? FiscalYearId { get; set; }
        public string JECreationDate { get; set; }
        public string JELastUpdateDate { get; set; }
        public byte StatusId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public short Year { get; set; }
    }
}
