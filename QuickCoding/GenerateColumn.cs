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
    public partial class GenerateColumn : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        Logs logs = new Logs();
        string columnPage = "column";
        public String inputResult;

        public GenerateColumn()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)this.Owner;
            int count = int.Parse(columnCount.Text);

            if (count == 0 && columnSpacing.Text == null)
            {
                mf.showErrorLog("列数与列间距不能为空！");
            }
            if (count >= 2  )
            {
                double spacing = double.Parse(columnSpacing.Text);
                double total = spacing * (count - 1);
                double start = -(total / 2);
                for (int i = 1; i <= 12; i++)
                {
                    mf.GenerateColumn(i);
                    double x = start + (i - 1) * spacing;
                    if (i <= count)
                    {
                        if (x == 0)
                            x = 0.01;
                    }
                    else
                        x = 0;
                    CM.WriteMultipleRegisters(110, new ushort[] { x.ToUShort(), 0 });
                    ushort currentcount = CM.ExecutionCount;
                    CM.WriteSingleCoil(6, true);
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
                }
                mf.GenerateColumnColor(count);
            }
            else
            {
                mf.showInfoLog("至少设置列数为两列！");
            }
            mf.showInfoLog("已经成功生成"+ count +"列！");
            Close();
        }
       

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void columnCount_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard keboard = new NumOfKeyBoard(columnPage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            columnCount.Text = inputResult;
        }

        private void columnSpacing_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard keboard = new NumOfKeyBoard(columnPage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            columnSpacing.Text = inputResult;
        }
    }
}
