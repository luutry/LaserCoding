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
    public partial class EditText : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        Logs logs = new Logs();


        public EditText()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        }

        private void showTextInfo()
        {
            string content =  CM.ReadInputRegisters(100, 50).ToProfaceString();
        }

        private void TbtnNew_Click(object sender, EventArgs e)
        {
            CM.WriteSingleCoil(22, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }
    }
}
