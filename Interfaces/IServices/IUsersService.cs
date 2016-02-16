using System.Collections.Generic;
using FRS.Models.IdentityModels;

namespace FRS.Interfaces.IServices
{
    public interface IUsersService
    {
        IEnumerable<AspNetUser> GetAllUsers();
    }
}
