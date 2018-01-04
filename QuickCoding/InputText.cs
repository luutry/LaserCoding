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
    public partial class InputText : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        Logs logs = new Logs();
        string textpage = "text";
        public String inputResultTextByKey;

        public InputText()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        /// <summary>
        /// 新建文本变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbtnNew_Click(object sender, EventArgs e)
        {        
            string textName = TtxbName.Text;
            string textContent = TtxbContent.Text;
            string textSpacing = TtxbSpacing.Text;
            bool moCheck = TcheckBoxWidth.Checked ? true : false;

            MainForm mf = (MainForm)this.Owner;
            mf.TextName = TtxbName.Text;
           
            if (textName == "" && textContent == "")
            {
                mf.showErrorLog("名称、内容不能有空值！");
                return;
            }
            if (textName.Length < 40 && textContent.Length < 100)
            {
                CM.WriteMultipleRegisters(10001, textName.ToProfaceUshort(19));
                CM.WriteMultipleRegisters(10020, textContent.ToProfaceUshort(50));
                CM.WriteMultipleRegisters(10070, new ushort[]
                {
                    textSpacing.ToNullableDoubleUShort(),
                    moCheck.ToUShort()
                });
                ushort currentcount = CM.ExecutionCount;
                ushort result;
                CM.WriteSingleCoil(10004, true);
                if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                {
                    result = CM.TemplateStatus;
                    if (result == 1)
                    {
                        mf.showInfoLog("文本新建成功！");
                        logs.writeLogFile("文本新建成功！");
                    }
                    else
                    {
                        mf.showErrorLog("文本新建失败！ 错误码为" + result);
                    }
                   
                }
            }
            else if (textName.Length > 40)
            {
                mf.showErrorLog("名称太长啦！");           
            }
            else if (textName.Length > 100)
            {
                mf.showErrorLog("内容太长啦！");        
            }
            Close();
        }

        private void TtxbName_Click(object sender, EventArgs e)
        {
            keyboard keboard = new keyboard(textpage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            TtxbName.Text = inputResultTextByKey;
        }

        private void TtxbContent_Click(object sender, EventArgs e)
        {
            keyboard keboard = new keyboard(textpage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            TtxbContent.Text = inputResultTextByKey;
        }

        private void TtxbSpacing_Click(object sender, EventArgs e)
        {
            keyboard keboard = new keyboard(textpage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            TtxbSpacing.Text = inputResultTextByKey;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
