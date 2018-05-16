using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CARPARK.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes(); //Url ayarlamamıza izin verir.


            //Oluşan olası hataları bu şekilde Web.config ile yönlendirebilmekteyiz. 
            routes.MapRoute(
              name: "Hata",
              url: "Hata/{kod}",
              defaults: new { controller = "Error", action = "Page404", kod = UrlParameter.Optional }
          );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
