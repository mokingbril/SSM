using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.IO;

namespace SSM.MVC.Extends
{
    public class ProcessExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //调异常；
            Exception exp = filterContext.Exception;
            //构建异常信息的字符串；
            string ExpMsg = exp.ToString();

            //记录日志
            LogHelper.WriteLog(ExpMsg);


            //标记此次发生的异常已经处理；
            filterContext.ExceptionHandled = true;
            //将~/Views/Shared/Error.cshtml显示出来；
            filterContext.Result = new RedirectResult("/Common/ShowError");
        }
    }
}