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
            aracModel.MarkaListesi = _aracService.GetAllBrand();
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
            List<SelectListItem> itemList = _aracService.GetAllModel(id);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubscriberAdd(AboneDTO abone, UyeDTO uye, AracDTO arac)
        {
            uye.KullaniciAdi = (abone.Adi + abone.Soyad).Trim().Replace(" ", string.Empty).ToLower();
            uye.Parola = PassManager.Base64Encrypt(abone.TCNO);
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
            model.AboneOdeme = _aboneService.GetAllSubscriberPayment(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SubscriberPaymentAdd(AboneOdemeDTO odeme)
        {
            if (odeme.Tutar != null)
            {
                odeme.OdemeTarihi = DateTime.Now;
                _aboneService.SubscriberPaymentInsert(odeme);
                return Json("/SubscriberList", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        [Route("SubscriberPaymentInvoice/{id}")]
        public ActionResult SubscriberPaymentInvoice(int id)
        {
            AboneViewModel model = new AboneViewModel();
            model = AboneModel(id);
            model.Odeme = _aboneService.SubscriberPayment(id);
            return View(model);
        }

    }
}