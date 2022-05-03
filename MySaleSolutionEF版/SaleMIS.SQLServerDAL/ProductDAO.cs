using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleMIS.IDAL;
using SaleMIS.Model;

namespace SaleMIS.SQLServerDAL
{
    public partial class ProductDAO:IProducts
    {
        public ProductDAO() { }

        //查询；
        public Product GetModel(int ProductId)
        {
            Product pt = new Product();
            using (DBContext dbc = new DBContext())
            {
                pt = dbc.Products.Single(p=>p.ProductId==ProductId);
            }
            return pt;
        }

        public List<Product> GetModelList(string strWhere)
        {
            List<Product> lcs = new List<Product>();
            using (DBContext dbc = new DBContext())
            {
                if (strWhere.Trim() != "")
                {
                    int Id = Convert.ToInt32(strWhere);
                    lcs = dbc.Products.Where(cs => cs.ProductId==Id).ToList();
                }
                else
                {
                    lcs = dbc.Products.ToList();
                }
            }
            return lcs;
        }

        //检查商品唯一；
        public List<Product> GetModelList(string ProductName, string Model)
        {
            List<Product> lcs = new List<Product>();
            using (DBContext dbc = new DBContext())
            {
                lcs = dbc.Products.Where(pt => pt.ProductName.Equals(ProductName) && pt.Model.Equals(Model)).ToList();
            }
            return lcs;
        }

        //统计查询；
        public List<Product> GetTableList()
        {
            List<Product> lcs = new List<Product>();
            List<OrderDetail> lod = new List<OrderDetail>();

            using (DBContext dbc = new DBContext())
            {
                lcs = dbc.Products.ToList();

                foreach (var item in lcs)
                {
                    lod = dbc.OrderDetails.Where(od=>od.ProductId==item.ProductId).ToList();
                    if(lod.Count<=0)
                    {
                        continue;
                    }
                    item.ProductName += "," + item.Model;
                    item.Model = item.UnitPrice.ToString();
                    item.UnitPrice = 0;
                    item.Quantity = 0;
                    foreach (var od in lod)
                    {
                        item.Quantity += od.Quantity;
                        item.UnitPrice += (od.Quantity * od.Discount * Convert.ToDecimal(item.Model) / 100);
                    }
                    
                }
            }

            return lcs;
        }

        //新增；
        public bool Add(Product pt)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                dbc.Products.Add(pt);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //修改；
        public bool Update(Product pd)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyProduct = dbc.Products.Single(pt => pt.ProductId == pd.ProductId);

                MyProduct.ProductName = pd.ProductName;
                MyProduct.Model = pd.Model;
                MyProduct.UnitPrice = pd.UnitPrice;
                MyProduct.Quantity = pd.Quantity;

                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //删除；
        public bool Delete(int ProductId)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                var MyProduct = dbc.Products.Single(pp => pp.ProductId==ProductId);
                dbc.Products.Remove(MyProduct);
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        //批量删除；
        public bool DeleteList(string ProductIdList)
        {
            bool result = false;
            using (DBContext dbc = new DBContext())
            {
                string[] IdList = ProductIdList.Split(',');
                int Id = 0;
                for (int i = 0; i < IdList.Length; i++)
                {
                    Id = Convert.ToInt32(IdList[i]);
                    var MyProduct = dbc.Products.Single(pp => pp.ProductId == Id);
                    dbc.Products.Remove(MyProduct);
                }
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

    }
}
