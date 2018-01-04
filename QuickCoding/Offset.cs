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
    public partial class Offset : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        MainForm mf = MainForm.mainform;

        private void Offset_Load(object sender, EventArgs e)
        {
            XY = CM.ReadInputRegisters(474, 2);
            tbOff_X.Text = ((short)XY[0]).ToString();
            tbOff_Y.Text = ((short)XY[1]).ToString();
        }

        public Offset()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        ushort[] XY;
        short currentX;
        short currentY;
        private void set(short x, short y)
        {
            CM.WriteMultipleRegisters(114, new ushort[] { (ushort)x,
                (ushort) y });
            CM.WriteSingleCoil(7, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                if (result == 1)
                {
                    XY = CM.ReadInputRegisters(474, 2);
                    currentX += (short)XY[0];
                    currentY += (short)XY[1];
                    tbOff_X.Text = currentX.ToString();
                    tbOff_Y.Text = currentY.ToString();
                    mf.showInfoLog("设置成功");
                }
                else
                {
                    mf.showErrorLog("设置失败！错误码为" + result);
                }
            }
        }

        private void OffsetRight_Click(object sender, EventArgs e)
        {
            short x = CM.OffsetX;
            short y = CM.OffsetY;
            x += 1;
            set(x, y);
        }
        private void OffsetUp_Click(object sender, EventArgs e)
        {
            short x = CM.OffsetX;
            short y = CM.OffsetY;
            y += 1;
            set(x, y);
        }

        private void Offsetleft_Click(object sender, EventArgs e)
        {
            short x = CM.OffsetX;
            short y = CM.OffsetY;
            x -= 1;
            set(x, y);
        }

        private void Offsetbottom_Click(object sender, EventArgs e)
        {
            short x = CM.OffsetX;
            short y = CM.OffsetY;
            y -= 1;
            set(x, y);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    
    }
}
