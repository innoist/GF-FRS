using System.Collections.Generic;
using FRS.WebApi.Models.Users;

namespace FRS.WebApi.ViewModels.Users
{
    public class UsersListViewModel
    {
        public UsersListViewModel()
        {
            Data = new List<UsersModel>();
        }

        public IEnumerable<UsersModel> Data { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}