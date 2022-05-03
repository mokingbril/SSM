using System;
using System.Data;
using System.Collections.Generic;

using SaleMIS.Model;
using SaleMIS.DALFactory;
using SaleMIS.IDAL;


namespace SaleMIS.BLL
{
	/// <summary>
	/// Customers
	/// </summary>
	public partial class CustomerManage
	{
		private readonly ICustomers dal=AbstractFactory.ChooseFactory().CreateCustomerDAO();

		public CustomerManage()
		{}

        //查询；
        public Customer GetModel(int CustomerId) 
        {
            return dal.GetModel(CustomerId);
        }

        public List<Customer> GetModelList(string strWhere)
        {
            return dal.GetModelList(strWhere);
        }

        //选择查询；
        public List<Customer> GetModelList(string strWhere, string Field)
        {
            return dal.GetModelList(strWhere,Field);
        }

        //新增；
        public bool Add(Customer cr) 
        {
            return dal.Add(cr);
        }

        //修改；
        public bool Update(Customer cr)
        {
            return dal.Update(cr);
        }

        //删除；
        public bool Delete(int CustomerId)
        {
            return dal.Delete(CustomerId);
        }

        //批量删除；
        public bool DeleteList(string CustomerIdList)
        {
            return dal.DeleteList(CustomerIdList);
        }
	}
}

