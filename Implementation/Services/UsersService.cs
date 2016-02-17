using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.IdentityModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace FRS.Implementation.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;
        private readonly IAspNetRoleRepository aspNetRoleRepository;

        public UsersService(IUserRepository userRepository, IAspNetRoleRepository aspNetRoleRepository)
        {
            this.userRepository = userRepository;
            this.aspNetRoleRepository = aspNetRoleRepository;
        }


        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return userRepository.GetAll();
        }
        public UsersSearchResponse GetAllUsers(UsersSearchRequest searchRequest)
        {
            return userRepository.GetUsersSearchResponse(searchRequest);
        }

        public IEnumerable<UserRole> GetAllRoles()
        {
            return aspNetRoleRepository.GetAll().ToList();
        }
    }
}
