using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FRS.Interfaces.Repository;
using FRS.Models.Common;
using FRS.Models.IdentityModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    /// <summary>
    /// User repository for User related functions
    /// </summary>
    public class UserRepository : BaseRepository<AspNetUser>, IUserRepository
    {
        #region private

        private readonly Dictionary<OrderByUsers, Func<AspNetUser, object>> orderClause =

            new Dictionary<OrderByUsers, Func<AspNetUser, object>>
            {
                //{OrderByUsers.Name, c => c.},
                {OrderByUsers.Id, c => c.Id},
            };

        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public UserRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AspNetUser> DbSet
        {
            get
            {
                return db.Users;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// To get the maximum user domain key
        /// </summary>
        public double GetMaxUserDomainKey()
        {
            return DbSet.Max(user => user.UserDomainKey);
        }
        /// <summary>
        /// Returns User by user Id
        /// </summary>
        public AspNetUser FindUserById(string userId)
        {
            return DbSet.FirstOrDefault(user => user.Id == userId);
        }
        public AspNetUser FindUserByUserName(string userName)
        {
            return DbSet.FirstOrDefault(user => user.UserName == userName);
        }

        /// <summary>
        /// Get User By Domain Key
        /// </summary>
        public AspNetUser GetLoggedInUser()
        {
            return DbSet.FirstOrDefault(user => user.UserName == LoggedInUserIdentity);
        }

        public UsersSearchResponse GetUsersSearchResponse(UsersSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<AspNetUser, bool>> query =
                s =>
                    (
                    (searchRequest.Name == null || s.FirstName.Contains(searchRequest.Name)) &&
                    (searchRequest.Name == null || s.LastName.Contains(searchRequest.Name)) &&
                    (searchRequest.PhoneNumber == null || s.Telephone.Contains(searchRequest.PhoneNumber)) &&
                    (searchRequest.Role == null || s.AspNetRoles.FirstOrDefault().Id == searchRequest.Role)
                    );

            IEnumerable<AspNetUser> users = searchRequest.IsAsc
                ? DbSet
                    .Where(query)
                    .OrderBy(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet
                    .Where(query)
                    .OrderByDescending(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new UsersSearchResponse { Users = users, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }

        #endregion
    }
}
