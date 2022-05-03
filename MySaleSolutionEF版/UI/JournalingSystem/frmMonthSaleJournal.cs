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
    public partial class frmMonthSaleJournal : Form
    {
        private OrderManage om_bll = new OrderManage();

        public frmMonthSaleJournal()
        {
            InitializeComponent();
        }

        private void frmMonthSaleJournal_Load(object sender, EventArgs e)
        {
            dgvMessage.DataSource = null;
            dgvMessage.AutoGenerateColumns = false;
            //string strSql = "select DATEPART(MONTH,OrderDate) as '月份',*,'-1'as '月总额' from Orders  union "
            //                +" select DATEPART(MONTH,OrderDate) as '月份','','-1','','-1','',SUM(Total) from Orders "
            //                +" group by DATEPART(MONTH,OrderDate) ";
            List<Order> los = om_bll.GetTableList();
            ShowAddMethod(los);
            dgvMessage.DataSource = los;
        }

        private static void ShowAddMethod(List<Order> los)
        {
            int MinMonth = 0;
            decimal MonthTotal = 0;
            for (int i = 0; i < los.Count; i++)
            {
                int Month = los[i].OrderDate.Value.Month;
                if (i == 0 && i != (los.Count - 1))
                {
                    MinMonth = Month;
                    MonthTotal += (decimal)los[i].Total;
                }
                else if (Month > MinMonth && i != (los.Count - 1))
                {
                    Order oo = new Order
                    {
                        OrderNo = string.Empty,
                        CustomerId = -1,
                        OrderDate = Convert.ToDateTime(string.Format("1900/{0}/1 00:00:00.000", MinMonth)),
                        Total = -MonthTotal,
                        Status = false,
                    };
                    los.Add(oo);
                    for (int j = los.Count-1; j >i ; j--)
                    {
                        los[j] = los[j - 1];
                    }
                    los[i] = oo;
                    i++;
                    MinMonth = Month;
                    MonthTotal = 0;
                }
                else if (i == (los.Count - 1))
                {
                    if (Month > MinMonth)
                    {
                        Order ob = new Order
                        {
                            OrderNo = string.Empty,
                            CustomerId = -1,
                            OrderDate = Convert.ToDateTime(string.Format("1900/{0}/1 00:00:00.000", MinMonth)),
                            Total = -MonthTotal,
                            Status = false,
                        };
                        los.Add(ob);
                        for (int j = los.Count - 1; j > i; j--)
                        {
                            los[j] = los[j - 1];
                        }
                        los[i] = ob;
                        MinMonth = Month;
                        MonthTotal = 0;
                    }
                    else 
                    {
                        MonthTotal += (decimal)los[i].Total;
                        Order oo = new Order
                        {
                            OrderNo = string.Empty,
                            CustomerId = -1,
                            OrderDate = Convert.ToDateTime(string.Format("1900/{0}/01 00:00:00.000", MinMonth)),
                            Total = -MonthTotal,
                            Status = false,
                        };
                        los.Add(oo);
                        break;
                    }
                    
                }
                else
                {
                    MonthTotal += (decimal)los[i].Total;
                }
            }
        }

        private void dgvMessage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = Convert.ToDateTime(e.Value).Month;
            }
            if(e.ColumnIndex==2 && e.Value.Equals(-1))
            {
                e.Value = string.Empty;
            }
            DateTime dt = Convert.ToDateTime("1900/01/01 00:00:00.000");
            if (e.ColumnIndex == 3 && Convert.ToDateTime(e.Value).Year==1900)
            {
                e.Value = string.Empty;
            }
            if (e.ColumnIndex == 4 && (decimal)e.Value < 0)
            {
                e.Value = string.Empty;
            }
            if (e.ColumnIndex == 6)
            {
                if ((decimal)e.Value >= 0)
                {
                    e.Value = string.Empty;
                }
                else 
                {
                    e.Value = -(decimal)e.Value;
                }
            }

            
        }

    }
}
