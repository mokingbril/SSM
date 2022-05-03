using System;
using System.Data;

using SaleMIS.Model;
using System.Collections.Generic;

namespace SaleMIS.IDAL
{
	/// <summary>
	/// 接口层OrderDetails
	/// </summary>
	public interface IOrderDetails
	{
		//查询；
        OrderDetail GetModel(long DetailId);

        List<OrderDetail> GetModelList(string strWhere);

        //分类查询；
        List<OrderDetail> GetOrberByList(string Field);

        //选择查询；
        List<OrderDetail> GetModelList(string strWhere, string Field);

        //多表查询；
        List<OrderDetail> GetTableList(string strWhere, string Field);

        //新增；
        bool Add(OrderDetail od);

        //修改；
        bool Update(OrderDetail od);

        //删除；
        bool Delete(long OrderDetailId);

        //批量删除；
        bool DeleteList(string DetailIdList);

	} 
}
