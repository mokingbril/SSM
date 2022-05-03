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
    public partial class frmEachCustomerMoney : Form
    {
        private OrderDetailManage odm = new OrderDetailManage();
        private CustomerManage cm_bll = new CustomerManage();


        public frmEachCustomerMoney()
        {
            InitializeComponent();
        }

        private void dgvRefrish(string strWhere, string Field)
        {
            //string strfield = "DetailsId,OrderDetails.OrderNo,OrderDetails.ProductId,ProductName,model," +
            //                  "(Discount*(OrderDetails.Quantity)*(UnitPrice/100))as 'PTotal',"
            //                  + "UnitPrice,CustomerId,Discount,OrderDetails.Quantity,OrderDate";
            //string strsql = "left join Orders on Orders.OrderNo=OrderDetails.OrderNo"
            //                + " left join Products on Products.ProductId=OrderDetails.ProductId ";
            List<OrderDetail> ds = odm.GetTableList(strWhere, Field);
            foreach (var od in ds)
            {
                od.OrderNo = (od.Discount * od.Quantity * od.Products.UnitPrice / 100).ToString();
            }
            dgvMessage.DataSource = null;
            dgvMessage.AutoGenerateColumns = false;
            dgvMessage.DataSource = ds;
        }

        private void frmEachCustomerMoney_Load(object sender, EventArgs e)
        {
            //产品项加载
            List<Customer> cs = cm_bll.GetModelList("");
            cboCustomers.DataSource = null;
            cboCustomers.Items.Clear();
            foreach (Customer c in cs)
            {
                c.CustomerName = c.CustomerId + " " + c.CustomerName;
            }
            cboCustomers.DataSource = cs;
            cboCustomers.DisplayMember = "CustomerName";
            cboCustomers.ValueMember = "CustomerId";

            //dgv加载；
            dgvRefrish("","");
        }

        private void btnSeek_Click(object sender, EventArgs e)
        {
            if (cboCustomers.SelectedItem == null)
            {
                Tools.MInfo("请选择一项客户编号！");
                return;
            }
            else
            {
                dgvRefrish(cboCustomers.SelectedValue.ToString(), "CustomerId");
            }
        }

        private void dgvMessage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.Value is Order)
            {
                Order oo = e.Value as Order;
                e.Value = oo.CustomerId;
            }
            else if (e.ColumnIndex == 4 && e.Value is Product)
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
            else if (e.ColumnIndex == 7 && e.Value is Product)
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
