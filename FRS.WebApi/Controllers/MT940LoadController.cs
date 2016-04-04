using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using FRS.Interfaces.IServices;
using FRS.Models.RequestModels;
using FRS.WebApi.BankMT940Loader;
using FRS.WebApi.ModelMappers;
using FRS.WebApi.ViewModels.MT940Load;
using FRS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace FRS.WebApi.Controllers
{
    public class MT940LoadController : ApiController
    {
        #region Private

        private readonly IMT940LoadService mt940LoadService;
        #endregion

        #region Public

        public MT940LoadController(IMT940LoadService mt940LoadService)
        {
            this.mt940LoadService = mt940LoadService;
        }

        #region Get
        [ApiException]
        [HttpGet]
        [Authorize]
        public MT940LoadDetail Get(long id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var detail = mt940LoadService.GetMt940LoadDetail(id);
            MT940LoadDetail model = new MT940LoadDetail()
            {
                Load = detail.Load.CreateFromServerToClient(),
                LoadMetaData = detail.LoadMetaData.CreateFromServerToClient(),
                Mt940LoadModel = detail.Mt940Load.CreateFromServerToClient()
            };

            return model;
        }

        [HttpGet]
        [Authorize]
        [ApiException]
        public MT940LoadListViewModel Get([FromUri]MT940LoadSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var response = mt940LoadService.GetMt940SearchResponse(searchRequest);
            MT940LoadListViewModel listViewModel = new MT940LoadListViewModel
            {
                Mt940Loads = response.Data.Select(x => x.CreateFromServerToClient()).ToList(),
                FilteredCount = response.FilteredCount,
                TotalCount = response.TotalCount
            };

            return listViewModel;
        }

        #endregion

        #region Post

        [HttpPost]
        [Authorize]
        [ApiException]
        public IHttpActionResult Post(long LoadId)
        {
            BankMT940LoaderClient mt940LoaderClient =
                new BankMT940LoaderClient("BasicHttpBinding_BankMT940Loader");
            //I wna tto call this async but for now we wil do sync
            mt940LoaderClient.Open();
            try
            {
                var response = mt940LoaderClient.LoadMT940(new LoadMT940Request()
                {
                    LoadId = LoadId,
                    UserId = User.Identity.GetUserId()
                });

                mt940LoaderClient.Close();
                return Json(response.Message);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            
        }

        #endregion

        #endregion
    }
}
