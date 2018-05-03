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
    public class DashboardController : Controller
    {
        private readonly IPersonelService _personelService;
        private readonly IUyeService _uyeService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;
        public DashboardController(IUnitofWork uow, IPersonelService personelService, IUyeService uyeService)
        {
            _uow = uow;
            _personelService = personelService;
            _uyeService = uyeService;
            _sessionContext = new SessionContext();
        }

        [Route("Dashboard")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Subscribers")]
        public ActionResult Subscribers()
        {
            List<PersonelUyeDTO> liste = _personelService.GetAllPersonel();
            return View(liste);
        }

        [Route("AddSubscriber")]
        public ActionResult AddSubscriber()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSubscriber(PersonelUyeDTO personel)
        {
            _personelService.Insert(personel);
            return RedirectToAction("Subscribers", "Dashboard");
        }

        [HttpPost]
        [Route("DetailSubscriber")]
        public ActionResult DetailSubscriber(int id)
        {
            return View();
        }

        [Route("UpdateSubscriber/{id}")]
        public ActionResult UpdateSubscriber(int id)
        {
            var entity = _personelService.Personel(id);
            var model = new PersonelViewModel();
            model.Personel = entity;
            model.Uye = _uyeService.Uye(Convert.ToInt32(entity.UyeID));
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateSubscriber(PersonelUyeDTO personel)
        {
            _personelService.Update(personel);
            return RedirectToAction("Subscribers", "Dashboard");
        }

    }
}