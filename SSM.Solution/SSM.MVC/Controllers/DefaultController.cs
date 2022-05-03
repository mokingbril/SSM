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
    public class DefaultController : Controller
    {
        private AdminManager Adminm = new AdminManager();
        private StudentManager Manager = new StudentManager();
        private TeacherManager Team = new TeacherManager();

        //主页面；
        [AuthorityCheck(2)]
        public ActionResult Index()
        {
            return View();
        }

        //登录；
        [HttpGet]
        public ActionResult Login() 
        {
            if (Session["LoginUser"] != null)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin stu)
        {
            //管理员登录；
            int RId = Convert.ToInt32(Request.Form["RId"]);
            if (RId == 1)
            {
                stu = Adminm.Login(stu);
                if (stu != null)
                {
                    AutoLogin(stu, RId);
                    return RedirectToAction("Index", "Default");
                }
                ViewBag.ErrorInfo = "用户名和密码与身份不一致！登录失败！";
            }

            //学生登录；
            if (RId == 2)
            {
                Student student = new Student {
                    StuNo = stu.LoginName,
                    LoginPwd = stu.LoginPwd
                };
                student = Manager.Login(student);
                if (student != null)
                {
                    AutoLogin(stu, RId);
                    return RedirectToAction("Index", "Default");
                }
                ViewBag.ErrorInfo = "用户名和密码与身份不一致！登录失败！";
            }

            //教师登录；
            if (RId == 3)
            {
                Teacher tea = new Teacher
                {
                    TeaNo = stu.LoginName,
                    LoginPwd = stu.LoginPwd
                };
                tea = Team.Login(tea);
                if (tea != null)
                {
                    AutoLogin(stu,RId);
                    return RedirectToAction("Index", "Default");
                }
                ViewBag.ErrorInfo = "用户名和密码与身份不一致！登录失败！";
            }

            return View();
        }

        //自动登录；
        private void AutoLogin(Admin ur, int RoleId)
        {
            UserVo user = new UserVo(ur.LoginName, RoleId);
            Session["LoginUser"] = user;
            if (Request.Form["AutoLogin"] == "true")
            {
                HttpCookie Cookie = null;
                if (RoleId==1)
                {
                    Cookie = new HttpCookie("AutoAdmin", user.LoginName);
                }
                else if (RoleId==2)
                {
                    Cookie = new HttpCookie("AutoStudent", user.LoginName);
                }
                else if (RoleId == 3)
                {
                    Cookie = new HttpCookie("AutoTeacher", user.LoginName);
                }
                Cookie.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Add(Cookie);
            }
        }

        //注销；
        [AuthorityCheck(2)]
        public ActionResult Logout() 
        {
            Session["LoginUser"] = null;
            return RedirectToAction("Login", "Default");
        }

        //注册；
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.StuCount = Manager.GetCount();
            ViewBag.TeaCount = Team.GetCount();
            return View();
        }

        //账号验证；
        [HttpPost]
        public ContentResult CheckLoginName(FormCollection fc) 
        {
            UserVo User = new UserVo(fc["LoginName"], Convert.ToInt32(fc["RId"]));
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/plain";
            cr.Content = "此号有效√";
            if (User.RId==2 && Manager.GetStudent(User.LoginName)!=null)
            {
                cr.Content = "×此学号已存在！";
            }
            if (User.RId==3 && Team.GetTeacher(User.LoginName)!=null)
            {
                cr.Content = "×此编号已存在！";
            }
            return cr;
        }

        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            if (Session["SecurityCode"]==null || fc["CheckCode"]==null)
            {
                ViewBag.ErrorInfo = "温馨提示：验证码不一致！";
                ViewBag.StuCount = Manager.GetCount();
                ViewBag.TeaCount = Team.GetCount();
                return View();
            }
            if (fc["CheckCode"].ToString() != Session["SecurityCode"].ToString())
            {
                ViewBag.ErrorInfo = "温馨提示：验证码不一致！";
                ViewBag.StuCount = Manager.GetCount();
                ViewBag.TeaCount = Team.GetCount();
                return View();
            }
            if (fc["role"] == "2")
            {
                if (Manager.GetStudent(fc["StuNo"]) != null)
                {
                    ViewBag.ErrorInfo = "温馨提示：此号已存在！";
                    ViewBag.StuCount = Manager.GetCount();
                    ViewBag.TeaCount = Team.GetCount();
                    return View();
                }
                DateTime dt=DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fc["Birthday"]))
                {
                    DateTime.TryParse(fc["Birthday"], out dt);
                }
                
                Student stu = new Student
                {
                    StuNo = fc["StuNo"],
                    LoginPwd = fc["LoginPwd"],
                    Name = fc["Name"],
                    Sex = fc["Sex"] == "0" ? false : true,
                    DId = Convert.ToInt32(fc["department"]),
                    GId = Convert.ToInt32(fc["gride"]),
                    Phone = fc["Phone"],
                    Address = fc["Address"],
                    Birthday=dt
                };
                Manager.Add(stu);
                UserVo User = new UserVo(stu.StuNo, 2);
                Session["LoginUser"] = User;
            }
            else
            {
                if (Team.GetTeacher(fc["TeaNo"]) != null)
                {
                    ViewBag.ErrorInfo = "温馨提示：此号已存在！";
                    ViewBag.StuCount = Manager.GetCount();
                    ViewBag.TeaCount = Team.GetCount();
                    return View();
                }
                Teacher teacher = new Teacher
                {
                    TeaNo = fc["TeaNo"],
                    Name = fc["Name"],
                    LoginPwd = fc["LoginPwd"],
                    Sex = fc["Sex"] == "0" ? false : true,
                    JId = Convert.ToInt32(fc["job"]),
                };
                Team.Add(teacher);
                UserVo User = new UserVo(teacher.TeaNo, 3);
                Session["LoginUser"] = User;
            }
            return RedirectToAction("Index", "Default");
        }

        //个人信息；
        [HttpGet]
        [AuthorityCheck(2)]
        public ActionResult UserDetails() 
        {
            var User = Session["LoginUser"] as UserVo;
            if (User.RId==1)
            {
                Admin admin = Adminm.GetAdmin(User.LoginName);
                ViewBag.User = admin;
            }
            else if(User.RId==2)
            {
                Student Stu = Manager.GetStudent(User.LoginName);
                ViewBag.User = Stu;
            }
            else
            {
                Teacher Tea = Team.GetTeacher(User.LoginName);
                ViewBag.User = Tea;
            }
            ViewBag.RoleId = User.RId;
            return View();
        }

        //修改个人档案信息；
        [HttpGet]
        [AuthorityCheck(2)]
        public ActionResult UserUpdate()
        {
            var User = Session["LoginUser"] as UserVo;
            if (User.RId == 1)
            {
                Admin admin = Adminm.GetAdmin(User.LoginName);
                ViewBag.User = admin;
            }
            else if (User.RId == 2)
            {
                Student Stu = Manager.GetStudent(User.LoginName);
                ViewBag.User = Stu;
            }
            else
            {
                Teacher Tea = Team.GetTeacher(User.LoginName);
                ViewBag.User = Tea;
            }
            ViewBag.RoleId = User.RId;
            List<Grade> AllG = new GradeManager().GetGrades();
            List<Department> AllD = new DepartmentManager().GetDepartments();
            ViewBag.AllG = AllG;
            ViewBag.AllD = AllD;
            return View();
        }

        [HttpPost]
        [AuthorityCheck(2)]
        public ActionResult UserUpdate(FormCollection fc)
        {
            var User = Session["LoginUser"] as UserVo;
            int RoleId = User.RId;
            if (string.IsNullOrWhiteSpace(fc["LoginPwd"]))
            {
                return RedirectToAction("Index", "Default");
            }
            if (RoleId == 1)
            {
                Admin admin = Adminm.GetAdmin(Convert.ToInt32(fc["Id"]));
                if (admin != null && Adminm.CheckUpdate(admin) && !string.IsNullOrWhiteSpace(fc["LoginName"]))
                {
                    admin.LoginName = fc["LoginName"];
                    admin.LoginPwd = fc["LoginPwd"];
                    Adminm.Update(admin);
                    UserVo ur = new UserVo(admin.LoginName, RoleId);
                    Session["LoginUser"] = ur;
                }
            }
            else if (RoleId == 2)
            {
                Student Stu = Manager.GetStudent(fc["StuNo"]);
                if (Stu != null && !string.IsNullOrWhiteSpace(fc["StuNo"]))
                {
                    Stu.Name = fc["Name"];
                    Stu.LoginPwd = fc["LoginPwd"];
                    Stu.Sex = Convert.ToBoolean(fc["Sex"]);
                    Stu.DId = Convert.ToInt32(fc["DId"]);
                    Stu.GId = Convert.ToInt32(fc["GId"]);
                    Stu.Phone = fc["Phone"];
                    DateTime dtt;
                    if (DateTime.TryParse(fc["Birthday"], out dtt))
                    {
                        Stu.Birthday = dtt;
                    }
                    if (string.IsNullOrWhiteSpace(fc["Birthday"]))
                    {
                        Stu.Birthday = null;
                    }
                    Stu.Address = fc["Address"];
                    Manager.Update(Stu);
                }
            }
            else
            {
                Teacher Tea = Team.GetTeacher(fc["TeaNo"]);
                if (Tea != null && !string.IsNullOrWhiteSpace(fc["TeaNo"]))
                {
                    Tea.Name = fc["Name"];
                    Tea.LoginPwd = fc["LoginPwd"];
                    Tea.Sex = Convert.ToBoolean(fc["Sex"]);
                    Tea.JId = Convert.ToInt32(fc["JId"]);
                    Team.Update(Tea);
                }
            }
            return RedirectToAction("Index", "Default");
        }

    }
}
