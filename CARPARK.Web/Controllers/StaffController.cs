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
    public class StaffController : Controller
    {
        private readonly IStaffService _personelService;
        private readonly IUserService _uyeService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;
        public StaffController(IUnitofWork uow, IStaffService personelService, IUserService uyeService)
        {
            _uow = uow;
            _personelService = personelService;
            _uyeService = uyeService;
            _sessionContext = new SessionContext();
        }
        [Route("StaffList")]
        public ActionResult StaffList()
        {
            List<PersonelDTO> liste = _personelService.GetAllStaff();
            return View(liste);
        }

        [Route("StaffAdd")]
        public ActionResult StaffAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StaffAdd(PersonelDTO personel, UyeDTO uye)
        {
            uye.KullaniciAdi = (personel.Ad + personel.Soyad).Trim().Replace(" ", string.Empty).ToLower();
            uye.Parola = PassManager.Base64Encrypt(personel.TCNo);
            int id = _uyeService.Insert(uye);
            _personelService.Insert(personel, id);
            return RedirectToAction("StaffList", "Staff");
        }

        [Route("StaffDetail/{id}")]
        public ActionResult StaffDetail(int id)
        {
            var personel = _personelService.Staff(id);
            PersonelViewModel model = new PersonelViewModel();
            model.Personel = personel;
            model.Uye = _uyeService.User(Convert.ToInt32(personel.UyeID));
            return View(model);
        }

        [Route("StaffUpdate/{id}")]
        public ActionResult StaffUpdate(int id)
        {
            var entity = _personelService.Staff(id);
            var model = new PersonelViewModel();
            model.Personel = entity;
            model.Uye = _uyeService.User(Convert.ToInt32(entity.UyeID));
            return View(model);
        }

        [HttpPost]
        public ActionResult StaffUpdate(PersonelDTO personel, UyeDTO uye)
        {
            _personelService.Update(personel);
            _uyeService.Update(uye);
            return RedirectToAction("StaffList", "Staff");
        }

        [HttpPost]
        public ActionResult StaffDelete(int id)
        {
            _personelService.Delete(id);
            return RedirectToAction("StaffList", "Staff");
        }
    }
}