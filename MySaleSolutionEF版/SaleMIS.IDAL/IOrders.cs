using System;
using System.Data;

using SaleMIS.Model;
using System.Collections.Generic;

namespace SaleMIS.IDAL
{
	/// <summary>
	/// 接口层Orders
	/// </summary>
	public interface IOrders
	{
		//查询；
        Order GetModel(string OrderId);

        List<Order> GetModelList(string strWhere);

        //选择查询；
        List<Order> GetModelList(string strWhere, string Field);

        //统计查询；
        List<Order> GetTableList();

        //新增；
        bool Add(Order or);

        //修改；
        bool Update(Order or);

        //删除；
        bool Delete(string OrderId);

        //批量删除；
        bool DeleteList(string OrderIdList);

	} 
}
