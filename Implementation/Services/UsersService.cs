using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.IdentityModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;

        public UsersService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return userRepository.GetAll();
        }
        public UsersSearchResponse GetAllUsers(UsersSearchRequest searchRequest)
        {
            return userRepository.GetUsersSearchResponse(searchRequest);
        }
    }
}
