using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleMIS.IDAL;
using SaleMIS.Model;

namespace SaleMIS.SQLServerDAL
{
    public partial class OrderDAO:IOrders
    {
        public OrderDAO() { }

        //查询；
        public Order GetModel(string OrderId) 
        {
            Order or = new Order();
            using (DBContext dbc = new DBContext())
            {
                or = dbc.Orders.Single(oo=>oo.OrderNo.Equals(OrderId));
            }
            return or;
        }

        public List<Order> GetModelList(string strWhere)
        {
            List<Order> lcs = new List<Order>();
            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    lcs = dbc.Orders.Where(cs => cs.OrderNo.Equals(strWhere)).ToList();
                }
                else
                {
                    lcs = dbc.Orders.ToList();
                }
            }
            return lcs;
        }

        //选择查询；
        public List<Order> GetModelList(string strWhere, string Field)
        {
            List<Order> lcs = new List<Order>();
            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    if (Field.Equals("CustomerId"))
                    {
                        string[] IdList = strWhere.Split(',');
                        int Id = 0;
                        for (int i = 0; i < IdList.Length; i++)
                        {
                            Id=Convert.ToInt32(IdList[i]);
                            if(dbc.Orders.Where(od => od.CustomerId == Id).ToList().Count<=0)
                            {
                                continue;
                            }
                            var MyOrder = dbc.Orders.Single(od => od.CustomerId == Id);
                            lcs.Add(MyOrder);
                        }
                    }
                    else if (Field.Equals("OrderDate"))
                    {
                        string[] DateList = strWhere.Split(',');
                        DateTime dtmin = Convert.ToDateTime(DateList[0]);
                        DateTime dtmax = Convert.ToDateTime(DateList[1]);
                        lcs = dbc.Orders.Where(or=>or.OrderDate>=dtmin && or.OrderDate<=dtmax).ToList();
                    }
                }
                else
                {
                    lcs = dbc.Orders.ToList();
                }
            }
            return lcs;
        }

        //统计查询；
        public List<Order> GetTableList()
        {
            List<Order> lcs = new List<Order>();

            using (DBContext dbc = new DBContext())
            {
                lcs = dbc.Orders.OrderBy(oo => oo.OrderDate).ToList();
            }

            return lcs;
        }

        //新增；
        public bool Add(Order or)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                dbc.Orders.Add(or);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //修改；
        public bool Update(Order or)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyOrder = dbc.Orders.Single(cc => cc.OrderNo.Equals(or.OrderNo));

                MyOrder.OrderNo = or.OrderNo;
                MyOrder.CustomerId = or.CustomerId;
                MyOrder.OrderDate = or.OrderDate;
                MyOrder.Total = or.Total;
                MyOrder.Status = or.Status;

                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //删除；
        public bool Delete(string OrderId)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyOrder = dbc.Orders.Single(cc => cc.OrderNo.Equals(OrderId));
                dbc.Orders.Remove(MyOrder);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //批量删除；
        public bool DeleteList(string OrderIdList)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                string[] IdList = OrderIdList.Split(',');
                string Id = "";
                for (int i = 0; i < IdList.Length; i++)
                {
                    Id = IdList[i].ToString();
                    var MyOrder = dbc.Orders.Single(cc => cc.OrderNo.Equals(Id));
                    dbc.Orders.Remove(MyOrder);
                }
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

    }
}
