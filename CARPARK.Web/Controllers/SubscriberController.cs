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

        public AracViewModel AracModel()
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
            return aracModel;
        }

        [Route("SubscriberAdd")]
        public ActionResult SubscriberAdd()
        {
            return View(AracModel());
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

        [HttpPost]
        public ActionResult SubscriberAdd(AboneDTO abone, UyeDTO uye, AracDTO arac)
        {
            uye.KullaniciAdi = (abone.Adi + abone.Soyad).Trim().Replace(" ", string.Empty).ToLower();
            uye.Parola = abone.TCNO;
            int uyeID = _uyeService.Insert(uye);
            int aracID = _aracService.Insert(arac);
            _aboneService.Insert(abone, uyeID, aracID);
            return RedirectToAction("SubscriberList", "Subscriber");
        }

        public AboneViewModel AboneModel(int id)
        {
            AboneViewModel model = new AboneViewModel();
            model.Abone = _aboneService.Subscriber(id);
            model.Uye = _uyeService.User(Convert.ToInt32(model.Abone.UyeID));
            model.Arac = _aracService.Car(Convert.ToInt32(model.Abone.AracID));
            model.Marka = _aracService.Brand(Convert.ToInt32(model.Arac.MarkaID));
            model.Model = _aracService.Model(Convert.ToInt32(model.Arac.ModelID));
            return model;
        }

        [Route("SubscriberDetail/{id}")]
        public ActionResult SubscriberDetail(int id)
        {
            AboneViewModel model = new AboneViewModel();
            model = AboneModel(id);
            return View(model);
        }

        [Route("SubscriberUpdate/{id}")]
        public ActionResult SubscriberUpdate(int id)
        {
            AboneViewModel model = new AboneViewModel();
            model = AboneModel(id);
            model.AracModel = AracModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SubscriberDelete(int id)
        {
            _aboneService.Delete(id);
            return RedirectToAction("SubscriberList", "Subscriber");
        }

        [Route("SubscriberPaymentList/{id}")]
        public ActionResult SubscriberPaymentList(int id)
        {
            AboneViewModel model = new AboneViewModel();
            model.Abone = _aboneService.Subscriber(id);
            model.AboneOdeme = _aboneService.GetAllSubscriberPayment();
            return View(model);
        }

        [HttpPost]
        public ActionResult SubscriberPaymentAdd(AboneOdemeDTO odeme)
        {
            odeme.OdemeTarihi = DateTime.Now;
            _aboneService.SubscriberPaymentInsert(odeme);
            return RedirectToAction("SubscriberPaymentList", "Subscriber");
        }

    }
}