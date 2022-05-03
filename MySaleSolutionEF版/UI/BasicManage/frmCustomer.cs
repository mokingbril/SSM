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
    public partial class frmCustomer : Form
    {
        private CustomerManage cm_bll = new CustomerManage();

        private OrderManage om_bll = new OrderManage();

        private List<Customer> csl = new List<Customer>();

        public frmCustomer()
        {
            InitializeComponent();
        }

        private void dgvMessage_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            ShowRefresh();
            AcceptButton = btnSelect;
        }

        private void ShowRefresh()
        {
            dgvMessage.DataSource = null;
            cboSelect.DataSource = null;
            cboSelect.Items.Clear();

            //获取数据信息并绑定控件；
            List<Customer> ds = cm_bll.GetModelList("");
            dgvMessage.AutoGenerateColumns = false;
            dgvMessage.DataSource = ds;
            cboSelect.DataSource = ds;
            cboSelect.DisplayMember = "CustomerId";
            cboSelect.ValueMember = "CustomerId";
            cboSelect.Text = "请输入编号或客户名";
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) { return; }

            long number = 0;
            if(!long.TryParse(txtPhone.Text,out number))
            {
                Tools.MaError("请输入有效的联系方式！");
                txtPhone.Text = "1";
                txtPhone.Focus();
            }
        }

        private void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtZipCode.Text))
            {
                return;
            }
            int number = 0;
            if (!int.TryParse(txtZipCode.Text, out number))
            {
                Tools.MaError("请输入有效的邮编号！");
                txtZipCode.Text = "0";
                txtZipCode.Focus();
                return;
            }
            if(txtZipCode.Text.Length > 6)
            {
                Tools.MaError("请输入有效的邮编号！");
                txtZipCode.Text = txtZipCode.Text.Substring(0,6);
                txtZipCode.Focus();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DialogResult result = Tools.MYNC("是否查找所有？");
            if(result==DialogResult.Yes)
            {
                ShowRefresh();
                return;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }

            if(string.IsNullOrWhiteSpace(cboSelect.Text))
            {
                Tools.MInfo("请输入编号或客户名！");
                cboSelect.Focus();
                return;
            }

            //查找ing并检查是否为null；
            int id = 0; 
            if (int.TryParse(cboSelect.Text, out id))
            {
                csl = cm_bll.GetModelList(cboSelect.Text, "CustomerId");
            }
            else 
            {
                csl = cm_bll.GetModelList(cboSelect.Text, "CustomerName");
            }
            if (csl.Count <= 0)
            {
                Tools.MaWarn("此编号或客户名不存在！");
                return;
            }
            else 
            {
                dgvMessage.DataSource = null;
                dgvMessage.AutoGenerateColumns = false;
                dgvMessage.DataSource = csl;
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtName.Text)|| string.IsNullOrWhiteSpace(txtContact.Text))
            {
                Tools.MInfo("客户名称和联系人不能为空！");
                return;
            }
            if(!string.IsNullOrWhiteSpace(txtId.Text))
            {
                return;
            }
            Customer cs = new Customer();
            cs.CustomerName = txtName.Text;
            cs.Contact = txtContact.Text;
            cs.Email = txtEmail.Text;
            cs.Phone = txtPhone.Text;
            cs.ZipCode = txtZipCode.Text;
            cs.City = txtCity.Text;
            cs.Address = txtAddress.Text;

            if (cm_bll.Add(cs))
            {
                Tools.MaSucceed("添加成功√！");
                ShowRefresh();
            }
            else 
            {
                Tools.MaError("新增过程中出现异常！新增失败！");
            }
        }

        private void dgvMessage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(dgvMessage.SelectedRows.Count!=1)
            {
                Tools.MInfo("请选择一行【仅一行】！");
                return;
            }

            //输入修改后的信息
            if(btnUpdate.Text.Equals("确定"))
            {
                //创建对象；
                Customer cs = new Customer();
                cs.CustomerId = Convert.ToInt32(txtId.Text);
                cs.CustomerName = txtName.Text;
                cs.Contact = txtContact.Text;
                cs.Phone = txtPhone.Text;
                cs.Email = txtEmail.Text;
                cs.City = txtCity.Text;
                cs.Address = txtAddress.Text;
                cs.ZipCode = txtZipCode.Text;

                //输入ing；
                if (cm_bll.Update(cs))
                {
                    Tools.MaSucceed("修改成功√！");
                    ShowRefresh();
                    txtId.Text = string.Empty;
                    txtName.Text = string.Empty;
                    btnUpdate.Text = "修改";
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else 
                {
                    Tools.MaError("修改过程中出现异常！修改失败！");
                }
                return;
            }

            //加载信息；
            txtId.Text = dgvMessage.SelectedRows[0].Cells[0].Value.ToString();
            txtName.Text = dgvMessage.SelectedRows[0].Cells[1].Value.ToString();
            txtContact.Text = dgvMessage.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = dgvMessage.SelectedRows[0].Cells[3].Value.ToString();
            txtEmail.Text = dgvMessage.SelectedRows[0].Cells[4].Value.ToString();
            txtCity.Text = dgvMessage.SelectedRows[0].Cells[5].Value.ToString();
            txtAddress.Text = dgvMessage.SelectedRows[0].Cells[6].Value.ToString();
            txtZipCode.Text = dgvMessage.SelectedRows[0].Cells[7].Value.ToString();

            //提示信息；
            Tools.MInfo("请修改左下方的信息，并点击“确定”按钮或按下Enter键！");
            btnUpdate.Text = "确定";
            btnAdd.Enabled = false; 
            btnDelete.Enabled = false;
            this.AcceptButton = btnUpdate;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(btnUpdate.Text.Equals("确定"))
            {
                btnDelete.Enabled = false;
                return;
            }
            if (dgvMessage.SelectedRows.Count <= 0)
            {
                Tools.MaWarn("请选定N行！");
                return;
            }

            //温馨提示！
            if (Tools.MChoose(string.Format("确定删除选中项({0}项)吗？", dgvMessage.SelectedRows.Count)) == DialogResult.OK)
            {
                //获取Id列表；
                string CustomerIdList = string.Empty;
                for (int i = 0; i < dgvMessage.SelectedRows.Count; i++)
                {
                    if (i == 0)
                    {
                        CustomerIdList = dgvMessage.SelectedRows[0].Cells[0].Value.ToString();
                        continue;
                    }
                    CustomerIdList += "," + dgvMessage.SelectedRows[i].Cells[0].Value.ToString();
                }

                //检查是否有数据关联；
                List<Order> dn = om_bll.GetModelList(CustomerIdList, "CustomerId");
                if (dn.Count > 0)
                {
                    Tools.MaError("此N项中存有与其他数据相关联的选项！无法删除！删除失败！");
                    return;
                }

                //执行删除命令ing；
                if (cm_bll.DeleteList(CustomerIdList))
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

    }
}
