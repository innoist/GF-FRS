using FRS.Models.IdentityModels;

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

    }
}
