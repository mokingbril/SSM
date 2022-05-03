using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleMIS.IDAL;
using SaleMIS.SQLServerDAL;

namespace SaleMIS.DALFactory
{
    public class SqlServerFactory : AbstractFactory
    {
        public override ICategories CreateCategoryDAO()
        {
            return new CategoryDAO();
        }

        public override ICustomers CreateCustomerDAO()
        {
            return new CustomerDAO();
        }

        public override IProducts CreateProductDAO()
        {
            return new ProductDAO();
        }

        public override IOrders CreateOrderDAO()
        {
            return new OrderDAO();
        }

        public override IOrderDetails CreateOrderDetailDAO()
        {
            return new OrderDetailDAO();
        }
    }
}
