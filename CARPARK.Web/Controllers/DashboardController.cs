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
        private readonly ISubscriberService _aboneService;
        private readonly IUserService _uyeService;
        private readonly IMoneyEntryService _gelirService;
        private readonly ICustomerService _musteriService;
        private readonly ICarService _aracService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;
        public DashboardController(IUnitofWork uow, ICustomerService musteriService, ICarService aracService, ISubscriberService aboneService, IUserService uyeService, IMoneyEntryService gelirService)
        {
            _uow = uow;
            _musteriService = musteriService;
            _aracService = aracService;
            _aboneService = aboneService;
            _uyeService = uyeService;
            _gelirService = gelirService;
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

        public ActionResult AracPlakaOkumaModel(string Plaka, string Bolge, string Marka, string Kasa, string Model, string Renk)
        {
           
            int aracID;
            aracID = _aracService.GetCar(Plaka);
            if (aracID == 0)
            {
                AracDTO arac = new AracDTO();
                arac.Bolge = Bolge;
                arac.Plaka = Plaka;
                arac.Renk = Renk;
                arac.Kasa = Kasa;

                AracMarkaDTO marka = _aracService.GetCarBrand(Marka);
                if (marka != null)
                {
                    arac.MarkaID = marka.MarkaID;
                    AracModelDTO model = _aracService.GetCarModel(marka.MarkaID, Model.Split('_')[1]);
                    if (model != null)
                    {
                        arac.ModelID = model.ModelID;
                    }
                    else
                    {
                        AracModelDTO mo = new AracModelDTO();
                        mo.MarkaID = marka.MarkaID;
                        mo.Model = Model.Split('_')[1];
                        int modelId = _aracService.CarModelInsert(mo);

                        arac.ModelID = modelId;
                    }
                }
                else
                {
                    AracMarkaDTO m = new AracMarkaDTO();
                    m.Marka = Marka;
                    int markaId = _aracService.CarBrandInsert(m);

                    arac.MarkaID = markaId;

                    AracModelDTO mo = new AracModelDTO();
                    mo.MarkaID = markaId;
                    mo.Model = Model;
                    int modelId = _aracService.CarModelInsert(mo);

                    arac.ModelID = modelId;
                }

                aracID = _aracService.Insert(arac);
            }
            if (!_aracService.GetBlackListCarControl(aracID))
            {
                AboneDTO abone = _aboneService.GetSubscriber(aracID);
                if (abone != null)
                {
                    AboneGirisCikisDTO kontrol = new AboneGirisCikisDTO();
                    kontrol.AboneId = abone.AboneID;
                    kontrol.Durum = true;
                    kontrol.Tarih = DateTime.Now;
                    bool durum = _aboneService.SubscriberInputOutput(kontrol);
                    if (durum == true)
                    {
                        var islemdurum = abone.Islem;
                        if (islemdurum == "Giriş Yaptı")
                        {
                            abone.Islem = "Çıkış Yaptı";
                        }
                        else if (islemdurum == "Çıkış Yaptı")
                        {
                            abone.Islem = "Giriş Yaptı";
                        }
                        _aboneService.Update(abone);
                    }
                }
                else
                {
                    MusteriDTO musteri = new MusteriDTO();
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
                    MusteriParkDTO park = new MusteriParkDTO();
                    park.MusteriID = musteriID;
                    park.GirisTarihi = DateTime.Now;
                    _musteriService.CustomerParkInsert(park, musteriID);
                }
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [Route("BlackList")]
        public ActionResult BlackList()
        {
            List<KaraListeDTO> list = _aracService.GetBlackList();
            List<BlackListViewModel> blackList = new List<BlackListViewModel>();
            foreach (var item in list)
            {
                BlackListViewModel black = new BlackListViewModel();
                black.ID = item.ID;
                black.Aciklama = item.Aciklama;
                AracDTO arac = _aracService.Car(Convert.ToInt32(item.AracID));
                AracMarkaDTO marka = _aracService.Brand(Convert.ToInt32(arac.MarkaID));
                AracModelDTO model = _aracService.Model(Convert.ToInt32(arac.ModelID));
                black.Marka = marka.Marka;
                black.Model = model.Model;
                black.Plaka = arac.Plaka;
                blackList.Add(black);
            }
            return View(blackList);
        }

        [HandleError]
        public AracViewModel AracModel()
        {
            AracViewModel aracModel = new AracViewModel();
            aracModel.MarkaListesi = _aracService.GetAllBrand();
            return aracModel;
        }


        [HandleError]
        [Route("BlackListInsert")]
        public ActionResult BlackListInsert()
        {
            return View(AracModel());
        }

        [HandleError]
        [HttpPost]
        public ActionResult BlackListInsert(AracDTO arac, KaraListeDTO kara)
        {
            int aracId;
            aracId = _aracService.GetCar(arac.Plaka);
            if (aracId == 0)
            {
                aracId = _aracService.Insert(arac);
            }
            KaraListeDTO ka = new KaraListeDTO();
            ka.Aciklama = kara.Aciklama;
            ka.AracID = aracId;
            _aracService.BlackListCarInsert(ka);
            return RedirectToAction("BlackList", "Dashboard");
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