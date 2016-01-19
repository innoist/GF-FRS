using System.Data.Entity;
using System.Linq;
using FRS.Interfaces.Repository;
using FRS.Models.IdentityModels;
using FRS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{
    /// <summary>
    /// User repository for User related functions
    /// </summary>
    public class UserRepository : BaseRepository<AspNetUser>, IUserRepository
    {
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

        /// <summary>
        /// Get User By Domain Key
        /// </summary>
        public AspNetUser GetLoggedInUser()
        {
            return DbSet.FirstOrDefault(user => user.UserName == LoggedInUserIdentity);
        }

        #endregion
    }
}
