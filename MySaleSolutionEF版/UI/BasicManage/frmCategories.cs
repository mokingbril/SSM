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
    public partial class frmCategories : Form
    {

        private CategoryManage cs_bll = new CategoryManage();

        public frmCategories()
        {
            InitializeComponent();
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            dgvRefresh();
        }

        private void dgvRefresh()
        {
            dgvShow.DataSource = null;
            List<Category> ds = cs_bll.GetModelList("");
            dgvShow.AutoGenerateColumns = false;
            dgvShow.DataSource = ds;
        }

        private void dgvShow_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnUnpdate.Enabled = true;
            btnRemove.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text)||string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                Tools.MaError("请在文本框中输入新的类型名称！");
                txtCategoryName.Focus();
                return;
            }
            if (cs_bll.GetModelList(txtCategoryName.Text, "CategoryName").Count > 0)
            {
                Tools.MaWarn("此类型名称已存在！请重新输入！");
                txtCategoryName.Text = string.Empty;
                return;
            }
            Category cg = new Category 
            {
                CategoryName=txtCategoryName.Text
            };
            if(cs_bll.Add(cg))
            {
                Tools.MaSucceed("新增成功√！");
                txtCategoryName.Text = string.Empty;
                dgvRefresh();
            }
            else
            {
                Tools.MaError("新增过程中出现异常！新增失败！");
            }

        }

        private void btnUnpdate_Click(object sender, EventArgs e)
        {
            if (dgvShow.SelectedRows.Count != 1) 
            {
                Tools.MaWarn("请选定一行(仅一行)！");
                return;
            }
            if (string.IsNullOrEmpty(txtCategoryName.Text) || string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                Tools.MInfo("请在文本框中输入修改后的类型名称！");
                txtCategoryName.Focus();
                return;
            }
            Category cr = new Category();
            cr.CategoryId = Convert.ToInt32(dgvShow.SelectedRows[0].Cells[0].Value);
            cr.CategoryName = txtCategoryName.Text;
            if (cs_bll.Update(cr))
            {
                Tools.MaSucceed("更新成功√！");
                txtCategoryName.Text = string.Empty;
                dgvRefresh();
            }
            else 
            {
                Tools.MaError("更新过程中出现异常！更新失败！");
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvShow.SelectedRows.Count <= 0)
            {
                Tools.MaWarn("请选定N行！");
                return;
            }

            if (Tools.MChoose(string.Format("确定删除选中项({0}项)吗？",dgvShow.SelectedRows.Count)) == DialogResult.OK)
            {
                string CategoryIdList = string.Empty;
                for (int i = 0; i < dgvShow.SelectedRows.Count;i++ )
                {
                    if(i==0)
                    {
                        CategoryIdList = dgvShow.SelectedRows[0].Cells[0].Value.ToString();
                        continue;
                    }
                    CategoryIdList += "," + dgvShow.SelectedRows[i].Cells[0].Value.ToString(); ;
                }
                if(cs_bll.DeleteList(CategoryIdList))
                {
                    Tools.MaSucceed("删除成功√！");
                    dgvRefresh();
                }
                else
                {
                    Tools.MaError("删除过程中出现异常！删除失败！");
                }
            }
        }

        private void dgvShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUnpdate.Enabled = true;
            btnRemove.Enabled = true;
        }

    }
}
