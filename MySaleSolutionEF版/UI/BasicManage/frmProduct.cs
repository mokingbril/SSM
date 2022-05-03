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

namespace UI.BasicManage
{
    public partial class frmProduct : Form
    {
        private ProductManage pm_bll = new ProductManage();

        private OrderDetailManage odm_bll = new OrderDetailManage();

        List<Product> ps = new List<Product>();

        public frmProduct()
        {
            InitializeComponent();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            ShowRefresh();
        }

        private void ShowRefresh()
        {
            //清理；
            dgvMessage.DataSource = null;
            cboProducts.DataSource = null;
            cboProducts.Items.Clear();

            //获取数据绑定控件；
            List<Product> ps = pm_bll.GetModelList("");
            dgvMessage.AutoGenerateColumns = false;
            dgvMessage.DataSource = ps;

            List<Product> das = pm_bll.GetModelList("");
            foreach (var p in das)
            {
                p.ProductName = p.ProductName + " " + p.Model;
            }
            cboProducts.DataSource = das;
            cboProducts.DisplayMember = "ProductName";

            cboProducts.ValueMember = "ProductId";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DialogResult jg = Tools.MYNC("是否查询所有项？");
            if (jg == DialogResult.Yes) 
            {
                ShowRefresh();
                return;
            }
            else if (jg == DialogResult.Cancel)
            {
                return;
            }
            if (cboProducts.SelectedItem != null)
            {
                ps = pm_bll.GetModelList(cboProducts.SelectedValue.ToString());
                dgvMessage.DataSource = null;
                dgvMessage.DataSource = ps;
            }
            else 
            {
                Tools.MInfo("请选择产品项！");
                cboProducts.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtName.Text)||string.IsNullOrWhiteSpace(txtModel.Text))
            {
                Tools.MaWarn("产品名称和产品规格必须填写！");
                return;
            }

            //检查是否唯一；
            List<Product> ps = pm_bll.GetModelList(txtName.Text,txtModel.Text);
            if (ps.Count > 0)
            {
                Tools.MaError("产品名称+产品规格已存在！");
                return;
            }

            //创建对象；
            Product p = new Product();
            p.ProductName = txtName.Text;
            p.Model = txtModel.Text;
            p.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            p.Quantity = Convert.ToInt32(txtQuantity.Text);

            //添加产品；
            if (pm_bll.Add(p))
            {
                Tools.MaSucceed("新增成功√");
                txtName.Text = string.Empty;
                txtModel.Text = string.Empty;
                ShowRefresh();
            }
            else 
            {
                Tools.MaError("新增过程中出现异常！新增失败！");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvMessage.SelectedRows.Count <= 0)
            {
                Tools.MaWarn("请选定N行！");
                return;
            }

            if (Tools.MChoose(string.Format("确定删除选中项({0}项)吗？", dgvMessage.SelectedRows.Count)) == DialogResult.OK)
            {
                //获取Id列表；
                string ProductIdlist = string.Empty;
                for (int i = 0; i < dgvMessage.SelectedRows.Count;i++ )
                {
                    if(i==0)
                    {
                        ProductIdlist = dgvMessage.SelectedRows[0].Cells[0].Value.ToString();
                        continue;
                    }
                    ProductIdlist += "," + dgvMessage.SelectedRows[i].Cells[0].Value.ToString();
                }

                //检查此N项是否与其他数据[OrderDetails]有关联？
                List<OrderDetail> ds = odm_bll.GetModelList(ProductIdlist,"ProductId");
                if(ds.Count > 0)
                {
                    Tools.MaWarn("此N项中存有与其他数据相关联的选项！无法删除！删除失败！");
                    return;
                }

                //删除选中项；
                if(pm_bll.DeleteList(ProductIdlist))
                {
                    Tools.MaSucceed("删除成功√！");
                    ShowRefresh();
                }
                else
                {
                    Tools.MaError("删除过程中出现异常！删除失败！");
                }
            }
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUnitPrice.Text)) { txtUnitPrice.Text = "1"; return; }
            decimal money = 0;
            if(!decimal.TryParse(txtUnitPrice.Text,out money))
            {
                Tools.MaError("请输入有效的实数！");
                txtUnitPrice.Text = "1";
                txtUnitPrice.Focus();
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                txtQuantity.Text = "0";
                return;
            }
            int Count = 0;
            if(!int.TryParse(txtQuantity.Text,out Count))
            {
                Tools.MaError("输入错误,请输入有效的正整数！");
                txtQuantity.Focus();
                txtQuantity.Text = "0";
                return;
            }
            if(Convert.ToInt32(txtQuantity.Text) < 0)
            {
                Tools.MaError("输入错误,请输入有效的正整数！");
                txtQuantity.Text = "0";
                txtQuantity.Focus();
            }
        }

    }
}
