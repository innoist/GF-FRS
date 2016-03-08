namespace FRS.WebApi.Models.MT940Balance
{
    public class MT940BalanceModel
    {
        public long MT940BalanceId { get; set; }
        public byte CurrencyId { get; set; }
        public string DebitOrCredit { get; set; }
        public string Currency { get; set; }
        public string EntryDate { get; set; }
        public decimal Value { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    }
}