using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SaleMIS.BLL;
using SaleMIS.Model;

namespace UI.MonthSaleAmount
{
    public partial class frmEachCategoryMoney : Form
    {
        private ProductManage pm_bll = new ProductManage();
        private OrderManage om_bll = new OrderManage();
        private OrderDetailManage odm = new OrderDetailManage();

        public frmEachCategoryMoney()
        {
            InitializeComponent();
        }

        private void frmEachCategoryMoney_Load(object sender, EventArgs e)
        {
            //产品项加载
            List<Product> ps = pm_bll.GetModelList("");
            cboProducts.DataSource = null;
            cboProducts.Items.Clear();
            foreach(Product p in ps)
            {
                p.ProductName = p.ProductName + " " + p.Model;
            }
            cboProducts.DataSource = ps;
            cboProducts.DisplayMember = "ProductName";
            cboProducts.ValueMember = "ProductId";

            //dgv加载；
            dgvRefrish("","");
        }

        private void dgvRefrish(string strWhere, string Field)
        {
            //string strfield = "DetailsId,OrderDetails.OrderNo,OrderDetails.ProductId,ProductName,model,"+
            //                  "(Discount*(OrderDetails.Quantity)*(UnitPrice/100))as 'PTotal',"
            //                  +"UnitPrice,CustomerId,Discount,OrderDetails.Quantity,OrderDate";
            //string strsql = "left join Orders on Orders.OrderNo=OrderDetails.OrderNo"
            //                + " left join Products on Products.ProductId=OrderDetails.ProductId ";

            List<OrderDetail> ds = odm.GetTableList(strWhere,Field);
            foreach(var od in ds)
            {
                od.OrderNo = (od.Discount * od.Quantity * od.Products.UnitPrice / 100).ToString();
            }
            dgvMessage.DataSource = null;
            dgvMessage.AutoGenerateColumns = false;
            dgvMessage.DataSource = ds;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (cboProducts.SelectedItem == null)
            {
                Tools.MInfo("请选择一项产品类型！");
                return;
            }
            else 
            {
                dgvRefrish(cboProducts.SelectedValue.ToString(),"ProductId");
            }

        }

        private void dtpBegin_ValueChanged(object sender, EventArgs e)
        {
            string mint = dtpBegin.Value.ToShortDateString() + " 00:00:00.000";
            string maxt = dtpEnd.Value.ToShortDateString() + " 23:59:59.000";
            if(Convert.ToDateTime(mint)>Convert.ToDateTime(maxt))
            {
                Tools.MaError("订单为空！");
                return;
            }
            string strsql = string.Format("{0},{1}", mint, maxt);
            dgvRefrish(strsql, "OrderDate");
        }

        private void dgvMessage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex==3&&e.Value is Order)
            {
                Order oo = e.Value as Order;
                e.Value = oo.CustomerId;
            }
            else if(e.ColumnIndex==4 && e.Value is Product)
            {
                Product pd = e.Value as Product;
                e.Value = pd.ProductName;
            }
            else if (e.ColumnIndex == 5 && e.Value is Product)
            {
                Product pd = e.Value as Product;
                e.Value = pd.Model;
            }
            else if (e.ColumnIndex == 6 && e.Value is Order)
            {
                Order oo = e.Value as Order;
                e.Value = oo.OrderDate;
            }
            else if (e.ColumnIndex == 9 && e.Value is Product)
            {
                Product pd = e.Value as Product;
                e.Value = pd.UnitPrice;
            }
            else if (e.ColumnIndex == 1 && e.Value is Order)
            {
                Order oo = e.Value as Order;
                e.Value = oo.OrderNo;
            }
        }

    }
}
