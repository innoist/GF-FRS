using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.Models.Users;
using FRS.WebBase.Mvc;
using FRS.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;

namespace FRS.WebApi.Areas.Load.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        #region Private

        private readonly IUsersService usersService = UnityWebActivator.Container.Resolve<IUsersService>();

        #endregion

        #region Constructor
        #endregion

        #region Public

        [Authorize]
        [ApiException]
        public IEnumerable<UsersModel> Get(UsersSearchRequest searchRequest)
        {
            return usersService.GetAllUsers().Select(x => x.MapUserFromServerToClient()).ToList();
        }

        #endregion
    }
}
