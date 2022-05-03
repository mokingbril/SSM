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

namespace UI.JournalingSystem
{
    public partial class frmProductsSortStatistics : Form
    {
        private OrderDetailManage odm_bll = new OrderDetailManage();
        private ProductManage pm_bll = new ProductManage();

        public frmProductsSortStatistics()
        {
            InitializeComponent();
        }

        private void frmProductsSortStatistics_Load(object sender, EventArgs e)
        {
            dgvMessage.DataSource = null;
            dgvMessage.AutoGenerateColumns = false;
            //string strfield = "select OrderDetails.ProductId,ProductName,Model,''as '合计',OrderDetails.Quantity,Discount,UnitPrice,"
            //                  + "(OrderDetails.Quantity*Discount*UnitPrice/100)as'总额' from OrderDetails "
            //                  +"right join Products on Products.ProductId=OrderDetails.ProductId ";

            //string strwhere = " union select Products.ProductId,ProductName,model,'合计',SUM(OrderDetails.Quantity),'-1','-1'"
            //                  +",SUM((OrderDetails.Quantity*Discount*UnitPrice/100))"
            //                  +" from Products left join OrderDetails on Products.ProductId=OrderDetails.ProductId "
            //                  +"group by Products.ProductId,ProductName,model";

            List<OrderDetail> dn = odm_bll.GetOrberByList("ProductId");
            List<Product> ds = pm_bll.GetTableList();
            dgvMessage.DataSource = ds;
        }

        private void dgvMessage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.Value==null)
            {
                e.Value = "合计";
            }
            else if (e.ColumnIndex == 1)
            {
                e.Value = e.Value.ToString().Split(',')[0];
            }
            else if (e.ColumnIndex == 2) 
            {
                e.Value = e.Value.ToString().Split(',')[1];
            }
        }

    }
}
