using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARPARK.Web.Controllers
{
    public class DashboardController : Controller
    {

        [Route("Dashboard")]
        public ActionResult Index()
        {
            return View();
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
    }
}