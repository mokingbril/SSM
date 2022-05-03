using System;
using System.Data;

using SaleMIS.Model;
using System.Collections.Generic;

namespace SaleMIS.IDAL
{
	/// <summary>
	/// 接口层Customers
	/// </summary>
	public interface ICustomers
	{
        //查询；
        Customer GetModel(int CustomerId);

        List<Customer> GetModelList(string strWhere);
        
        //选择查询；
        List<Customer> GetModelList(string strWhere, string Field);

        //新增；
        bool Add(Customer cr);

        //修改；
        bool Update(Customer cr);

        //删除；
        bool Delete(int CustomerId);

        //批量删除；
        bool DeleteList(string CustomerIdList);

	} 
}
