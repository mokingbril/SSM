using System;
using System.Data;
using System.Collections.Generic;

using SaleMIS.Model;
using SaleMIS.DALFactory;
using SaleMIS.IDAL;

namespace SaleMIS.BLL
{
	/// <summary>
	/// Orders
	/// </summary>
	public partial class OrderManage
	{
        private readonly IOrders dal = AbstractFactory.ChooseFactory().CreateOrderDAO();

		public OrderManage()
		{}
        //查询；
        public Order GetModel(string OrderId) 
        {
            return dal.GetModel(OrderId);
        }

        public List<Order> GetModelList(string strWhere) 
        {
            return dal.GetModelList(strWhere);
        }

        //选择查询；
        public List<Order> GetModelList(string strWhere, string Field)
        {
            return dal.GetModelList(strWhere,Field);
        }

        //统计查询；
        public List<Order> GetTableList() 
        {
            return dal.GetTableList();
        }

        //新增；
        public bool Add(Order or) 
        {
            return dal.Add(or);
        }

        //修改；
        public bool Update(Order or)
        {
            return dal.Update(or);
        }

        //删除；
        public bool Delete(string OrderId)
        {
            return dal.Delete(OrderId);
        }

        //批量删除；
        public bool DeleteList(string OrderIdList)
        {
            return dal.DeleteList(OrderIdList);
        }

	}
}

