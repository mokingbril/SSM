using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace UI
{
    //工具类；
    public class Tools
    {
        //系统提示；
        public static void MaError(string msa)
        {
            MessageBox.Show(msa, "错误提示！", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MaSucceed(string msa)
        {
            MessageBox.Show(msa, "温馨提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MInfo(string msa)
        {
            MessageBox.Show(msa, "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MaWarn(string msa)
        {
            MessageBox.Show(msa, "系统提示！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        public static DialogResult MChoose(string msa)
        {
           DialogResult result= MessageBox.Show(msa, "系统提示！", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
           return result;
        }

        public static DialogResult MYN(string msa)
        {
            DialogResult result = MessageBox.Show(msa, "系统提示！", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            return result;
        }

        public static DialogResult MYNC(string msa)
        {
            DialogResult result = MessageBox.Show(msa, "系统提示！", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            return result;
        }
    }
}
