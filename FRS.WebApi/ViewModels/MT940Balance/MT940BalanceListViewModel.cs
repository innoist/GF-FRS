using System.Collections.Generic;
using FRS.Models.Common.DropDown;
using FRS.WebApi.Models.Currency;
using FRS.WebApi.Models.MT940Balance;

namespace FRS.WebApi.ViewModels.MT940Balance
{
    public class MT940BalanceListViewModel
    {
        public MT940BalanceListViewModel()
        {
            Data = new List<MT940BalanceModel>();
            Currencies = new List<DropDownModel>();
        }

        public IList<MT940BalanceModel> Data { get; set; }
        public IList<DropDownModel> Currencies { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}