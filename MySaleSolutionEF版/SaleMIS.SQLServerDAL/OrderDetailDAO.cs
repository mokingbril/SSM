using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleMIS.IDAL;
using SaleMIS.Model;

namespace SaleMIS.SQLServerDAL
{
    public partial class OrderDetailDAO:IOrderDetails
    {
        public OrderDetailDAO() { }

        //查询；
        public OrderDetail GetModel(long DetailId)
        {
            OrderDetail or = new OrderDetail();
            using (DBContext dbc = new DBContext())
            {
                or = dbc.OrderDetails.Single(od => od.DetailsId==DetailId);
            }
            return or;
        }

        public List<OrderDetail> GetModelList(string strWhere)
        {
            List<OrderDetail> lcs = new List<OrderDetail>();
            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    lcs = dbc.OrderDetails.Where(od=>od.DetailsId==Convert.ToInt64(strWhere)).ToList();
                }
                else
                {
                    lcs = dbc.OrderDetails.ToList();
                }
            }
            return lcs;
        }

        //选择查询；
        public List<OrderDetail> GetModelList(string strWhere, string Field)
        {
            List<OrderDetail> lcs = new List<OrderDetail>();
            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    if (Field.Equals("ProductId"))
                    { 
                        string[] IdList = strWhere.Split(',');
                        int Id = 0;
                        for (int i = 0; i < IdList.Length; i++) 
                        {
                            Id=Convert.ToInt32(IdList[i]);
                            if (dbc.OrderDetails.Where(od => od.ProductId == Id).ToList().Count<=0)
                            {
                                continue;
                            }
                            var MyOrderDetail = dbc.OrderDetails.Single(od => od.ProductId == Id);
                            lcs.Add(MyOrderDetail);
                        }
                    }
                    else if (Field.Equals("OrderNo"))
                    {
                        lcs = dbc.OrderDetails.Where(od=>od.OrderNo.Equals(strWhere)).ToList();
                    }
                }
                else
                {
                    lcs = dbc.OrderDetails.ToList();
                }
            }
            return lcs;
        }

        //多表查询；
        public List<OrderDetail> GetTableList(string strWhere, string Field) 
        {
            List<OrderDetail> lcs = new List<OrderDetail>();

            using (DBContext dbc = new DBContext())
            {
                if (string.IsNullOrWhiteSpace(strWhere))
                {
                    lcs = dbc.OrderDetails.ToList();
                }
                else 
                {
                    if (Field.Equals("OrderNo"))
                    {
                        lcs = dbc.OrderDetails.Where(od=>od.OrderNo.Equals(strWhere)).ToList();
                    }
                    else if (Field.Equals("DetailsId"))
                    {
                        long Id=Convert.ToInt64(strWhere);
                        lcs = dbc.OrderDetails.Where(od => od.DetailsId == Id).ToList();
                    }
                    else if (Field.Equals("ProductId"))
                    {
                        int Id=Convert.ToInt32(strWhere);
                        lcs = dbc.OrderDetails.Where(od=>od.ProductId==Id).ToList();
                    }
                }
                if (Field.Equals("CustomerId") && strWhere.Trim()!="")
                {
                    lcs = dbc.OrderDetails.ToList();
                    int Id = Convert.ToInt32(strWhere);
                    foreach (var od in lcs)
                    {
                        od.Order = dbc.Orders.Single(oo => oo.OrderNo.Equals(od.OrderNo));
                        od.Products = dbc.Products.Single(pt => pt.ProductId == od.ProductId);
                    }
                    for (int i = 0; i < lcs.Count;i++)
                    {
                        if (lcs[i].Order.CustomerId != Id)
                        {
                            lcs.RemoveAt(i);
                            i--;
                        }

                    }
                }
                else if (Field.Equals("OrderDate") && strWhere.Trim() != "")
                {
                    lcs = dbc.OrderDetails.ToList();
                    string[] MyDate = strWhere.Split(',');
                    DateTime dtmin = Convert.ToDateTime(MyDate[0]);
                    DateTime dtmax = Convert.ToDateTime(MyDate[1]);
                    foreach (var od in lcs)
                    {
                        od.Order = dbc.Orders.Single(oo => oo.OrderNo.Equals(od.OrderNo));
                        od.Products = dbc.Products.Single(pt => pt.ProductId == od.ProductId);
                    }
                    for (int i = 0; i < lcs.Count; i++)
                    {
                        if (lcs[i].Order.OrderDate < dtmin || lcs[i].Order.OrderDate > dtmax)
                        {
                            lcs.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else 
                {
                    foreach (var od in lcs)
                    {
                        od.Order = dbc.Orders.Single(oo => oo.OrderNo.Equals(od.OrderNo));
                        od.Products = dbc.Products.Single(pt => pt.ProductId == od.ProductId);
                    }
                }

            }

            return lcs;
        }

        //分类查询；
        public List<OrderDetail> GetOrberByList(string Field) 
        {
            List<OrderDetail> lcs = new List<OrderDetail>();

            using (DBContext dbc = new DBContext())
            {
                if (Field.Equals("ProductId"))
                {
                    lcs = dbc.OrderDetails.OrderBy(od => od.ProductId).ToList();
                }
                else if (Field.Equals("OrderNo"))
                {
                    lcs = dbc.OrderDetails.OrderBy(od => od.OrderNo).ToList();
                }
                else 
                {
                    lcs = dbc.OrderDetails.OrderBy(od=>od.DetailsId).ToList();
                }
            }
            return lcs;
        }

        //新增；
        public bool Add(OrderDetail od)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                dbc.OrderDetails.Add(od);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //修改；
        public bool Update(OrderDetail od)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyOrderDetail = dbc.OrderDetails.Single(ol=>ol.DetailsId==od.DetailsId);

                MyOrderDetail.OrderNo = od.OrderNo;
                MyOrderDetail.ProductId = od.ProductId;
                MyOrderDetail.Quantity = od.Quantity;
                MyOrderDetail.Discount = od.Discount;

                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //删除；
        public bool Delete(long OrderDetailId)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyOrderDetail = dbc.OrderDetails.Single(cc => cc.DetailsId == OrderDetailId);
                dbc.OrderDetails.Remove(MyOrderDetail);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //批量删除；
        public bool DeleteList(string DetailIdList)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                string[] IdList = DetailIdList.Split(',');
                long Id = 0;
                for (int i = 0; i < IdList.Length; i++)
                {
                    Id = Convert.ToInt64(IdList[i]);
                    var MyOrderDetail = dbc.OrderDetails.Single(cc => cc.DetailsId == Id);
                    dbc.OrderDetails.Remove(MyOrderDetail);
                }
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

    }
}
