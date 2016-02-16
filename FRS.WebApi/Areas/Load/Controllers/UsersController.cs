using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.Users;
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
        public UsersListViewModel Get([FromUri]UsersSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = usersService.GetAllUsers(searchRequest);
            return new UsersListViewModel
            {
                Data = response.Users.Select(x=>x.MapUserFromServerToClient()).ToList(),
                FilteredCount = response.FilteredCount,
                TotalCount = response.TotalCount
            };
        }

        #endregion
    }
}
