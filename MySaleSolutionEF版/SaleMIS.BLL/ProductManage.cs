using System;
using System.Data;
using System.Collections.Generic;

using SaleMIS.Model;
using SaleMIS.DALFactory;
using SaleMIS.IDAL;

namespace SaleMIS.BLL
{
	/// <summary>
	/// Products
	/// </summary>
	public partial class ProductManage
	{
        private readonly IProducts dal = AbstractFactory.ChooseFactory().CreateProductDAO();

		public ProductManage()
		{}

        //查询；
        public Product GetModel(int ProductId) 
        {
            return dal.GetModel(ProductId);
        }

        public List<Product> GetModelList(string strWhere) 
        {
            return dal.GetModelList(strWhere);
        }

        //检查商品唯一；
        public List<Product> GetModelList(string ProductName, string Model)
        {
            return dal.GetModelList(ProductName,Model);
        }

         //统计查询；
        public List<Product> GetTableList() 
        {
            return dal.GetTableList();
        }

        //新增；
        public bool Add(Product pt) 
        {
            return dal.Add(pt);
        }

        //修改；
        public bool Update(Product pd)
        {
            return dal.Update(pd);
        }

        //删除；
        public bool Delete(int ProductId)
        {
            return dal.Delete(ProductId);
        }

        //批量删除；
        public bool DeleteList(string ProductIdList)
        {
            return dal.DeleteList(ProductIdList);
        }

	}
}

