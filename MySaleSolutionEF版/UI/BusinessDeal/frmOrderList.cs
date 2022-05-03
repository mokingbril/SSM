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

namespace UI.BusinessDeal
{
    public partial class frmOrderList : Form
    {
        private OrderManage om_bll = new OrderManage();

        public frmOrderList()
        {
            InitializeComponent();
        }

        private void frmOrderList_Load(object sender, EventArgs e)
        {
            List<Order> os = om_bll.GetModelList("");
            ShowRefresh(os);
        }

        private void ShowRefresh(List<Order> ds)
        {
            dgvMessage.DataSource = null;
            dgvMessage.AutoGenerateColumns = false;
            dgvMessage.DataSource = ds;
        }

        private void dtpChoose_ValueChanged(object sender, EventArgs e)
        {
            string mint = dtpChoose.Value.ToShortDateString().Replace('/', '-') + " 00:00:00.000";
            string maxt = dtpOver.Value.ToShortDateString().Replace('/', '-') + " 23:59:59.000";

            string strsql = string.Format("{0},{1}",mint,maxt);
            List<Order> os = om_bll.GetModelList(strsql, "OrderDate");
            if (os.Count <= 0)
            {
                Tools.MInfo("订单为空！");
            }
            ShowRefresh(os);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(Tools.MYN("是否查询所有？")==DialogResult.Yes)
            {
                List<Order> os = om_bll.GetModelList("");
                ShowRefresh(os);
                return;
            }
            if(string.IsNullOrWhiteSpace(txtOrderNo.Text))
            {
                Tools.MInfo("订单编号不能为空！");
                return;
            }

            List<Order> ds = om_bll.GetModelList(txtOrderNo.Text);
            if(ds.Count<=0)
            {
                Tools.MInfo("订单为空！");
            }
            ShowRefresh(ds);
        }

        private void txtOrderNo_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtOrderNo.Text))
            {
                return;
            }
            long number = 0;
            if(!long.TryParse(txtOrderNo.Text,out number))
            {
                txtOrderNo.Text = string.Empty;
                Tools.MInfo("请输入有效的订单编号！"); 
                return;

            }
        }
    }
}
