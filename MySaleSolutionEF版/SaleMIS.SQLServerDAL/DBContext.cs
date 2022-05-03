using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleMIS.Model;
using System.Data.Entity;

namespace SaleMIS.SQLServerDAL
{
    public class DBContext : DbContext
    {
        public DBContext()
            : base("name=MySaleConnection")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

    }
}
