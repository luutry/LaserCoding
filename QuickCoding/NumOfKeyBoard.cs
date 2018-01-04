using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HDNing;

namespace QuickCoding
{
    public partial class NumOfKeyBoard : Form
    {
        String content = null;
        string currentPage;

        public NumOfKeyBoard( String pageSympol)
        {
            currentPage = pageSympol;
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void b7_Click(object sender, EventArgs e)
        {
            content += "7";
            tbxInputString.Text = content;
        }

        private void b8_Click(object sender, EventArgs e)
        {
            content += "8";
            tbxInputString.Text = content;
        }

        private void b9_Click(object sender, EventArgs e)
        {
            content += "9";
            tbxInputString.Text = content;
        }

        private void b4_Click(object sender, EventArgs e)
        {
            content += "4";
            tbxInputString.Text = content;
        }

        private void b5_Click(object sender, EventArgs e)
        {
            content += "5";
            tbxInputString.Text = content;
        }

        private void b6_Click(object sender, EventArgs e)
        {
            content += "6";
            tbxInputString.Text = content;
        }

        private void b1_Click(object sender, EventArgs e)
        {
            content += "1";
            tbxInputString.Text = content;
        }

        private void b2_Click(object sender, EventArgs e)
        {
            content += "2";
            tbxInputString.Text = content;
        }

        private void b3_Click(object sender, EventArgs e)
        {
            content += "3";
            tbxInputString.Text = content;
        }

        private void b0_Click(object sender, EventArgs e)
        {
            content += "0";
            tbxInputString.Text = content;
        }

        private void bdot_Click(object sender, EventArgs e)
        {
            content += ".";
            tbxInputString.Text = content;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (currentPage == "text")
            {
                InputText it = (InputText)this.Owner;
                it.inputResultTextByKey = tbxInputString.Text;
            }
            if (currentPage == "home")
            {
                MainForm mf = (MainForm)this.Owner;
                mf.inputResult = tbxInputString.Text;
            }
            if (currentPage == "date")
            {
                InputDate id = (InputDate)this.Owner;
                id.inputResult = tbxInputString.Text;
            }
            if (currentPage == "serial")
            {
                InputSerial isl = (InputSerial)this.Owner;
                isl.inputResult = tbxInputString.Text;
            }
            if (currentPage == "column")
            {
                GenerateColumn gc = (GenerateColumn)this.Owner;
                gc.inputResult =  tbxInputString.Text;
            }
            if (currentPage == "param")
            {
                Param pa = (Param)this.Owner;
                pa.inputResult = tbxInputString.Text;
            }
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            if (content != null && content != "")
            {
                content = content.Substring(0, content.Length - 1);
                tbxInputString.Text = content;
            }     
            else
                content = null;         
        }

        private void bcancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
