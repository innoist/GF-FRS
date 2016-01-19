using FRS.Models.IdentityModels;
using FRS.Models.IdentityModels.ViewModels;

namespace FRS.Interfaces.IServices
{
    public interface IRegisterUserService
    {
        /// <summary>
        /// Gives the maximum domain key from the records
        /// </summary>
        double GetMaxUserDomainKey();
        
        /// <summary>
        /// Saves user details provided while signup
        /// </summary>
        void SaveUserDetails(AspNetUser addedUser, RegisterViewModel model);

        /// <summary>
        /// Setup User default data
        /// </summary>
        void SetupUserDefaultData(string userId);


        /// <summary>
        /// To check if URL is available 
        /// </summary>
        bool IsCompanyUrlAvailable(string url);
    }
}
