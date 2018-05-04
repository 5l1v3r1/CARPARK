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

        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;
        public DashboardController(IUnitofWork uow)
        {
            _uow = uow;
            _sessionContext = new SessionContext();
        }

        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}