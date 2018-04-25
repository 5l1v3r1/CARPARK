using CARPARK.Web.Code;
using System.Web;
using System.Web.Mvc;

namespace CARPARK.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorAttribute());
        }
    }
}
