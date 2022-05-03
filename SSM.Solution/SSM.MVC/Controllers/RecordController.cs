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
    public class RecordController : Controller
    {
        private RecordManager Manager = new RecordManager();
        private SubjectManager Subm = new SubjectManager();
        private StudentManager Stum = new StudentManager();
        
        //成绩管理主页；
        [AuthorityCheck(3)]
        public ActionResult Index()
        {
            List<Subject> AllSubs = Subm.GetSubjects();
            ViewBag.Subs = AllSubs;
            return View();
        }

        [AuthorityCheck(2)]
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
            List<Record> Records = Manager.GetRecords(PageIndex, PageSize, out Pages).OrderByDescending(r=>r.Tip).ToList();

            ArrayList all = new ArrayList();
            foreach (var s in Records)
            {
                all.Add(new
                {
                    SubName = s.Subject.Name,
                    StuNo = s.StuNo,
                    Id = s.Id,
                    Score = s.Score == null ? "缺考" : s.Score.ToString(),
                    ExamTime = s.ExamTime.ToLongDateString()+" "+s.ExamTime.ToLongTimeString(),
                    Tip = s.Tip,
                    TrueScore = s.TrueScore
                });

            }
            var result = new { total = Pages * PageSize, rows = all };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            cr.Content = jss.Serialize(result);
            return cr;
        }

        //查询成绩；
        [HttpGet]
        [AuthorityCheck(2)]
        public ActionResult Seek()
        {
            List<Subject> AllSubs = Subm.GetSubjects();
            ViewBag.Subs = AllSubs;
            return View();
        }

        [HttpGet]
        [AuthorityCheck(2)]
        public ContentResult GetStu()
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/json";
            int PageIndex = 1; int PageSize = 10; int Pages = 0, Option = 1;
            List<Record> Records = null;

            if (Request.QueryString["data[Level]"]!=null && Request.QueryString["data[Level]"]=="true")
            {
                //分级查询；
                int DID = 1, GID = 1, SubID = 1; double Score = 0;
                if (Request.QueryString["page"] != null)
                {
                    PageIndex = int.Parse(Request.QueryString["page"]);
                    PageSize = int.Parse(Request.QueryString["rows"]);
                    DID = int.Parse(string.IsNullOrWhiteSpace(Request.QueryString["data[DID]"]) ? "1" : Request.QueryString["data[DID]"]);
                    GID = int.Parse(string.IsNullOrWhiteSpace(Request.QueryString["data[GID]"]) ? "0" : Request.QueryString["data[GID]"]);
                    SubID = int.Parse(string.IsNullOrWhiteSpace(Request.QueryString["data[SubID]"]) ? "0" : Request.QueryString["data[SubID]"]);
                    Score = double.Parse(string.IsNullOrWhiteSpace(Request.QueryString["data[Score]"]) ? "-1" : Request.QueryString["data[Score]"]);
                }
                Records = Manager.GetRecords(PageIndex, PageSize, DID, GID, SubID, Score, out Pages);
            }
            else
            {
                string stuNo = string.Empty;
                if (Request.QueryString["page"] != null)
                {
                    PageIndex = int.Parse(Request.QueryString["page"]);
                    PageSize = int.Parse(Request.QueryString["rows"]);
                    stuNo = Request.QueryString["data[StuNo]"] == null ? "" : Request.QueryString["data[StuNo]"];
                    Option = int.Parse(Request.QueryString["data[Option]"] == null ? "1" : Request.QueryString["data[Option]"]);//操作数；
                }
                Records = Manager.GetRecords(PageIndex, PageSize, stuNo, Option, out Pages);
            }

            ArrayList all = new ArrayList();
            foreach (var s in Records)
            {
                all.Add(new
                {
                    SubName = s.Subject.Name,
                    StuNo = s.StuNo,
                    Name = s.Student.Name,
                    Sex = s.Student.Sex ? "男" : "女",
                    DName = s.Student.Department.Name,
                    GName = s.Student.Grade.Name,
                    Id = s.Id,
                    Score = s.Score == null ? "缺考" : s.Score.ToString(),
                    ExamTime = s.ExamTime.ToLongDateString() + " " + s.ExamTime.ToLongTimeString(),
                    Tip=s.Tip,
                    TrueScore=s.TrueScore
                });
            }
            var result = new { total = Pages * PageSize, rows = all };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            cr.Content = jss.Serialize(result);
            return cr;
        }

        //错误申请；
        [HttpPost]
        [AuthorityCheck(2)]
        public ContentResult ErrorApply(Record rd)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Record t = Manager.GetRecord(rd.StuNo,Request.Form["SubName"]);
            if (t != null)
            {
                t.Tip = rd.Tip;
                t.TrueScore = rd.TrueScore;
                Manager.Update(t);
                cr.Content = "OK";
            }
            return cr;
        }

        //申请录入成绩；
        [HttpGet]
        [AuthorityCheck(2)]
        public ActionResult ApplyInsert()
        {
            List<Subject> AllSubs = Subm.GetSubjects();
            ViewBag.Subs = AllSubs;
            return View();
        }

        [HttpPost]
        [AuthorityCheck(2)]
        public ContentResult ApplyInsert(Record rd)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            UserVo User = Session["LoginUser"] as UserVo;
            if (User!=null && User.RId==2)
            {
                rd.StuNo = User.LoginName;
                rd.Tip = "申请录入此成绩";
                rd.Score = null;
                return Add(rd);
            }
            return cr;
        }

        //录入成绩；
        [AuthorityCheck(2)]
        [HttpGet]
        public ActionResult Inserts()
        {
            UserVo User = Session["LoginUser"] as UserVo;
            if (User.RId!=3)
            {
                return RedirectToAction("Index","Default");
            }
            List<Subject> AllSubs = Subm.GetSubjects();
            ViewBag.Subs = AllSubs;
            return View();
        }

        //成绩管理-录入成绩；
        [AuthorityCheck(2)]
        [HttpPost]
        public ContentResult Add(Record rd)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            DateTime TestTime;
            if (!DateTime.TryParse(Request.Form["ExamTime"], out TestTime) || Stum.GetStudent(rd.StuNo) == null || Manager.CheckOne(rd))
            {
                return cr;
            }
            if (Manager.GetRecord(rd.Id) == null)
            {
                Manager.Add(rd);
                cr.Content = "OK";
            }
            return cr;
        }

        //成绩管理；
        [AuthorityCheck(3)]
        [HttpPost]
        public ContentResult Update(Record rd)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            DateTime TestTime;
            if (!DateTime.TryParse(Request.Form["ExamTime"], out TestTime) || Stum.GetStudent(rd.StuNo) == null || Manager.CheckUpdate(rd) == false || rd.SubId == 0)
            {
                return cr;
            }
            Record t = Manager.GetRecord(rd.Id);
            if (t != null)
            {
                t.SubId = rd.SubId;
                t.StuNo = rd.StuNo;
                t.Score = rd.Score;
                t.ExamTime = rd.ExamTime;
                t.Tip = rd.Tip;
                t.TrueScore = rd.TrueScore;
                Manager.Update(t);
                cr.Content = "OK";
            }
            return cr;
        }

        [AuthorityCheck(3)]
        [HttpPost]
        public ContentResult Delete(Record rd)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "LOST";
            Record tr = Manager.GetRecord(rd.Id);
            if (tr != null)
            {
                Manager.Delete(tr);
                cr.Content = "OK";
            }
            return cr;
        }
    }
}
