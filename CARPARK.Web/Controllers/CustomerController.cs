using CARPARK.COMMON;
using CARPARK.Data.UnitofWork;
using CARPARK.DTO.EntitisDTO;
using CARPARK.DTO.EntitisModelViewDTO;
using CARPARK.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARPARK.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICarService _aracService;
        private readonly ICustomerService _musteriService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;
        public CustomerController(IUnitofWork uow, ICarService aracService, ICustomerService musteriService)
        {
            _uow = uow;
            _aracService = aracService;
            _musteriService = musteriService;
            _sessionContext = new SessionContext();
        }

        [Route("CustomerList")]
        [HandleError]
        public ActionResult CustomerList()
        {
            var liste = _musteriService.CustomerList();
            return View(liste);
        }
            
        [Route("CustomerDetail/{id}")]
        [HandleError]
        public ActionResult CustomerDetail(int id)
        {
            MusteriViewModel model = new MusteriViewModel();
            model.Musteri = _musteriService.Customer(id);
            if (model.Musteri.HizmetTuru == "Yıkama")
            {
                model.Yikama = _musteriService.CustomerWashing(id);
            }
            else if (model.Musteri.HizmetTuru == "Park")
            {
                model.Park = _musteriService.CustomerPark(id);
            }
            return View(model);
        }

        [Route("CustomerInsert/{id}")]
        [HandleError]
        public ActionResult CustomerInsert(int id)
        {
            MusteriViewModel model = new MusteriViewModel();
            model.AracModel = AracModel();
            if (id == 1)
            {
                model.CustomerPartial = "Park";
            }
            else if (id == 2)
            {
                model.CustomerPartial = "Yıkama";
            }
            return View(model);
        }

        [HandleError]
        public AracViewModel AracModel()
        {
            AracViewModel aracModel = new AracViewModel();
            aracModel.MarkaListesi = _aracService.GetAllBrand();
            return aracModel;
        }

        [HttpPost]
        [HandleError]
        public JsonResult AracModelList(int id)
        {
            List<SelectListItem> itemList = _aracService.GetAllModel(id);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleError]
        public ActionResult CustomerParkInsert(MusteriDTO musteri, MusteriParkDTO park)
        {
            return View();
        }

    }
}