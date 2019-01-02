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
        private readonly IMoneyEntryService _gelirService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;
        public CustomerController(IUnitofWork uow, ICarService aracService, ICustomerService musteriService, IMoneyEntryService gelirService)
        {
            _uow = uow;
            _aracService = aracService;
            _musteriService = musteriService;
            _gelirService = gelirService;
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
            AracDTO arac = new AracDTO();
            arac.MarkaID = musteri.MarkaID;
            arac.ModelID = musteri.ModelID;
            arac.Plaka = musteri.Plaka;
            int aracID;
            aracID = _aracService.GetCar(arac.Plaka);
            if (aracID == 0)
            {
                aracID = _aracService.Insert(arac);
            }
            musteri.HizmetTuru = "Park";
            if (musteri.Aciklama == null)
            {
                musteri.Aciklama = "Açıklama Girilmedi.";
            }
            if (musteri.Tutar == null)
            {
                musteri.Tutar = 0;
            }
            int musteriID = _musteriService.CustomerInsert(musteri, aracID);
            _musteriService.CustomerParkInsert(park, musteriID);
            return RedirectToAction("CustomerList", "Customer");
        }


        [HttpPost]
        [HandleError]
        public ActionResult CustomerWashingInsert(MusteriDTO musteri, MusteriYikamaDTO yikama)
        {
            AracDTO arac = new AracDTO();
            arac.MarkaID = musteri.MarkaID;
            arac.ModelID = musteri.ModelID;
            arac.Plaka = musteri.Plaka;
            int aracID;
            aracID = _aracService.GetCar(arac.Plaka);
            if (aracID == 0)
            {
                aracID = _aracService.Insert(arac);
            }
            musteri.HizmetTuru = "Yıkama";
            if (musteri.Aciklama == null)
            {
                musteri.Aciklama = "Açıklama Girilmedi.";
            }
            if (musteri.Tutar == null)
            {
                musteri.Tutar = 0;
            }
            int musteriID = _musteriService.CustomerInsert(musteri, aracID);
            _musteriService.CustomerWashingInsert(yikama, musteriID);
            return RedirectToAction("CustomerList", "Customer");
        }


        [Route("CustomerPayment/{id}")]
        [HandleError]
        public ActionResult CustomerPayment(int id)
        {
            MusteriDTO musteri = _musteriService.Customer(id);
            MusteriParkDTO park = _musteriService.CustomerPark(id);
            MusteriViewModel model = new MusteriViewModel();
            model.Musteri = musteri;
            model.Park = park;
            return View(model);
        }

        [HttpPost]
        [HandleError]
        public ActionResult CustomerPaymentInsert(MusteriParkDTO park)
        {
            DateTime bas = Convert.ToDateTime(park.GirisTarihi);
            DateTime son = Convert.ToDateTime(park.CikisTarihi);
            var fark = (son - bas).Hours;
            var fiyat = fark * 10;

            MusteriDTO musteri = _musteriService.Customer(park.MusteriID);
            musteri.Tutar = fiyat;
            musteri.Durum = false;

            bool durum = _musteriService.CustomerUpdate(musteri);
            if (durum == true)
            {
                bool parkdurum = _musteriService.CustomerParkUpdate(park);
                if (parkdurum == true)
                {
                    GelirlerDTO gelir = new GelirlerDTO();
                    gelir.GelirTuru = "Müsteri Park Ödeme";
                    gelir.OdemeTarihi = Convert.ToDateTime(park.CikisTarihi);
                    gelir.Tutar = Convert.ToDecimal(fiyat);
                    gelir.AracID = Convert.ToInt32(musteri.AracId);
                    _gelirService.Insert(gelir);
                }
            }
            return RedirectToAction("CustomerList", "Customer");
        }

    }
}