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
using QuickCoding;

namespace HDNing
{
    public partial class InputSerial : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        Logs logs = new Logs();
        string serialPage = "serial";
        public String inputResult;

        public InputSerial()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        /// <summary>
        /// 新建序列号变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnNew_Click(object sender, EventArgs e)
        {
            string serialName = StxbName.Text;
            string serialStart = StxbStart.Text;
            string serialEnd = StxbEnd.Text;
            string serialAdd = StxbAdd.Text;
            string serialRepeat = StxbRepeat.Text;
            string serialMin = StxbMin.Text;
            string serialSpacing = StxbSpacing.Text;
            bool moCheck = ScheckWidth.Checked ? true : false;

            MainForm mf = (MainForm)this.Owner;
            mf.SerialName = serialName;

            if (serialName == "" && serialStart == "")
            {
                mf.showErrorLog("名称、内容不能有空值！");
                return;
            }
            if (serialName.Length < 40 && serialStart.Length < 100)
            {
                CM.WriteMultipleRegisters(30001, serialName.ToProfaceUshort(19));
                CM.WriteMultipleRegisters(30020, new ushort[]{
                    serialStart.ToNullableIntUShort(),
                    serialEnd.ToNullableIntUShort(),
                    serialAdd.ToNullableIntUShort(),
                    serialRepeat.ToNullableIntUShort(),
                    serialMin.ToNullableIntUShort(),
                    serialSpacing.ToNullableDoubleUShort(),
                    moCheck.ToUShort()
                });             
                ushort currentcount = CM.ExecutionCount;
                ushort result;
                CM.WriteSingleCoil(30005, true);
                if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                {
                    result = CM.TemplateStatus;
                    if (result == 1)
                    {
                        mf.showInfoLog("序列号新建成功！");
                        logs.writeLogFile("序列号新建成功！");
                    }
                    else
                    {
                        mf.showInfoLog("序列号新建失败， 错误码为" +result);
                    }
            
                }
            }
            else if (serialName.Length > 20)
            {
                mf.showErrorLog("名称太长啦！");   
            }
            else if (serialName.Length > 50)
            {
                mf.showErrorLog("内容太长啦！");   
            }
            Close();
        }

        private void StxbName_Click(object sender, EventArgs e)
        {
            keyboard keboard = new keyboard(serialPage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            StxbName.Text = inputResult;
        }

        private void showNumKeyBoard()
        {
            NumOfKeyBoard keboard = new NumOfKeyBoard(serialPage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
        }

        

        private void StxbStart_Click(object sender, EventArgs e)
        {
            showNumKeyBoard();
            StxbStart.Text = inputResult;
        }

        private void StxbEnd_Click(object sender, EventArgs e)
        {
           showNumKeyBoard();
            StxbEnd.Text = inputResult;
        } 

        private void StxbAdd_Click(object sender, EventArgs e)
        {
            showNumKeyBoard();
            StxbAdd.Text = inputResult;
        }

        private void StxbRepeat_Click(object sender, EventArgs e)
        {
            showNumKeyBoard();
            StxbRepeat.Text = inputResult;
        }

        private void StxbMin_Click(object sender, EventArgs e)
        {
            showNumKeyBoard();
            StxbMin.Text = inputResult;
        }

        private void StxbSpacing_Click(object sender, EventArgs e)
        {
            showNumKeyBoard();
            StxbSpacing.Text = inputResult;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
