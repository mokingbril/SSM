using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using SSM.BLL;
using SSM.Models;
using SSM.MVC.Models;

namespace SSM.MVC
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private JobManager Jmanager = new JobManager();
        private DepartmentManager Dmanager = new DepartmentManager();
        private GradeManager Gmanager = new GradeManager();

        protected void Application_Start()
        {
            //记录日志框架加载；
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //缓存不常变动的数据；
            if (HttpContext.Current.Cache["Job"] == null)
            {
                HttpContext.Current.Cache["Job"] = Jmanager.GetJobs();
            }
            if (HttpContext.Current.Cache["Department"] == null)
            {
                HttpContext.Current.Cache["Department"] = Dmanager.GetDepartments();
            }
            if (HttpContext.Current.Cache["Grade"] == null)
            {
                HttpContext.Current.Cache["Grade"] = Gmanager.GetGrades();
            }
        }

        protected void Session_Start()
        {
            //自动登录；
            HttpRequest Request = HttpContext.Current.Request;
            HttpCookie cookie = Request.Cookies["AutoAdmin"];
            if (cookie != null)
            {
                if (new AdminManager().GetAdmin(cookie.Value) == null)
                {
                    cookie.Expires = DateTime.Now.AddHours(-1);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    UserVo ur = new UserVo(cookie.Value, 1);
                    Session["LoginUser"] = ur;
                }
                
            }
            cookie = Request.Cookies["AutoStudent"];
            if (cookie != null)
            {
                if (new StudentManager().GetStudent(cookie.Value)==null)
                {
                    cookie.Expires = DateTime.Now.AddHours(-1);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    UserVo ur = new UserVo(cookie.Value, 2);
                    Session["LoginUser"] = ur;
                }
                
            }
            cookie = Request.Cookies["AutoTeacher"];
            if (cookie != null)
            {
                if (new TeacherManager().GetTeacher(cookie.Value) == null)
                {
                    cookie.Expires = DateTime.Now.AddHours(-1);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    UserVo ur = new UserVo(cookie.Value, 3);
                    Session["LoginUser"] = ur;
                }
            }
        }

    }
}