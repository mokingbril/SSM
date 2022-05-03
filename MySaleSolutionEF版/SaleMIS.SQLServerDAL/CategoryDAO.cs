using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleMIS.IDAL;
using SaleMIS.Model;

namespace SaleMIS.SQLServerDAL
{
    public partial class CategoryDAO:ICategories
    {
        public CategoryDAO() { }

        //查询；
        public Category GetModel(int CategoryId)
        {
            Category ca = new Category();
            using (DBContext dbc = new DBContext())
            {
                ca = dbc.Categories.Single(cc=>cc.CategoryId==CategoryId);
            }
            return ca;
        }

        public List<Category> GetModelList(string strWhere) 
        {
            List<Category> lcs = new List<Category>();
            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    int Id = Convert.ToInt32(strWhere);
                    lcs = dbc.Categories.Where(cs => cs.CategoryId == Id).ToList();
                }
                else
                {
                    lcs = dbc.Categories.ToList();
                }
            }
            return lcs;
        }

        //选择查询；
        public List<Category> GetModelList(string strWhere, string Field)
        {
            List<Category> lcs = new List<Category>();

            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    if (Field.Equals("CategoryName"))
                    {
                        lcs = dbc.Categories.Where(cc=>cc.CategoryName.Equals(strWhere)).ToList();
                    }
                }
                else
                {
                    lcs = dbc.Categories.ToList();
                }
            }
            return lcs;
        }

        //新增；
        public bool Add(Category ca) 
        {
            bool result = false;
            using(DBContext dbc=new DBContext())
            {
                dbc.Categories.Add(ca);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //修改；
        public bool Update(Category cy) 
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyCategory=dbc.Categories.Single(cc=>cc.CategoryId==cy.CategoryId);
                MyCategory.CategoryName = cy.CategoryName;
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //删除；
        public bool Delete(int CategoryId) 
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyCategory = dbc.Categories.Single(cc => cc.CategoryId == CategoryId);
                dbc.Categories.Remove(MyCategory);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //批量删除；
        public bool DeleteList(string CategoryIdList) 
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                string[] IdList = CategoryIdList.Split(',');
                int Id = 0;
                for (int i = 0; i < IdList.Length;i++ )
                {
                    Id = Convert.ToInt32(IdList[i]);
                    var MyCategory = dbc.Categories.Single(cc => cc.CategoryId == Id);
                    dbc.Categories.Remove(MyCategory);
                }
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

    }
}
