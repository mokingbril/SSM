using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Drawing;

namespace SSM.MVC.Controllers
{
    public class CommonController : Controller
    {
        //验证码；
        [HttpGet]
        public ContentResult CheckCode()
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "image/jpeg";

            //加载资源；
            char[] MyCode = new char[62];
            char ch = '0';
            for (int i = 0; i < 10; i++)
            {
                MyCode[i] = ch;
                ch++;
            }
            ch = 'A';
            for (int i = 10; i < 36; i++)
            {
                MyCode[i] = ch;
                ch++;
            }
            ch = 'a';
            for (int i = 36; i < MyCode.Length; i++)
            {
                MyCode[i] = ch;
                ch++;
            }

            //生成并保存验证码；
            Random random = new Random();
            string idCode = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(0, MyCode.Length - 1);
                idCode += MyCode[index];
            }

            Session["SecurityCode"] = idCode;

            //处理到图像中
            Bitmap bitmap = new Bitmap(100, 40);
            Graphics gps = Graphics.FromImage(bitmap);
            gps.FillRectangle(Brushes.White, 1, 1, 98, 38);
            gps.DrawString(idCode, new Font("Times New Roman", 17, FontStyle.Italic), Brushes.Black, 8, 8);

            //干扰
            for (int i = 0; i < 100; i++)
            {
                bitmap.SetPixel(random.Next(1, 98), random.Next(1, 38), Color.Gray);
            }
            for (int i = 0; i < 100; i++)
            {
                bitmap.SetPixel(random.Next(1, 98), random.Next(1, 38), Color.Orange);
                bitmap.SetPixel(random.Next(1, 98), random.Next(1, 38), Color.White);
            }
            for (int i = 0; i < 100; i++)
            {
                bitmap.SetPixel(random.Next(1, 98), random.Next(1, 38), Color.Red);
            }
            gps.DrawLine(new Pen(Brushes.OrangeRed), 10, 10, 90, 30);
            gps.DrawLine(new Pen(Brushes.Green), 15, 20, 70, 25);
            gps.DrawLine(new Pen(Brushes.Yellow), 5, 15, 80, 15);
            gps.DrawLine(new Pen(Brushes.White), 10, 20, 80, 25);

            //写回到Response
            bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return cr;
        }

        //强制跳转登录；
        [HttpGet]
        public ActionResult Login()
        {
            return RedirectToAction("Login", "Default");
        }

        //报错；
        public ActionResult ShowError()
        {
            return View("Error");
        }

        //权限不足提示；
        public ActionResult AuthError()
        {
            return View("AuthError");
        }

    }
}
