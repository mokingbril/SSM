using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleMIS.IDAL;
using SaleMIS.Model;

namespace SaleMIS.SQLServerDAL
{
    public partial class CustomerDAO:ICustomers
    {
        public CustomerDAO() { }

        //查询；
        public Customer GetModel(int CustomerId)
        {
            Customer ct = new Customer();
            using (DBContext dbc = new DBContext())
            {
                ct = dbc.Customers.Single(cc => cc.CustomerId == CustomerId);
            }
            return ct;
        }

        public List<Customer> GetModelList(string strWhere)
        {
            List<Customer> lcs = new List<Customer>();
            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    int Id = Convert.ToInt32(strWhere);
                    lcs = dbc.Customers.Where(cs => cs.CustomerId == Id).ToList();
                }
                else
                {
                    lcs = dbc.Customers.ToList();
                }
            }
            return lcs;
        }

        //选择查询；
        public List<Customer> GetModelList(string strWhere, string Field)
        {
            List<Customer> lcs = new List<Customer>();
            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    if (Field.Equals("CustomerName"))
                    {
                        lcs = dbc.Customers.Where(cs => cs.CustomerName.Equals(strWhere)).ToList();
                    }
                    else
                    {
                        int Id = Convert.ToInt32(strWhere);
                        lcs = dbc.Customers.Where(cs => cs.CustomerId == Id).ToList();
                    }
                }
                else
                {
                    lcs = dbc.Customers.ToList();
                }
            }
            return lcs;
        }

        //新增；
        public bool Add(Customer cr)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                dbc.Customers.Add(cr);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //修改；
        public bool Update(Customer cr)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyCustomer = dbc.Customers.Single(cc => cc.CustomerId == cr.CustomerId);

                MyCustomer.CustomerName = cr.CustomerName;
                MyCustomer.Contact = cr.Contact;
                MyCustomer.Phone = cr.Phone;
                MyCustomer.Email = cr.Email;
                MyCustomer.City = cr.City;
                MyCustomer.Address = cr.Address;
                MyCustomer.ZipCode = cr.ZipCode;

                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //删除；
        public bool Delete(int CustomerId)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyCustomer = dbc.Customers.Single(cc => cc.CustomerId == CustomerId);
                dbc.Customers.Remove(MyCustomer);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //批量删除；
        public bool DeleteList(string CustomerIdList)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                string[] IdList = CustomerIdList.Split(',');
                int Id = 0;
                for (int i = 0; i < IdList.Length; i++)
                {
                    Id = Convert.ToInt32(IdList[i]);
                    var MyCustomer = dbc.Customers.Single(cc => cc.CustomerId == Id);
                    dbc.Customers.Remove(MyCustomer);
                }
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

    }
}
