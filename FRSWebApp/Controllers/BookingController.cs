using Cares.WebApp.Common;
using Cares.WebApp.Models;
using Cares.WebApp.ModelsMapper;
using Cares.WebApp.WebApi;
using Cares.WebApp.WepApiInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cares.WebApp.Controllers
{
    /// <summary>
    /// Booking Controller
    /// </summary>
    public class BookingController : Controller
    {        
        private IWebApiService webApiService;
        public BookingController()
        {
            webApiService = new WebApiService();
        }

        /// <summary>
        /// Booking main screen
        /// </summary>
        public ActionResult Index(BookingSearchRequest request)
        {
            try
            {
                if (request.OperationWorkPlaceCode != null && request.OperationWorkPlaceId != 0)
                {
                    var booking = new BookingViewModel()
                    {
                        OperationWorkPlaceCode = request.OperationWorkPlaceCode,
                        OperationWorkPlaceId = request.OperationWorkPlaceId,
                        StartDt = request.StartDt,
                        EndDt = request.EndDt
                    };
                    TempData["Booking"] = booking;
                    CompleteBookingData.Booking = booking;
                    return RedirectToAction("HireGroup");
                }
                ViewBag.OperationWorkPlaces = webApiService.GetOperationWorkplaceList(1);
                return View();
            }
            catch (Exception exp)
            {
                string a = exp.Message;
                throw;
            }
        }

        /// <summary>
        /// Hire Group Selection screen
        /// </summary>
        public ActionResult HireGroup(FormCollection collection)
        {
            if (collection["HireGroupDetailId"] != null)
            {
                var bookingView = new BookingViewModel
                {
                    HireGroupDetailId = Convert.ToInt64(collection["HireGroupDetailId"]),
                    OperationWorkPlaceId = Convert.ToInt64(collection["OperationWorkPlaceId"]),
                    OperationWorkPlaceCode = Convert.ToString(collection["OperationWorkPlaceCode"]),
                    StartDt = Convert.ToDateTime(collection["StartDateTime"]),
                    EndDt = Convert.ToDateTime(collection["EndDateTime"]),
                    TariffTypeCode = Convert.ToString(collection["TariffTypeCode"])
                };
                TempData["Booking"] = bookingView;
                return RedirectToAction("Services");
            }
            //hire group get
            var bookingViewModel = TempData["Booking"] as BookingViewModel;
            var hireGroupRequest = new GetHireGroupRequest();
            if (bookingViewModel != null)
            {
                hireGroupRequest.StartDateTime = bookingViewModel.StartDt;
                hireGroupRequest.EndDateTime = bookingViewModel.EndDt;
                hireGroupRequest.OutLocationId = bookingViewModel.OperationWorkPlaceId;
                hireGroupRequest.ReturnLocationId = bookingViewModel.OperationWorkPlaceId;
                hireGroupRequest.DomainKey = 1;
            }
            IEnumerable<HireGroupDetail> hireGroupDetails = webApiService.GetHireGroupList(hireGroupRequest)  .AvailableHireGroups.Select(x => x.CreateFrom());
            ViewBag.BookingVM = TempData["Booking"] as BookingViewModel;
            return View(hireGroupDetails.ToList());
        }

        /// <summary>
        /// Services upon submition
        /// </summary>
        [HttpPost]
        public ActionResult Services(WebApiBookingMainRequest bookingMain)
        {
             webApiService.SaveBookingMain(bookingMain);
             return Json(new
             {
                 message = "Booking successfully saved!"
             });
        }

        /// <summary>
        /// Services upon rendering
        /// </summary>
        [HttpGet]
        public ActionResult Services(FormCollection collection)
        {
            var bookingViewModel = TempData["Booking"] as BookingViewModel;
            if (bookingViewModel != null)
            {
                var hireGroupRequest = new WebApiRequest
                {
                    StartDateTime = bookingViewModel.StartDt,
                    EndDateTime = bookingViewModel.EndDt,
                    OutLocationId = bookingViewModel.OperationWorkPlaceId,
                    DomainKey = 1,
                    TarrifTypeCode = bookingViewModel.TariffTypeCode
                };
                var additionalServicesRequstResponse = new AdditionalServicesRequstResponse
                {
                    WebApiAdditionalDriverRates =
                        webApiService.GetAdditionalDriverRates(hireGroupRequest).WebApiAdditionalDriverRates,
                    WebApiAvailableInsurancesRates =
                        webApiService.GetAvailableInsurancesRates(hireGroupRequest).ApiAvailableInsurances,
                    WebApiAvailableChuffersRates =
                        webApiService.GetAvailableChauffersRates(hireGroupRequest).ApiAvailableChuffersRates
                };
                ViewBag.BookingVM = TempData["Booking"] as BookingViewModel;
                return View(additionalServicesRequstResponse);
            }
            return View();
        }

        /// <summary>
        /// GET: Customer Info.
        /// </summary>
        public ActionResult CustomerInfo()
        {
            var bookingView = new BookingViewModel
            {
                HireGroupDetailId = Convert.ToInt64(Request.Form["HireGroupDetailId"]),
                OperationWorkPlaceId = Convert.ToInt64(Request.Form["OperationWorkPlaceId"]),
                OperationWorkPlaceCode = Convert.ToString(Request.Form["OperationWorkPlaceCode"]),
                StartDt = Convert.ToDateTime(Request.Form["StartDt"]),
                EndDt = Convert.ToDateTime(Request.Form["EndDt"])
            };
            ViewBag.BookingVM = bookingView;
            return View();
        }

    } 
}
