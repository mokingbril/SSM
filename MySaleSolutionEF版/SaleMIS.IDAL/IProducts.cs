using System;
using System.Data;

using SaleMIS.Model;
using System.Collections.Generic;

namespace SaleMIS.IDAL
{
	/// <summary>
	/// 接口层Products
	/// </summary>
	public interface IProducts
	{
		//查询；
        Product GetModel(int ProductId);

        List<Product> GetModelList(string strWhere);

        //检查商品唯一；
        List<Product> GetModelList(string ProductName, string Model);

         //统计查询；
        List<Product> GetTableList();

        //新增；
        bool Add(Product pt);

        //修改；
        bool Update(Product pd);

        //删除；
        bool Delete(int ProductId);

        //批量删除；
        bool DeleteList(string ProductIdList);

	} 
}
