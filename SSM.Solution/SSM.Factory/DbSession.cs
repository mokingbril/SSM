using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SSM.IDAL;
using SSM.Models;
using System.Reflection;
using System.Configuration;

namespace SSM.Factory
{
    public class DbSession
    {
        //创建各种DAO类对象的方法,T代表IDAL中的接口
        public T CreateDAO<T>()
        {
            string assemblyName = ConfigurationManager.AppSettings["DALAssembly"];
            Type[] types = Assembly.Load(assemblyName).GetTypes();
            Type target = null;
            foreach (Type type in types)
            {
                if (typeof(T).IsAssignableFrom(type))
                {
                    target = type;
                    break;
                }
            }

            return (T)Activator.CreateInstance(target);
        }

        //一次提交
        public void SaveChanges()
        {
            EFDbContextFactory.GetContext().SaveChanges();
        }
    }
}
