using System;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.IdentityModels;
using FRS.Models.IdentityModels.ViewModels;

namespace FRS.Implementation.Services
{
    /// <summary>
    /// Register User Service 
    /// </summary>
    public class RegisterUserService : IRegisterUserService
    {
        #region Private

        private readonly IUserRepository userRepository;
        private readonly IUserDetailsRepository userDetailsRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public RegisterUserService(IUserRepository userRepository, IUserDetailsRepository userDetailsRepository)
        {
            this.userRepository = userRepository;
            this.userDetailsRepository = userDetailsRepository;
        }
        #endregion
        #region Public

        /// <summary>
        /// Gives the maximum domain key from the records
        /// </summary>
        public double GetMaxUserDomainKey()
        {
            return userRepository.GetMaxUserDomainKey();
        }

        /// <summary>
        /// Saves user details provided while signup
        /// </summary>
        public void SaveUserDetails(AspNetUser addedUser, RegisterViewModel model)
        {
            UserDetail user = userDetailsRepository.Create();
            user.AccountType = model.AccountType;
            user.Address = model.CompanyAddress;
            user.CompanyName = model.CompanyName;
            user.CountryName = model.CountryName;
            user.CompanyShortUrl = model.ShortUrl;
            user.UserId = addedUser.Id;
            userDetailsRepository.Add(user);
            userDetailsRepository.SaveChanges();
        }
        /// <summary>
        /// Executes store procedure for copying default data for newly registered user
        /// </summary>
        public void SetupUserDefaultData(string userId)
        {
            UserDetail userDetails = userDetailsRepository.FindByUserId(userId);
            if (userDetails != null)
            {
                AspNetUser user = userRepository.FindUserById(userId);
                if (user == null)
                {
                    throw new Exception("User not found!");
                }
                userDetailsRepository.CopyUserDefaultData(userId, user.UserDomainKey);
                // We Need to keep the Userdetails it should not be deleted
                //userDetailsRepository.Delete(userDetails);
                //userDetailsRepository.SaveChanges();
            }
        }


        /// <summary>
        /// To check if URL is available 
        /// </summary>
        public bool IsCompanyUrlAvailable(string url)
        {
          return  userDetailsRepository.IsCompanyUrlAvailable(url);
        }

        #endregion
    }
}