using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        
        //退出系统；
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //基本数据管理_产品类型管理；
        private void mnuCategoryManage_Click(object sender, EventArgs e)
        {
            BasicManage.frmCategories fc = new BasicManage.frmCategories();
            fc.MdiParent = this;
            fc.Show();
        }

        //基本数据管理_产品数据管理；
        private void mnuProductManage_Click(object sender, EventArgs e)
        {
            BasicManage.frmProduct fp = new BasicManage.frmProduct();
            fp.MdiParent = this;
            fp.Show();
        }

        //基本数据管理_客户数据管理；
        private void mnuCustomerManage_Click(object sender, EventArgs e)
        {
            BasicManage.frmCustomer fcr = new BasicManage.frmCustomer();
            fcr.MdiParent = this;
            fcr.Show();
        }



        //业务处理_下订单；
        private void mnuAddOrder_Click(object sender, EventArgs e)
        {
            BusinessDeal.frmAddOrder fao = new BusinessDeal.frmAddOrder();
            fao.MdiParent = this;
            fao.Show();
        }

        //业务处理_订单过程管理；
        private void mnuOrderingManage_Click(object sender, EventArgs e)
        {
            BusinessDeal.frmOrderingManage fom = new BusinessDeal.frmOrderingManage();
            fom.MdiParent = this;
            fom.Show();
        }

        //业务处理_订单列表；
        private void mnuOrderList_Click(object sender, EventArgs e)
        {
            BusinessDeal.frmOrderList fol = new BusinessDeal.frmOrderList();
            fol.MdiParent = this;
            fol.Show();
        }



        //报表系统_月销售报表；
        private void mnuMonthSaleJournal_Click(object sender, EventArgs e)
        {
            JournalingSystem.frmMonthSaleJournal fmsj = new JournalingSystem.frmMonthSaleJournal();
            fmsj.MdiParent = this;
            fmsj.Show();
        }

        //报表系统_产品销售分类统计；
        private void mnuProductsSortStatistics_Click(object sender, EventArgs e)
        {
            JournalingSystem.frmProductsSortStatistics fpss = new JournalingSystem.frmProductsSortStatistics();
            fpss.MdiParent = this;
            fpss.Show();
        }



        //月销售总额_每种产品类型的订购额；
        private void mnuEachCategoryMoney_Click(object sender, EventArgs e)
        {
            MonthSaleAmount.frmEachCategoryMoney fecm = new MonthSaleAmount.frmEachCategoryMoney();
            fecm.MdiParent = this;
            fecm.Show();
        }

        //月销售总额_每个客户的订购额；
        private void mnuCustomerMoney_Click(object sender, EventArgs e)
        {
            MonthSaleAmount.frmEachCustomerMoney fcm = new MonthSaleAmount.frmEachCustomerMoney();
            fcm.MdiParent = this;
            fcm.Show();
        }

    }
}
