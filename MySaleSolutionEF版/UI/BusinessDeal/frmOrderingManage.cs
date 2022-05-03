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
    public partial class frmOrderingManage : Form
    {
        private OrderManage om_bll = new OrderManage();
        private OrderDetailManage odm_bll = new OrderDetailManage();
        private ProductManage pm_bll = new ProductManage();

        public frmOrderingManage()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (Tools.MYN("是否查询所有？") == DialogResult.Yes)
            {
                AllRefresh();
                return;
            }
            if (cboFind.SelectedItem==null)
            {
                Tools.MInfo("订单编号不能为空！请选择！");
                return;
            }

            List<OrderDetail> ds = odm_bll.GetTableList(cboFind.SelectedValue.ToString(), "OrderNo");
            if (ds.Count <= 0)
            {
                Tools.MInfo("订单为空！");
            }
            ShowRefreshMethod(ds);
        }

        private void AllRefresh()
        {
            List<OrderDetail> dn = odm_bll.GetTableList("", "");
            ShowRefreshMethod(dn);
        }

        private void cboRefresh()
        {
            List<Order> dor = om_bll.GetModelList("");
            List<Product> dp = pm_bll.GetModelList("");
            cboFind.DataSource = null;
            cboFind.Items.Clear();
            cboFind.DataSource = dor;
            cboFind.DisplayMember = "OrderNo";
            cboFind.ValueMember = "OrderNo";

            foreach (Product p in dp)
            {
                p.ProductName = p.ProductName + " " + p.Model;
            }
            cboProductId.DataSource = dp;
            cboProductId.DisplayMember = "ProductName";
            cboProductId.ValueMember = "ProductId";
        }

        private void frmOrderingManage_Load(object sender, EventArgs e)
        {
            AllRefresh();
            cboRefresh();
        }

        private void ShowRefreshMethod(List<OrderDetail> lod)
        {
            dgvMessage.DataSource = null;
            dgvMessage.AutoGenerateColumns = false;
            dgvMessage.DataSource = lod;
        }

        private void btnSeek_Click(object sender, EventArgs e)
        {
            if (Tools.MYN("是否查询所有？") == DialogResult.Yes)
            {
                AllRefresh();
                return;
            }
            if (cboProductId.SelectedItem == null)
            {
                Tools.MInfo("产品编号不能为空！请选择！");
                return;
            }

            List<OrderDetail> ds = odm_bll.GetTableList(cboProductId.SelectedValue.ToString(), "ProductId");
            if (ds.Count <= 0)
            {
                Tools.MInfo("订单为空！");
            }
            ShowRefreshMethod(ds);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvMessage.SelectedRows.Count <= 0)
            {
                Tools.MaWarn("请选择要取消的N个订单！");
                return;
            }

            //取消所属订单号的所有订单；
            if (Tools.MYN("是否注销所属订单号的所有订单？") == DialogResult.Yes)
            {
                //获取Id列表；
                string OrderId = string.Empty;
                string DetailsId = string.Empty;

                for (int i = 0; i < dgvMessage.SelectedRows.Count; i++)
                {
                    string Id = dgvMessage.SelectedRows[i].Cells[1].Value.ToString();
                    if (i == 0)
                    {
                        OrderId = Id;
                        List<OrderDetail> ds = odm_bll.GetModelList(Id, "OrderNo");
                        foreach(var dr in ds)
                        {
                            DetailsId += dr.DetailsId.ToString()+",";
                        }
                        continue;
                    }
                    List<OrderDetail> dl = odm_bll.GetModelList(Id, "OrderNo");
                    foreach (var dr in dl)
                    {
                        DetailsId += dr.DetailsId.ToString() + ",";
                    }
                    OrderId += "," + Id;                   
                }
                DetailsId = DetailsId.Substring(0,DetailsId.Length-1);

                if (Tools.MChoose("要注销的所有订单明细编号为：" + DetailsId + "\r\n需取消的所有订单号为:" + OrderId 
                    + "\r\n确定要删除上述的订单吗？") == DialogResult.Cancel) 
                {
                    Tools.MInfo("已撤销操作！删除失败！");
                    return;
                }

                if (odm_bll.DeleteList(DetailsId))
                {}
                else
                {
                    Tools.MaError("删除过程中出现异常！取消失败！");
                    return;
                }
                if (om_bll.DeleteList(OrderId))
                {
                    Tools.MaSucceed("注销成功！");
                    AllRefresh();
                }
                else
                {
                    Tools.MaError("删除过程中出现异常！取消失败！");
                    return;
                }
                return;
            }


            //删除所属订单号的所有商品；
            if (Tools.MChoose("确定要注销所属订单号的所有商品吗？") == DialogResult.OK)
            {
                //获取Id列表；
                long ID=100;
                for (int i = 0; i < dgvMessage.SelectedRows.Count; i++)
                {
                    ID = Convert.ToInt64(dgvMessage.SelectedRows[i].Cells[0].Value);
                    OrderDetail od = odm_bll.GetModel(Convert.ToInt64(ID));
                    Order ors = om_bll.GetModel(od.OrderNo);

                    decimal UnitPrice = Convert.ToDecimal(pm_bll.GetModel(Convert.ToInt32(od.ProductId)).UnitPrice);
                    ors.Total = ors.Total - (od.Quantity * od.Discount * (UnitPrice / 100));

                    //开始删除；
                    if (odm_bll.Delete(ID))
                    {
                        if (ors.Total == 0)
                        {
                            Tools.MInfo("订单号：" + od.OrderNo + "为空！\r\n此订单将被注销！");
                            om_bll.Delete(od.OrderNo);
                        }
                        else
                        {
                            if (om_bll.Update(ors))
                            {
                                Tools.MaSucceed("订单编号：" + ID + "注销商品成功！");
                            }
                        }

                    }
                    else 
                    {
                        Tools.MaError("订单编号：" + ID + "注销商品失败！");
                        return;
                    }
                }
                AllRefresh();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(dgvMessage.SelectedRows.Count!=1)
            {
                Tools.MaWarn("请选中一行【仅一行】！");
                return;
            }
            frmAddOrder fao = new frmAddOrder();
            fao.MdiParent = MdiParent;
            fao.Text = "修改订单";
            fao.type = "修改";
            fao.DetailsId = Convert.ToInt32(dgvMessage.SelectedRows[0].Cells[0].Value);
            fao.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboFind.SelectedItem == null)
            {
                Tools.MInfo("请在下拉列表中选择一项订单编号！");
            }
            else 
            {
                Order odr = om_bll.GetModel(cboFind.SelectedValue.ToString());
                if((bool)odr.Status)
                {
                    Tools.MaError("此订单已完成！请另下订单！");
                    return;
                }

                frmAddOrder fao = new frmAddOrder();
                fao.type = "添加";
                fao.Text = "添加商品";
                fao.OrderId = cboFind.SelectedValue.ToString();
                fao.MdiParent = MdiParent;
                fao.Show();
                this.Hide();
            }
        }

        private void dgvMessage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex==2 && e.Value is Order)
            {
                Order or = e.Value as Order;
                e.Value = or.CustomerId;
            }
            else if (e.ColumnIndex == 6 && e.Value is Order)
            {
                Order or = e.Value as Order;
                e.Value = or.OrderDate;
            }
            else if (e.ColumnIndex == 7 && e.Value is Order)
            {
                Order or = e.Value as Order;
                e.Value = or.Total;
            }
            else if (e.ColumnIndex == 8 && e.Value is Order)
            {
                Order or = e.Value as Order;
                e.Value = or.Status;
            }
        }

    }
}
