namespace UI
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuManage = new System.Windows.Forms.MenuStrip();
            this.mnuBasicManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCategoryManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomerManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBusinessDeal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOrderingManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOrderList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuJournalingSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonthSaleJournal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductsSortStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonthSaleAmount = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEachCategoryMoney = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomerMoney = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuManage
            // 
            this.mnuManage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBasicManage,
            this.mnuBusinessDeal,
            this.mnuJournalingSystem,
            this.mnuMonthSaleAmount,
            this.mnuExit});
            this.mnuManage.Location = new System.Drawing.Point(0, 0);
            this.mnuManage.Name = "mnuManage";
            this.mnuManage.Size = new System.Drawing.Size(660, 25);
            this.mnuManage.TabIndex = 0;
            this.mnuManage.Text = "menuStrip1";
            // 
            // mnuBasicManage
            // 
            this.mnuBasicManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCategoryManage,
            this.mnuProductManage,
            this.mnuCustomerManage});
            this.mnuBasicManage.Name = "mnuBasicManage";
            this.mnuBasicManage.Size = new System.Drawing.Size(92, 21);
            this.mnuBasicManage.Text = "基本数据管理";
            // 
            // mnuCategoryManage
            // 
            this.mnuCategoryManage.Name = "mnuCategoryManage";
            this.mnuCategoryManage.Size = new System.Drawing.Size(148, 22);
            this.mnuCategoryManage.Text = "产品类型管理";
            this.mnuCategoryManage.Click += new System.EventHandler(this.mnuCategoryManage_Click);
            // 
            // mnuProductManage
            // 
            this.mnuProductManage.Name = "mnuProductManage";
            this.mnuProductManage.Size = new System.Drawing.Size(148, 22);
            this.mnuProductManage.Text = "产品数据管理";
            this.mnuProductManage.Click += new System.EventHandler(this.mnuProductManage_Click);
            // 
            // mnuCustomerManage
            // 
            this.mnuCustomerManage.Name = "mnuCustomerManage";
            this.mnuCustomerManage.Size = new System.Drawing.Size(148, 22);
            this.mnuCustomerManage.Text = "客户数据管理";
            this.mnuCustomerManage.Click += new System.EventHandler(this.mnuCustomerManage_Click);
            // 
            // mnuBusinessDeal
            // 
            this.mnuBusinessDeal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddOrder,
            this.mnuOrderingManage,
            this.mnuOrderList});
            this.mnuBusinessDeal.Name = "mnuBusinessDeal";
            this.mnuBusinessDeal.Size = new System.Drawing.Size(68, 21);
            this.mnuBusinessDeal.Text = "业务处理";
            // 
            // mnuAddOrder
            // 
            this.mnuAddOrder.Name = "mnuAddOrder";
            this.mnuAddOrder.Size = new System.Drawing.Size(148, 22);
            this.mnuAddOrder.Text = "下订单";
            this.mnuAddOrder.Click += new System.EventHandler(this.mnuAddOrder_Click);
            // 
            // mnuOrderingManage
            // 
            this.mnuOrderingManage.Name = "mnuOrderingManage";
            this.mnuOrderingManage.Size = new System.Drawing.Size(148, 22);
            this.mnuOrderingManage.Text = "订单过程管理";
            this.mnuOrderingManage.Click += new System.EventHandler(this.mnuOrderingManage_Click);
            // 
            // mnuOrderList
            // 
            this.mnuOrderList.Name = "mnuOrderList";
            this.mnuOrderList.Size = new System.Drawing.Size(148, 22);
            this.mnuOrderList.Text = "订单列表";
            this.mnuOrderList.Click += new System.EventHandler(this.mnuOrderList_Click);
            // 
            // mnuJournalingSystem
            // 
            this.mnuJournalingSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMonthSaleJournal,
            this.mnuProductsSortStatistics});
            this.mnuJournalingSystem.Name = "mnuJournalingSystem";
            this.mnuJournalingSystem.Size = new System.Drawing.Size(68, 21);
            this.mnuJournalingSystem.Text = "报表系统";
            // 
            // mnuMonthSaleJournal
            // 
            this.mnuMonthSaleJournal.Name = "mnuMonthSaleJournal";
            this.mnuMonthSaleJournal.Size = new System.Drawing.Size(172, 22);
            this.mnuMonthSaleJournal.Text = "月销售报表";
            this.mnuMonthSaleJournal.Click += new System.EventHandler(this.mnuMonthSaleJournal_Click);
            // 
            // mnuProductsSortStatistics
            // 
            this.mnuProductsSortStatistics.Name = "mnuProductsSortStatistics";
            this.mnuProductsSortStatistics.Size = new System.Drawing.Size(172, 22);
            this.mnuProductsSortStatistics.Text = "产品销售分类统计";
            this.mnuProductsSortStatistics.Click += new System.EventHandler(this.mnuProductsSortStatistics_Click);
            // 
            // mnuMonthSaleAmount
            // 
            this.mnuMonthSaleAmount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEachCategoryMoney,
            this.mnuCustomerMoney});
            this.mnuMonthSaleAmount.Name = "mnuMonthSaleAmount";
            this.mnuMonthSaleAmount.Size = new System.Drawing.Size(80, 21);
            this.mnuMonthSaleAmount.Text = "月销售总额";
            // 
            // mnuEachCategoryMoney
            // 
            this.mnuEachCategoryMoney.Name = "mnuEachCategoryMoney";
            this.mnuEachCategoryMoney.Size = new System.Drawing.Size(196, 22);
            this.mnuEachCategoryMoney.Text = "每种产品类型的订购额";
            this.mnuEachCategoryMoney.Click += new System.EventHandler(this.mnuEachCategoryMoney_Click);
            // 
            // mnuCustomerMoney
            // 
            this.mnuCustomerMoney.Name = "mnuCustomerMoney";
            this.mnuCustomerMoney.Size = new System.Drawing.Size(196, 22);
            this.mnuCustomerMoney.Text = "每个客户的订购额";
            this.mnuCustomerMoney.Click += new System.EventHandler(this.mnuCustomerMoney_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(44, 21);
            this.mnuExit.Text = "退出";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(660, 446);
            this.Controls.Add(this.mnuManage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuManage;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "销售管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mnuManage.ResumeLayout(false);
            this.mnuManage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuManage;
        private System.Windows.Forms.ToolStripMenuItem mnuBasicManage;
        private System.Windows.Forms.ToolStripMenuItem mnuBusinessDeal;
        private System.Windows.Forms.ToolStripMenuItem mnuJournalingSystem;
        private System.Windows.Forms.ToolStripMenuItem mnuMonthSaleAmount;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuCategoryManage;
        private System.Windows.Forms.ToolStripMenuItem mnuProductManage;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomerManage;
        private System.Windows.Forms.ToolStripMenuItem mnuAddOrder;
        private System.Windows.Forms.ToolStripMenuItem mnuOrderingManage;
        private System.Windows.Forms.ToolStripMenuItem mnuOrderList;
        private System.Windows.Forms.ToolStripMenuItem mnuMonthSaleJournal;
        private System.Windows.Forms.ToolStripMenuItem mnuProductsSortStatistics;
        private System.Windows.Forms.ToolStripMenuItem mnuEachCategoryMoney;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomerMoney;
    }
}

