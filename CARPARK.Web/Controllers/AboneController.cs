using CARPARK.COMMON;
using CARPARK.Data.UnitofWork;
using CARPARK.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARPARK.Web.Controllers
{
    public class AboneController : Controller
    {
        private readonly IAboneService _aboneService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;

        public AboneController(IUnitofWork uow, IAboneService aboneService)
        {
            _uow = uow;
            _aboneService = aboneService;
            _sessionContext = new SessionContext();
        }

        
        public ActionResult Index()
        {
            return View();
        }
    }
}