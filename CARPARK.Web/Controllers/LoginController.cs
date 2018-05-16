using CARPARK.COMMON;
using CARPARK.COMMON.PassOperations;
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
    public class LoginController : Controller
    {
        public ActionResult Index() {return View(); }
        private readonly ILoginService _loginService;
        private readonly IUnitofWork _uow;
        private SessionContext _sessionContext;

        public LoginController(IUnitofWork uow, ILoginService loginService)
        {
            _uow = uow;
            _loginService = loginService;
            _sessionContext = new SessionContext();
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginDTO uye)
        {
            uye.Parola = PassManager.Base64Encrypt(uye.Parola);//Uye parola şifre çözümü yapıldı.
            var result = _loginService.GetUserInformation(uye.KullaniciAdi, uye.Parola);
            if (result != null)
            {
                //install-package automapper -version:4.1 kurulumu yapılacak.
                AutoMapper.Mapper.DynamicMap(result, _sessionContext);
                Session["SessionContext"] = _sessionContext;
                Session["UyeID"] = _sessionContext.UyeID;
                Session["KullaniciAdi"] = _sessionContext.KullaniciAdi;

                return Json("/Dashboard", JsonRequestBehavior.AllowGet);

            }
            else
                return Json("", JsonRequestBehavior.AllowGet);

        }
        [Route("LogOut")]
        public ActionResult LogOut()
        {
            Session.Abandon(); //butun sessionları temizler.
            Response.Redirect("/login");
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}