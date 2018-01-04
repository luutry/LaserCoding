using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using QuickCoding;

namespace HDNing
{
    public partial class InputDate : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        Logs logs = new Logs();
        String datePage = "date";
        public string inputResult;

        public InputDate()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        /// <summary>
        /// 新建日期变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DbtnNew_Click(object sender, EventArgs e)
        {
            string dateName = DtxbName.Text;
            string dateDecollator = DtxbDecollator.Text;
            int dateFormat = DtxbDateFormat.SelectedIndex +1;
            string dateSpacing = DtxbSpacing.Text;
            bool moCheck = DcheckWidth.Checked ? true : false;

            MainForm mf = (MainForm)this.Owner;
            mf.DateName = dateName;

            if (dateName == "" && dateDecollator == "" && dateFormat == 0)
            {
                mf.showErrorLog("名称、分隔符、格式不能有空值！ ");           
                return;
            }
            if (dateName.Length < 40)
            {
                CM.WriteMultipleRegisters(20001, dateName.ToProfaceUshort(19));
                CM.WriteMultipleRegisters(20020, dateDecollator.ToProfaceUshort(1));
                CM.WriteMultipleRegisters(20021, new ushort[]{
                    (ushort)dateFormat,
                    DtxbSpacing.Text.ToNullableDoubleUShort(),
                    moCheck.ToUShort() 
                });
                
                ushort currentcount = CM.ExecutionCount;
                ushort result;
                CM.WriteSingleCoil(20004, true);
                if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                {
                    result = CM.TemplateStatus;
                    if (result == 1)
                    {
                        mf.showInfoLog("日期变量新建成功！");
                        logs.writeLogFile("日期变量新建成功！");          
                    }
                    else
                    {
                        mf.showErrorLog("日期变量新建错误，错误码为：" + result);
                    }
                      
                }
            }
            else if (dateName.Length > 20)
            {
                mf.showErrorLog("名称太长啦！");
            }
            Close();
        }

        private void DtxbName_Click(object sender, EventArgs e)
        {
            keyboard keboard = new keyboard(datePage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            DtxbName.Text = inputResult;
        }

        private void DtxbDecollator_Click(object sender, EventArgs e)
        {
            keyboard keboard = new keyboard(datePage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            DtxbDecollator.Text = inputResult;
        }

        private void DtxbSpacing_Click(object sender, EventArgs e)
        {
            keyboard keboard = new keyboard(datePage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            DtxbSpacing.Text = inputResult;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
