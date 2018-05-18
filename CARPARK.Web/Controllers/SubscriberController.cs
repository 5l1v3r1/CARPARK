using CARPARK.COMMON;
using CARPARK.COMMON.PassOperations;
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
        private readonly IMoneyEntryService _gelirService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;

        public SubscriberController(IUnitofWork uow, ISubscriberService aboneService, IUserService uyeService, ICarService aracService, IMoneyEntryService gelirService)
        {
            _uow = uow;
            _aboneService = aboneService;
            _uyeService = uyeService;
            _aracService = aracService;
            _gelirService = gelirService;
            _sessionContext = new SessionContext();
        }

        [HandleError]
        [Route("SubscriberList")]
        public ActionResult SubscriberList()
        {
            List<AboneDTO> liste = _aboneService.GetAllSubscriber();
            return View(liste);
        }

        [HandleError]
        public AracViewModel AracModel()
        {
            AracViewModel aracModel = new AracViewModel();
            aracModel.MarkaListesi = _aracService.GetAllBrand();
            return aracModel;
        }

        [HandleError]
        [Route("SubscriberInsert")]
        public ActionResult SubscriberInsert()
        {
            return View(AracModel());
        }

        [HandleError]
        [HttpPost]
        public JsonResult AracModelList(int id)
        {
            List<SelectListItem> itemList = _aracService.GetAllModel(id);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        [HandleError]
        [HttpPost]
        public ActionResult SubscriberInsert(AboneDTO abone, UyeDTO uye, AracDTO arac)
        {
            uye.KullaniciAdi = (abone.Adi + abone.Soyad).Trim().Replace(" ", string.Empty).ToLower();
            uye.Parola = PassManager.Base64Encrypt(abone.TCNO);
            int uyeID = _uyeService.Insert(uye);
            int aracID = _aracService.Insert(arac);
            _aboneService.Insert(abone, uyeID, aracID);
            return RedirectToAction("SubscriberList", "Subscriber");
        }

        [HandleError]
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

        [HandleError]
        [Route("SubscriberDetail/{id}")]
        public ActionResult SubscriberDetail(int id)
        {
            AboneViewModel model = new AboneViewModel();
            model = AboneModel(id);
            return View(model);
        }

        [HandleError]
        [Route("SubscriberUpdate/{id}")]
        public ActionResult SubscriberUpdate(int id)
        {
            AboneViewModel model = new AboneViewModel();
            model = AboneModel(id);
            model.AracModel = AracModel();
            return View(model);
        }

        [HandleError]
        [HttpPost]
        public ActionResult SubscriberDelete(int id)
        {
            _aboneService.Delete(id);
            return RedirectToAction("SubscriberList", "Subscriber");
        }

        [HandleError]
        [Route("SubscriberPaymentList/{id}")]
        public ActionResult SubscriberPaymentList(int id)
        {
            AboneViewModel model = new AboneViewModel();
            model.Abone = _aboneService.Subscriber(id);
            model.AboneOdeme = _aboneService.GetAllSubscriberPayment(id);
            return View(model);
        }

        [HandleError]
        [HttpPost]
        public ActionResult SubscriberPaymentInsert(AboneOdemeDTO odeme)
        {
            odeme.OdemeTarihi = DateTime.Now;
            _aboneService.SubscriberPaymentInsert(odeme);
            GelirlerDTO gelir = new GelirlerDTO();
            gelir.GelirTuru = "Abone Ödeme";
            gelir.OdemeTarihi = Convert.ToDateTime(odeme.OdemeTarihi);
            gelir.Tutar = Convert.ToDecimal(odeme.Tutar);
            var abone = _aboneService.Subscriber(Convert.ToInt32(odeme.AboneID));
            gelir.AracID = Convert.ToInt32(abone.AracID);
            _gelirService.Insert(gelir);
            return RedirectToAction("SubscriberList", "Subscriber");

        }

        [HandleError]
        [Route("SubscriberPaymentInvoice/{id}")]
        public ActionResult SubscriberPaymentInvoice(int id)
        {
            var abone = _aboneService.SubscriberPayment(id);
            AboneViewModel model = new AboneViewModel();
            model = AboneModel(Convert.ToInt32(abone.AboneID));
            model.Odeme = _aboneService.SubscriberPayment(id);
            return View(model);
        }

    }
}