using System.Data.Entity;
using FRS.Interfaces.Repository;
using FRS.Models.IdentityModels;
using Microsoft.Practices.Unity;

namespace FRS.Repository.Repositories
{

    public class AspNetRoleRepository : BaseRepository.BaseRepository<UserRole>, IAspNetRoleRepository
    {
        public AspNetRoleRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<UserRole> DbSet
        {
            get { return db.UserRoles; }
        }
    }

}
