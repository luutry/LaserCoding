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
    public partial class QR : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        MainForm mf = MainForm.mainform;

        public QR()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        }

        private void newQR_Click(object sender, EventArgs e)
        {
            NewAndEdit nae = new NewAndEdit();
            nae.ShowDialog();
        }

        private void editQR_Click(object sender, EventArgs e)
        {
            NewAndEdit nae = new NewAndEdit();
            nae.ShowDialog();
        }

        private void deleteQR_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ushort index =(ushort) comboBox1.SelectedIndex;
            CM.WriteMultipleRegisters(119, new ushort[] { (ushort)(index + 1) });
        
            
            CM.WriteSingleCoil(23, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result = CM.TemplateStatus;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                if (result == 1)
                {
                    comboBox1.SelectedIndex = index;
                    mf.showInfoLog("当前选择二维码为" + (index+1));
                    string  s = CM.ReadInputRegisters(10, 1).ToProfaceString();
                }
                else
                {
                    mf.showErrorLog("二维码选择出错，错误码为" + result);
                }

            }
        }
 
    }
}
