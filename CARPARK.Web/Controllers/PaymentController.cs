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
    public class PaymentController : Controller
    {
        private readonly IMoneyEntryService _gelirService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;

        public PaymentController(IUnitofWork uow, IMoneyEntryService gelirService)
        {
            _uow = uow;
            _gelirService = gelirService;
            _sessionContext = new SessionContext();
        }

        [Route("PaymentList")]
        [HandleError]
        public ActionResult PaymentList()
        {
            List<GelirlerDTO> list = _gelirService.GetAllMoneyList();
            return View(list);
        }
    }
}