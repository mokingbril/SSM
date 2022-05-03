using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SSM.BLL;
using SSM.Models;
using SSM.MVC.Extends;
using SSM.MVC.Models;
using System.Collections;
using System.Web.Script.Serialization;

namespace SSM.MVC.Controllers
{
    [AuthorityCheck(3)]
    public class GradeController : Controller
    {
        private GradeManager Manager = new GradeManager();

        //管理主页；
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //分页获取；
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
            List<Grade> AllGrades = Manager.GetGrades(PageIndex, PageSize, out Pages);

            ArrayList all = new ArrayList();
            foreach (var gd in AllGrades)
            {
                all.Add(new { GId=gd.GId,Name=gd.Name });
            }
            var result = new { total = Pages * PageSize, rows = all };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            cr.Content = jss.Serialize(result);
            return cr;
        }

        //管理年级信息；
        [HttpPost]
        public ContentResult Add(Grade gd)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            if (Manager.GetGrade(gd.Name) == null)
            {
                Manager.Add(gd);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Update(Grade gd)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Grade t = Manager.GetGrade(gd.GId);
            Grade g = Manager.GetGrade(gd.Name);
            if (t != null && g==null)
            {
                t.Name = gd.Name;
                Manager.Update(t);
                cr.Content = "OK";
            }
            return cr;
        }

        public ContentResult Delete(Grade gd)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Grade tr = Manager.GetGrade(gd.GId);
            if (tr != null)
            {
                StudentManager stm = new StudentManager();
                List<Student> Stus = stm.GetStudets(gd.GId);
                stm.AllDelete(Stus);
                Manager.Delete(tr);
                cr.Content = "OK";
            }
            return cr;
        }

    }
}
