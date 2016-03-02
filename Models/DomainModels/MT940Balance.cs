using System.Collections.Generic;

namespace FRS.Models.DomainModels
{
   public  class MT940Balance
    {
        public long MT940BalanceId { get; set; }
        public byte CurrencyId { get; set; }
        public string DebitOrCredit { get; set; }
        public System.DateTime EntryDate { get; set; }
        public decimal Value { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual ICollection<MT940CustomerStatement> MT940CustomerStatement { get; set; }
        public virtual ICollection<MT940CustomerStatement> MT940CustomerStatement1 { get; set; }
        public virtual ICollection<MT940CustomerStatement> MT940CustomerStatement2 { get; set; }
        public virtual ICollection<MT940CustomerStatement> MT940CustomerStatement3 { get; set; }
    }
}
