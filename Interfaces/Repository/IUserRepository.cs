using FRS.Models.IdentityModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Interfaces.Repository
{
    public interface   IUserRepository : IBaseRepository<AspNetUser, long>
    {
        /// <summary>
        /// To get the maximum user domain key
        /// </summary>
        double GetMaxUserDomainKey();

        /// <summary>
        /// Finds user by user id
        /// </summary>
        AspNetUser FindUserById(string userId);

        /// <summary>
        /// Get User by Name
        /// </summary>
        AspNetUser GetLoggedInUser();

        UsersSearchResponse GetUsersSearchResponse(UsersSearchRequest searchRequest);
        AspNetUser FindUserByUserName(string userName);
    }
}
