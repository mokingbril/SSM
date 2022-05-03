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
    public class SubjectController : Controller
    {
        private SubjectManager Manager = new SubjectManager();
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
            int PageIndex = 1; int PageSize = 10; int Pages = 0;
            if (Request.QueryString["page"] != null)
            {
                PageIndex = int.Parse(Request.QueryString["page"]);
                PageSize = int.Parse(Request.QueryString["rows"]);
            }
            List<Subject> Subjects = Manager.GetSubjects(PageIndex, PageSize, out Pages);

            ArrayList all = new ArrayList();
            foreach (var s in Subjects)
            {
                all.Add(new
                {
                    SubId=s.SubId,
                    Name = s.Name,
                    Period = s.Period,
                    Score=s.Score
                });

            }
            var result = new { total = Pages * PageSize, rows = all };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            cr.Content = jss.Serialize(result);
            return cr;
        }

        //管理课程信息；
        [HttpPost]
        public ContentResult Add(Subject sub)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            double score = 0;
            if (!double.TryParse(Request.Form["Score"], out score) || Manager.CheckOne(sub))
            {
                return cr;
            }
            if (Manager.GetSubject(sub.SubId) == null)
            {
                Manager.Add(sub);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Update(Subject sub)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            double score = 0;
            if (!double.TryParse(Request.Form["Score"], out score) || Manager.CheckUpdate(sub)==false)
            {
                return cr;
            }
            Subject t = Manager.GetSubject(sub.SubId);
            if (t != null)
            {
                t.Name = sub.Name;
                t.Period = sub.Period;
                t.Score = sub.Score;
                Manager.Update(t);
                cr.Content = "OK";
            }
            return cr;
        }

        [HttpPost]
        public ContentResult Delete(Subject sub)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Subject tr = Manager.GetSubject(sub.SubId);
            if (tr != null)
            {
                List<Record> Scores = Scorem.GetRecords(tr.SubId);
                Scorem.AllDelete(Scores);
                Manager.Delete(tr);
                cr.Content = "OK";
            }
            return cr;
        }

    }
}
