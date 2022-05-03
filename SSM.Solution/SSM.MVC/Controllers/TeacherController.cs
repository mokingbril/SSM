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
    public class TeacherController : Controller
    {
        private TeacherManager Manager = new TeacherManager();
        private JobManager Jobm = new JobManager();

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
            List<Teacher> AllTeachers = Manager.GetTeachers(PageIndex, PageSize, out Pages);

            ArrayList all = new ArrayList();
            foreach (var tea in AllTeachers)
            {
                all.Add(new {TeaNo=tea.TeaNo,Name=tea.Name,LoginPwd=tea.LoginPwd,Sex=tea.Sex==true?"男":"女",JName=tea.Job.Name });
            }
            var result = new  { total = Pages * PageSize, rows = all };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            cr.Content = jss.Serialize(result);
            return cr;
        }

        //管理教师执教信息；
        [HttpPost]
        public ContentResult Add(Teacher tea)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            if (Manager.GetTeacher(tea.TeaNo)==null)
            {
                Manager.Add(tea);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Update(Teacher tea)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Teacher t = Manager.GetTeacher(tea.TeaNo);
            if (t != null)
            {
                t.LoginPwd = tea.LoginPwd;
                t.Name = tea.Name;
                t.Sex = tea.Sex;
                t.JId = tea.JId;
                Manager.Update(t);
                cr.Content = "OK";
            }
            return cr;
        }

        public ContentResult Delete(Teacher tea)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Teacher tr = Manager.GetTeacher(tea.TeaNo);
            if (tr != null)
            {
                Manager.Delete(tr);
                cr.Content = "OK";
            }
            return cr;
        }
    }
}
