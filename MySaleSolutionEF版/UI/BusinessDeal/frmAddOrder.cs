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
    public partial class frmAddOrder : Form
    {

        private ProductManage pm_bll=new ProductManage();
        private CustomerManage cm_bll = new CustomerManage();
        private OrderManage om_bll = new OrderManage();
        private OrderDetailManage odm_bll = new OrderDetailManage();

        private List<Order> orders = new List<Order>();

        public string type = string.Empty;
        public int DetailsId { get; set; }
        public string OrderId { get; set; }

        public frmAddOrder()
        {
            InitializeComponent();
        }

        //关闭窗口；
        private void btnCannel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //加载窗口；
        private void frmAddOrder_Load(object sender, EventArgs e)
        {
            ShowRefresh();
            UpdateLoad();
            AddLoad();
        }

        private void AddLoad()
        {
            if (type.Equals("添加"))
            {
                cboId.Text = txtId.Text = OrderId;
                cboCustomerId.Enabled = false;
                dtpOrderDate.Enabled = false;
                Order odr = om_bll.GetModel(OrderId);
                cboCustomerId.SelectedValue = odr.CustomerId;
                dtpOrderDate.Value = Convert.ToDateTime(odr.OrderDate);
                txtTotal.Text = (odr.Total).ToString();
            }
        }

        private void UpdateLoad()
        {
            if (type.Equals("修改"))
            {
                rdoYes.Enabled = true;
                rdoNo.Enabled = true;
                List<OrderDetail> ds = odm_bll.GetTableList(DetailsId.ToString(), "DetailsId");
                txtDetailId.Text = ds[0].DetailsId.ToString();
                cboId.Text = txtId.Text = ds[0].OrderNo;
                cboProductId.SelectedValue = ds[0].ProductId;
                nudOrderCount.Value = Convert.ToDecimal(ds[0].Quantity);
                nudDiscount.Value = Convert.ToDecimal(ds[0].Discount);
                cboCustomerId.Text = ds[0].Order.CustomerId.ToString();
                dtpOrderDate.Value = Convert.ToDateTime(ds[0].Order.OrderDate);
                txtTotal.Text = ds[0].Order.Total.ToString();
                if (Convert.ToBoolean(ds[0].Order.Status))
                {
                    rdoYes.Checked = true;
                }
                else
                {
                    rdoNo.Checked = true;
                }

            }
        }

        //刷新；
        private void ShowRefresh()
        {
            //清理；
            cboProductId.DataSource = null;
            cboCustomerId.DataSource = null;
            cboCustomerId.Items.Clear();
            cboProductId.Items.Clear();

            //绑定；
            List<Customer> dsc = cm_bll.GetModelList("");
            List<Product> dsp = pm_bll.GetModelList("");


            cboCustomerId.DataSource = dsc;
            cboCustomerId.DisplayMember = "CustomerId";
            cboCustomerId.ValueMember = "CustomerId";

            foreach (Product p in dsp)
            {

                p.ProductName = p.ProductName + " " + p.Model;
            }
            cboProductId.DataSource = dsp;
            cboProductId.DisplayMember = "ProductName";
            cboProductId.ValueMember = "ProductId";

            //刷新日期；
            dtpOrderDate.MinDate = DateTime.Now;
        }

        //下订单；
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(type.Equals("添加"))
            {
                AddMethod();
                return;
            }
            if(cboCustomerId.SelectedItem ==null || cboProductId.SelectedItem == null)
            {
                Tools.MaWarn("编号[客户&&产品]必选！");
                return;
            }

            //获取订单总额；
            decimal UnitPrice = Convert.ToDecimal(pm_bll.GetModel(Convert.ToInt32(cboProductId.SelectedValue)).UnitPrice);
            txtTotal.Text = (nudOrderCount.Value * UnitPrice * (nudDiscount.Value / 100)).ToString();
            txtUnitPrice.Text = UnitPrice.ToString();

            if(type.Equals("修改"))
            {
                btnUpdate();
                return;
            }

            //查询订单序号；
            orders = om_bll.GetModelList("");
            int max = 0;
            if (orders.Count <= 0)
            {
                max = 1;
            }
            else 
            {
                string strOrno = string.Empty;
                foreach (Order ors in orders)
                {
                    strOrno = ors.OrderNo.Substring(8, ors.OrderNo.Length - 8);
                    if (max < Convert.ToInt32(strOrno))
                    {
                        max = Convert.ToInt32(strOrno);
                    }
                }
                max++;
            }

            string Nums = max.ToString();

            if(max < 1000)
            {
                if(max<10)
                {
                    Nums = "00" + max.ToString();
                }
                else if(max <100)
                {
                    Nums = "0" + max.ToString();
                }
            }

            //获取时间；
            string date = DateTime.Now.ToShortDateString().Replace('/', '0');
            string dateA = date.Substring(0,6);
            string dateB = date.Substring(7,date.Length-7);
            date = dateA + dateB;

            //获取订单总额；
            MessageBox.Show("订单总额："+txtTotal.Text);
            if(Tools.MChoose("确定下订单吗？")==DialogResult.Cancel)
            {
                return;
            }

            //开始下订单；
            Order order = new Order();
            order.CustomerId = Convert.ToInt32(cboCustomerId.SelectedValue);
            //order.OrderDate = dtpOrderDate.Value;
            order.OrderDate = DateTime.Now;
            order.Total = Convert.ToDecimal(txtTotal.Text);
            order.Status = false;
            order.OrderNo = date + Nums;

            if (om_bll.Add(order))
            {
                Tools.MaSucceed("新增订单成功√");
                txtId.Text = order.OrderNo;
            }
            else 
            {
                Tools.MaError("出现异常！新增订单失败！");
                return;
            }


            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderNo = order.OrderNo;
            orderDetail.ProductId = Convert.ToInt32(cboProductId.SelectedValue);
            orderDetail.Quantity = Convert.ToInt32(nudOrderCount.Value);
            orderDetail.Discount = Convert.ToInt32(nudDiscount.Value);

            if (odm_bll.Add(orderDetail))
            {
                Tools.MaSucceed("新增订单明细成功√");
                cboId.Text = orderDetail.OrderNo;
                //txtDetailId.Text = DetailId.ToString();
            }
            else
            {
                Tools.MaError("出现异常！新增订单明细失败！");
                return;
            }

        }

        //关于修改的方法；
        private void btnUpdate() 
        {
            //修改订单；
            Order order = new Order();
            order.CustomerId = Convert.ToInt32(cboCustomerId.SelectedValue);
            order.OrderDate = DateTime.Now;
            order.Total = Convert.ToDecimal(txtTotal.Text);
            order.Status = false;
            if(rdoYes.Checked)
            {
                order.Status = true;
            }
            order.OrderNo = txtId.Text;
            if (om_bll.Update(order))
            {
                Tools.MaSucceed("修改订单成功√");
            }
            else
            {
                Tools.MaError("出现异常！修改订单失败！");
                return;
            }

            OrderDetail orderDetail = new OrderDetail();
            orderDetail.DetailsId = Convert.ToInt32(txtDetailId.Text);
            orderDetail.OrderNo = cboId.Text;
            orderDetail.ProductId = Convert.ToInt32(cboProductId.SelectedValue);
            orderDetail.Quantity = Convert.ToInt32(nudOrderCount.Value);
            orderDetail.Discount = Convert.ToInt32(nudDiscount.Value);

            if (odm_bll.Update(orderDetail))
            {
                Tools.MaSucceed("修改订单明细成功√");
                this.Close();
            }
            else
            {
                Tools.MaError("出现异常！修改订单明细失败！");
                return;
            }
        }

        //关于添加的方法；
        private void AddMethod() 
        {
            if (cboProductId.SelectedItem == null)
            {
                Tools.MaWarn("产品编号必选！");
                return;
            }

            //获取订单总额；
            Order odr = om_bll.GetModel(OrderId);
            decimal UnitPrice = Convert.ToDecimal(pm_bll.GetModel(Convert.ToInt32(cboProductId.SelectedValue)).UnitPrice);
            txtUnitPrice.Text = UnitPrice.ToString();
            txtTotal.Text = (nudOrderCount.Value * UnitPrice * (nudDiscount.Value / 100) + Convert.ToDecimal(odr.Total)).ToString();

            //提醒；
            MessageBox.Show("订单总额：" + txtTotal.Text);
            if (Tools.MChoose("确定下订单吗？") == DialogResult.Cancel)
            {
                return;
            }

            //开始添加；
            Order order = new Order();
            order.OrderNo = txtId.Text;
            order.CustomerId = Convert.ToInt32(cboCustomerId.SelectedValue);
            order.OrderDate = DateTime.Now;
            order.Total = Convert.ToDecimal(txtTotal.Text);
            order.Status = false;

            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderNo = cboId.Text;
            orderDetail.ProductId = Convert.ToInt32(cboProductId.SelectedValue);
            orderDetail.Quantity = Convert.ToInt32(nudOrderCount.Value);
            orderDetail.Discount = Convert.ToInt32(nudDiscount.Value);

            if (odm_bll.Add(orderDetail))
            {
                Tools.MaSucceed("新增订单明细成功√");
                //txtDetailId.Text = DetailId.ToString();
            }
            else
            {
                Tools.MaError("出现异常！新增订单明细失败！");
                return;
            }
            if (om_bll.Update(order))
            {
                Tools.MaSucceed("修改订单成功√");
                this.Close();
            }
            else
            {
                Tools.MaError("出现异常！修改订单失败！");
                return;
            }

        }

    }
}
