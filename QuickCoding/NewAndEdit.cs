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
    public partial class NewAndEdit : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        MainForm mf = MainForm.mainform;

        public NewAndEdit()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CM.WriteMultipleRegisters(40001, textBoxName.Text.ToProfaceUshort(19));
            CM.WriteMultipleRegisters(40020, textBoxValue.Text.ToProfaceUshort(49));
            CM.WriteMultipleRegisters(40070, new ushort[]{
                textBoxCount.Text.ToNullableIntUShort(),
                textBoxShape.Text.ToNullableIntUShort(),
                textBoxCode.Text.ToNullableIntUShort(),
                textBoxWidth.Text.ToNullableIntUShort(),
                textBoxX.Text.ToNullableDoubleUShort(),
                textBoxY.Text.ToNullableDoubleUShort()
            });

            CM.WriteSingleCoil(40004, true);
            ushort result = CM.TemplateStatus;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                if (result == 1)
                {
                    mf.showInfoLog("新建二维码成功!");
                }
                else
                {
                    mf.showErrorLog("新建二维码出错，错误码为" + result);
                }
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
