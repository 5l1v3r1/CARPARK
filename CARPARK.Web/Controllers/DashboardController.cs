using CARPARK.COMMON;
using CARPARK.Data.UnitofWork;
using CARPARK.DTO.EntitisDTO;
using CARPARK.DTO.EntitisModelViewDTO;
using CARPARK.Service.Interfaces;
using CARPARK.Web.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CARPARK.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICustomerService _musteriService;
        private readonly ICarService _aracService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;
        public DashboardController(IUnitofWork uow, ICustomerService musteriService, ICarService aracService)
        {
            _uow = uow;
            _musteriService = musteriService;
            _aracService = aracService;
            _sessionContext = new SessionContext();
        }

        [Route("DashboardPanel")]
        public ActionResult DashboardPanel()
        {

            MusteriViewModel model = new MusteriViewModel();
            model.MusteriListe = _musteriService.CustomerList();
            return View(model);
        }

        public ActionResult GenelAyarlar()
        {
            return View();
        }


        public ActionResult AracPlakaOkuma()
        {
           
            return View();
        }

        public JsonResult OpenAPRDataReader ()
        {
            List<string> DataImages = SavePlateInformationToDatabase.SavePlateInformation();
            return Json(DataImages, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MusteriParkOdeme(int id)
        {
            MusteriViewModel model = new MusteriViewModel();
            model.Musteri = _musteriService.Customer(id);
            model.Park = _musteriService.CustomerPark(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}