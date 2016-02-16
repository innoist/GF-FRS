using System.Collections.Generic;
using FRS.Models.IdentityModels;

namespace FRS.Models.ResponseModels
{
    public class UsersSearchResponse
    {

        public UsersSearchResponse()
        {
            Users = new List<AspNetUser>();
        }

        public IEnumerable<AspNetUser> Users { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}
