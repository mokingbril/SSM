using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SSM.BLL;
using SSM.Models;
using SSM.MVC.Models;
using SSM.MVC.Extends;
using System.Web.Script.Serialization;
using System.Collections;

namespace SSM.MVC.Controllers
{
    [AuthorityCheck(3)]
    public class AdminController : Controller
    {
        private AdminManager Manager = new AdminManager();

        //管理员管理主页；
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //分页获取管理员集合；
        [HttpGet]
        public ContentResult GetAll()
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/json";
            int PageIndex = 1; int PageSize = 10; int Pages = 0;
            if (Request.QueryString["page"] != null)
            {
                PageIndex = int.Parse(Request.QueryString["page"]);
                PageSize = int.Parse(Request.QueryString["rows"]);
            }
            List<Admin> AllAdmins = Manager.GetAdmins(PageIndex, PageSize, out Pages);

            ArrayList all = new ArrayList();
            foreach (var ad in AllAdmins)
            {
                all.Add(new { Id = ad.Id, LoginName = ad.LoginName, LoginPwd = ad.LoginPwd });
            }
            var result = new { total = Pages * PageSize, rows = all };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            cr.Content = jss.Serialize(result);
            return cr;
        }

        //管理员管理；
        [HttpPost]
        public ContentResult Add(Admin tea)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            if (Manager.GetAdmin(tea.LoginName) == null)
            {
                Manager.Add(tea);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Update(Admin tea)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Admin t = Manager.GetAdmin(tea.Id);
            if (t != null && Manager.CheckUpdate(tea))
            {
                t.LoginPwd = tea.LoginPwd;
                t.LoginName = tea.LoginName;
                Manager.Update(t);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Delete(Admin ad)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Admin tr = Manager.GetAdmin(ad.Id);
            if (tr != null)
            {
                Manager.Delete(tr);
                cr.Content = "OK";
            }
            return cr;
        }

    }
}
