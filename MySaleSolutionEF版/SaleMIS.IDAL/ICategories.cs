using System;
using System.Data;

using SaleMIS.Model;
using System.Collections.Generic;

namespace SaleMIS.IDAL
{
	public interface ICategories
	{
		//查询；
        Category GetModel(int CategoryId);

        List<Category> GetModelList(string strWhere);

        //选择查询；
        List<Category> GetModelList(string strWhere, string Field);

        //新增；
        bool Add(Category ca);

        //修改；
        bool Update(Category cy);
        
        //删除；
        bool Delete(int CategoryId);
        
        //批量删除；
        bool DeleteList(string CategoryIdList);

	} 
}
