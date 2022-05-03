using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using log4net;
using System.Threading;

namespace SSM.MVC.Extends
{
    public class LogHelper
    {
        //异常信息的队列,先进先出的数据结构：进队列和出队列的方法
        public static Queue<string> ExcMsg;

        static LogHelper()
        {
            ExcMsg = new Queue<string>(); //创建队列
            //从线程池中取出一个工作线程作为队列处理的专用线程 
            ThreadPool.QueueUserWorkItem(u =>
            {
                while (true)
                {
                    string str = string.Empty;

                    if (ExcMsg == null)
                    {
                        continue;
                    }

                    lock (ExcMsg) //因为要从队列中取出信息，所以在修改前锁定队列
                    {
                        if (ExcMsg.Count > 0)
                            str = ExcMsg.Dequeue(); //如果队列中有信息，从队列的头部取出一条信息
                    }
                    //往日志文件里面写就可以了。
                    if (!string.IsNullOrEmpty(str))
                    {
                        ILog log = log4net.LogManager.GetLogger("Test");
                        log.Error(str);
                    }
                    if (ExcMsg.Count() <= 0)
                    {
                        Thread.Sleep(30);
                    }


                }
            });
        }

        public static void WriteLog(string msg)
        {
            //写日志的功能并不直接写硬盘文件，而是将信息放入队列中
            lock (ExcMsg)
            {
                ExcMsg.Enqueue(msg);
            }
        }

    }
}