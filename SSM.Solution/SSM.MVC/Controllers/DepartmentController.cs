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
    public class DepartmentController : Controller
    {
        private DepartmentManager Manager = new DepartmentManager();

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
            List<Department> AllDepartment = Manager.GetDepartments(PageIndex, PageSize, out Pages);

            ArrayList all = new ArrayList();
            foreach (var dt in AllDepartment)
            {
                all.Add(new { DId = dt.DId, Name = dt.Name });
            }
            var result = new { total = Pages * PageSize, rows = all };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            cr.Content = jss.Serialize(result);
            return cr;
        }

        //管理系院信息；
        [HttpPost]
        public ContentResult Add(Department dt)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            if (Manager.GetDepartment(dt.Name) == null)
            {
                Manager.Add(dt);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Update(Department dt)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Department t = Manager.GetDepartment(dt.DId);
            Department g = Manager.GetDepartment(dt.Name);
            if (t != null && g == null)
            {
                t.Name = dt.Name;
                Manager.Update(t);
                cr.Content = "OK";
            }
            return cr;
        }

        public ContentResult Delete(Department dt)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Department tr = Manager.GetDepartment(dt.DId);
            if (tr != null)
            {
                StudentManager stm = new StudentManager();
                List<Student> Stus = stm.GetStudetsByD(dt.DId);
                stm.AllDelete(Stus);
                Manager.Delete(tr);
                cr.Content = "OK";
            }
            return cr;
        }

    }
}
