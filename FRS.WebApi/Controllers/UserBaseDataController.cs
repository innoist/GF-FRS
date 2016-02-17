using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using FRS.Interfaces.IServices;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models.Users;
using FRS.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserBaseDataController : ApiController
    {

        #region Private

        private readonly IUsersService usersService = UnityWebActivator.Container.Resolve<IUsersService>();

        #endregion

        // GET: api/UserBaseData
        [HttpGet]
        public IEnumerable<RoleDDL> Get()
        {
            var roles = usersService.GetAllRoles().Select(x => x.MapRoleFromServerToClient()).ToList();
            return roles;
        }
        
        [HttpGet]
        public UsersModel Get(string Id)
        {
            var user = usersService.GetUser(Id).MapUserFromServerToClient();
            return user;
        }
        
    }
}
