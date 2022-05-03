using System;
using System.Data;
using System.Collections.Generic;

using SaleMIS.Model;
using SaleMIS.DALFactory;
using SaleMIS.IDAL;

namespace SaleMIS.BLL
{
	/// <summary>
	/// OrderDetails
	/// </summary>
	public partial class OrderDetailManage
	{
        private readonly IOrderDetails dal = AbstractFactory.ChooseFactory().CreateOrderDetailDAO();

		public OrderDetailManage()
		{}

        //查询；
        public OrderDetail GetModel(long DetailId) 
        {
            return dal.GetModel(DetailId);
        }

        public List<OrderDetail> GetModelList(string strWhere) 
        {
            return dal.GetModelList(strWhere);
        }

        //选择查询；
        public List<OrderDetail> GetModelList(string strWhere, string Field)
        {
            return dal.GetModelList(strWhere,Field);
        }

        //多表查询；
        public List<OrderDetail> GetTableList(string strWhere, string Field) 
        {
            return dal.GetTableList(strWhere, Field);
        }

        //分类查询；
        public List<OrderDetail> GetOrberByList(string Field) 
        {
            return dal.GetOrberByList(Field);
        }

        //新增；
        public bool Add(OrderDetail od) 
        {
            return dal.Add(od);
        }

        //修改；
        public bool Update(OrderDetail od)
        {
            return dal.Update(od);
        }

        //删除；
        public bool Delete(long OrderDetailId)
        {
            return dal.Delete(OrderDetailId);
        }

        //批量删除；
        public bool DeleteList(string DetailIdList)
        {
            return dal.DeleteList(DetailIdList);
        }

	}
}

