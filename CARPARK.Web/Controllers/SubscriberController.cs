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
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _aboneService;
        private readonly ICarService _aracService;
        private readonly IUserService _uyeService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;

        public SubscriberController(IUnitofWork uow, ISubscriberService aboneService, IUserService uyeService, ICarService aracService)
        {
            _uow = uow;
            _aboneService = aboneService;
            _uyeService = uyeService;
            _aracService = aracService;
            _sessionContext = new SessionContext();
        }

        [Route("SubscriberList")]
        public ActionResult SubscriberList()
        {
            List<AboneDTO> liste = _aboneService.GetAllSubscriber();
            return View(liste);
        }

        [Route("SubscriberAdd")]
        public ActionResult SubscriberAdd()
        {
            AracViewModel aracModel = new AracViewModel();
            List<AracMarkaDTO> markalar = _aracService.GetAllBrand();
            aracModel.MarkaListesi = (from m in markalar
                                      select new SelectListItem
                                      {
                                          Text = m.Marka,
                                          Value = m.MarkaID.ToString()
                                      }).ToList();

            aracModel.MarkaListesi.Insert(0, new SelectListItem { Text = "Marka Seçiniz", Value = "", Selected = true });
            return View(aracModel);
        }

        [HttpPost]
        public JsonResult AracModelList(int id)
        {

            List<AracModelDTO> modelList = _aracService.GetAllModel();
            List<SelectListItem> itemList = (from md in modelList
                                             select new SelectListItem
                                             {
                                                 Value = md.ModelID.ToString(),
                                                 Text = md.Model
                                             }).ToList();
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
    }
}