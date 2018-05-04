using CARPARK.COMMON;
using CARPARK.Data.UnitofWork;
using CARPARK.DTO.EntitisDTO;
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
        private readonly IUserService _uyeService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;

        public SubscriberController(IUnitofWork uow, ISubscriberService aboneService, IUserService uyeService)
        {
            _uow = uow;
            _aboneService = aboneService;
            _uyeService = uyeService;
            _sessionContext = new SessionContext();
        }

        [Route("SubscriberList")]
        public ActionResult SubscriberList()
        {
            List<AboneDTO> liste = _aboneService.GetAllSubscriber();
            return View(liste);
        }
    }
}