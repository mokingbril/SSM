using System;
using System.Data;
using System.Collections.Generic;

using SaleMIS.Model;
using SaleMIS.DALFactory;
using SaleMIS.IDAL;


namespace SaleMIS.BLL
{
	/// <summary>
	/// Categories
	/// </summary>
	public partial class CategoryManage
	{
        private  ICategories dal = AbstractFactory.ChooseFactory().CreateCategoryDAO();

		public CategoryManage()
		{}

        //查询；
        public Category GetModel(int CategoryId) 
        {
            return dal.GetModel(CategoryId);
        }

        public List<Category> GetAllModelList() 
        {
            return dal.GetModelList("");
        }

        public List<Category> GetModelList(string strWhere) 
        {
            return dal.GetModelList(strWhere);
        }

        //选择查询；
        public List<Category> GetModelList(string strWhere, string Field) 
        {
            return dal.GetModelList(strWhere,Field);
        }

        //新增；
        public bool Add(Category ca) 
        {
            return dal.Add(ca);
        }

        //修改；
        public bool Update(Category cy) 
        {
            return dal.Update(cy);
        }

        //删除；
        public bool Delete(int CategoryId)
        {
            return dal.Delete(CategoryId);
        }

        //批量删除；
        public bool DeleteList(string CategoryIdList)
        {
            return dal.DeleteList(CategoryIdList);
        }

	}
}

