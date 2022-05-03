using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleMIS.IDAL;
using System.Configuration;

namespace SaleMIS.DALFactory
{
    public abstract class AbstractFactory
    {
        //选择实际工厂，创建工厂对象
        public static AbstractFactory ChooseFactory()
        {
            //根据条件选择（写在配置文件中）
            string dbtype = ConfigurationManager.AppSettings["DAL"];
            AbstractFactory af = null;
            //创建工厂对象
            switch (dbtype)
            {
                case "SQLSERVER": af = new SqlServerFactory(); break;
            }

            return af;
        }

        //约束实际工厂的功能:创建各种DAO类对象，返回的是接口对象
        public abstract ICategories CreateCategoryDAO();
        public abstract ICustomers CreateCustomerDAO();
        public abstract IProducts CreateProductDAO();
        public abstract IOrders CreateOrderDAO();
        public abstract IOrderDetails CreateOrderDetailDAO();


    }
}
