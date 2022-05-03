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
    public class StudentController : Controller
    {
        private StudentManager Manager = new StudentManager();
        private RecordManager Scorem = new RecordManager();

        //管理主页；
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
            int PageIndex = 1; int PageSize = 2; int Pages = 0;
            if (Request.QueryString["page"] != null)
            {
                PageIndex = int.Parse(Request.QueryString["page"]);
                PageSize = int.Parse(Request.QueryString["rows"]);
            }
            List<Student> Students = Manager.GetStudets(PageIndex, PageSize, out Pages);

            ArrayList all = new ArrayList();
            foreach (var s in Students)
            {
                all.Add(new
                {
                    StuNo = s.StuNo,
                    LoginPwd = s.LoginPwd,
                    Name = s.Name,
                    Sex = s.Sex == true ? "男" : "女",
                    DName = s.Department.Name,
                    GName = s.Grade.Name,
                    Phone = s.Phone,
                    Address = s.Address,
                    Birthday = s.Birthday!=null?s.Birthday.Value.ToLongDateString():null
                });
            }
            var result = new { total = Pages * PageSize, rows = all };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            cr.Content = jss.Serialize(result);
            return cr;
        }

        //管理学生档案；
        [HttpPost]
        public ContentResult Add(Student stu)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            if (new DepartmentManager().GetDepartment(stu.DId)==null || new GradeManager().GetGrade(stu.GId)==null)
            {
                return cr;
            }
            if (Manager.GetStudent(stu.StuNo) == null)
            {
                Manager.Add(stu);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Update(Student stu)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Student t = Manager.GetStudent(stu.StuNo);
            if (t != null)
            {
                t.LoginPwd = stu.LoginPwd;
                t.Name = stu.Name;
                t.Sex = stu.Sex;
                t.DId = stu.DId;
                t.GId = stu.GId;
                t.Phone = stu.Phone;
                t.Birthday = stu.Birthday;
                t.Address = stu.Address;
                Manager.Update(t);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Delete(Student stu)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Student tr = Manager.GetStudent(stu.StuNo);
            if (tr != null)
            {
                List<Record> Scores = Scorem.GetRecords(tr.StuNo);
                Scorem.AllDelete(Scores);
                Manager.Delete(tr);
                cr.Content = "OK";
            }
            return cr;
        }
    }
}
