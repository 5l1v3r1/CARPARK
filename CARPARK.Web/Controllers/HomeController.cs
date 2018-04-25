using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARPARK.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        public  ActionResult Login()
        {
            return View();
        }


        [Route("Contact")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}