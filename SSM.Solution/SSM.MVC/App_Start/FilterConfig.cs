using System.Web;
using System.Web.Mvc;

using SSM.MVC.Extends;

namespace SSM.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ProcessExceptionAttribute());
        }
    }
}