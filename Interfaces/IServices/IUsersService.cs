using System.Collections.Generic;
using FRS.Models.IdentityModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.IServices
{
    public interface IUsersService
    {
        IEnumerable<AspNetUser> GetAllUsers();
        UsersSearchResponse GetAllUsers(UsersSearchRequest searchRequest);
        IEnumerable<UserRole> GetAllRoles();
    }
}
