using CARPARK.COMMON;
using CARPARK.Data.UnitofWork;
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
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;
        public DashboardController(IUnitofWork uow, IPersonelService personelService)
        {
            _uow = uow;
            _personelService = personelService;
            _sessionContext = new SessionContext();
        }

        [Route("Dashboard")]
        public ActionResult Index()
        {
            //Personel Id göre bagıntılı tablolardan veri cek!
            var model = new PersonelViewModel();
            model.PersonelListesi = _personelService.GetAllPersonel();
           
            return View(model);
        }

        [Route("Subscribers")]
        public ActionResult Subscribers()
        {
            return View();
        }

        [Route("AddSubscriber")]
        public ActionResult AddSubscriber()
        {
            return View();
        }

        [HttpPost]
        [Route("DetailSubscriber")]
        public ActionResult DetailSubscriber(int id)
        {
            var model = new PersonelViewModel();
            //model.Personel = _personelService.Personel(id);
            return View();
        }

        [Route("UpdateSubscriber")]
        public ActionResult UpdateSubscriber()
        {
            return View();
        }
    }
}