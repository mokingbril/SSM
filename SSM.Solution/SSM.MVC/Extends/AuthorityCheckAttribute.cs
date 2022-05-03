using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SSM.Models;
using SSM.MVC.Models;
using System.Web.Mvc;

namespace SSM.MVC.Extends
{
    public class AuthorityCheckAttribute : AuthorizeAttribute
    {
        public int AuthLevel { get; private set; }

        public AuthorityCheckAttribute(int Level)
        {
            AuthLevel = Level;
        }

        //Auth(1):所有用户(游客)   Auth(2)注册用户   Auth(3)管理员；
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthLevel > 1)
            {
                var user = filterContext.HttpContext.Session["LoginUser"] as UserVo;
                if (user == null)
                {
                    filterContext.Result = new RedirectResult("/Common/Login");
                    return;
                }
                if (user == null || (user != null && AuthLevel == 3) && user.RId!=1)
                {
                    filterContext.Result = new RedirectResult("/Common/AuthError");
                    return;
                }
            }
        }

    }
}