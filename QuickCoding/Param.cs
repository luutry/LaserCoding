using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HDNing;

namespace QuickCoding
{
    public partial class Param : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        MainForm mf = MainForm.mainform;
        string paramPage = "param";
        public String inputResult;


        public Param()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            showParams();
        }

        private void showParams()
        {        
             tbPower.Text = mf.currentParams[0].ToString();
             tbPrintSpeed.Text = mf.currentParams[1].ToString();          
             tbFrequency.Text = mf.currentParams[8].ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CM.WriteMultipleRegisters(476, new ushort[]{
                tbPower.Text.ToNullableIntUShort(),
                tbPrintSpeed.Text.ToNullableIntUShort(),            
            });
            CM.WriteMultipleRegisters(484, new ushort[]{
                 tbFrequency.Text.ToNullableIntUShort()
            });
            CM.WriteSingleCoil(10, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                if (result == 1)
                {
                    mf.showInfoLog("设置成功!");
                }
                else
                {
                    mf.showErrorLog("设置失败,错误码为" + result);
                }
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbPower_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard keboard = new NumOfKeyBoard(paramPage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            tbPower.Text = inputResult;
        }

        private void tbPrintSpeed_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard keboard = new NumOfKeyBoard(paramPage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            tbPrintSpeed.Text = inputResult;
        }

        private void tbFrequency_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard keboard = new NumOfKeyBoard(paramPage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            tbFrequency.Text = inputResult;
        }
    }
}
