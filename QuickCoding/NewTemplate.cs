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
    public partial class NewTemplate : Form
    {
        ModbusManager CM = MainForm.mainform.masterWay.CM;
        MainForm mf = MainForm.mainform;
        string newtempPage = "newTemp";
        public String inputResult;

        public NewTemplate()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        }

        string name;
        ushort tempNum;
        private void btnOK_Click(object sender, EventArgs e)
        {
            mf.UpdateTemplateListData();
            if (mf.availableTempNumber != null)
            {
                tempNum = mf.availableTempNumber[0];
            }
            else
            {
                tempNum = 1;
            }
            name = tempName.Text;

            if (name == "")
            {
                mf.showErrorLog("模板名称不能为空！");
                return;
            }
            if (name.Length < 40)
            {
                if (tempNum > 100)
                {
                    mf.showErrorLog("新建模板数量已经超标!");                 
                }
                CM.WriteMultipleRegisters(0, new ushort[1] { (ushort)tempNum });
                CM.WriteMultipleRegisters(1, name.ToProfaceUshort(19));
                ushort currentcount = CM.ExecutionCount;
                ushort result;
                CM.WriteSingleCoil(0, true);
                if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                {
                    result = CM.TemplateStatus;
                    if (result == 1)
                    {
                        mf.showInfoLog("模板新建成功!");             
                        mf.UpdateTemplateListData();
                        mf.showTemplatesList();
                        Close();
                    }
                    else
                    {
                        mf.showErrorLog("新建失败，错误码为" + result);
                    }
                }
            }
            else
            {
                mf.showErrorLog("名字太长!");
            }
        }

        private void tempName_Click(object sender, EventArgs e)
        {
            keyboard keboard = new keyboard(newtempPage);
            keboard.Owner = this;
            keboard.StartPosition = FormStartPosition.CenterParent;
            keboard.ShowDialog();
            tempName.Text = inputResult;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
