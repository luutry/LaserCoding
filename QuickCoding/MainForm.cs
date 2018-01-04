using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
//using System.Windows.FrameworkElement;
using System.Data.Linq;
using System.Diagnostics;
using System.Threading;
using HDNing;
using Microsoft.Win32;


namespace QuickCoding
{
    public partial class MainForm : Form
    {
        public MasterWay masterWay = new MasterWay();
        Logs logs = new Logs();
        TemplateVar currentTemplate;
        public static MainForm mainform;
        ListViewItem lvi;
        int varSpacingNum = -1;
        int lastSelectRow;
        private Point mPoint = new Point();
        string homeSympol = "home";

        AutoSizeFormClass asc = new AutoSizeFormClass();

        public MainForm()
        {
            InitializeComponent();
            mainform = this;
       //  this.WindowState = FormWindowState.Maximized;
            this.Width = 1024;
            this.Height = 768;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            AutoStart();
        }

        public  void AutoStart()
        {
          string KJLJ = Application.ExecutablePath;
            if (!System.IO.File.Exists(KJLJ))//判断指定文件是否存在
                return;
            string newKJLJ = KJLJ.Substring(KJLJ.LastIndexOf("\\") + 1);
            RegistryKey Rkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (Rkey == null)
            {
                Rkey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            }
            Rkey.SetValue(newKJLJ, KJLJ);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }


        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxLogs.DrawMode = DrawMode.OwnerDrawFixed;
            comboBoxLogs.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox3.SelectedIndex = 0;
            this.comboBox4.SelectedIndex = 0;
            this.comboBox5.SelectedIndex = 0;
            comboBoxLogs.Items.Add(logs.showInfo("程序启动！"));
            IPTextbox.Text= Properties.Settings.Default.IPAddress;
            this.comboBoxLogs.SelectedIndex = 0;
            asc.controlAutoSize(this);
            connectByIP_Click(null, null);
        }

        #region TCP连接
        /// <summary>
        ///  TCP 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void connectByIP_Click(object sender, EventArgs e)
        {
           ModbusManager CM = masterWay.CM;

            if (!CM.TCPConnected)
            {
                try
                {
                    string ip = IPTextbox.Text;
                    if (ip == "")
                    {
                        comboBoxLogs.Items.Insert(0, logs.showError("请输入主机IP!"));
                        comboBoxLogs.SelectedIndex = 0;
                        return;
                    }
                    CM.TCPConnect(ip);
                    CM.Start();                                      
                    comboBoxLogs.Items.Insert(0, logs.showInfo("连接成功!"));
                    comboBoxLogs.SelectedIndex = 0;
                    connectByIP.Text = "已连接";
                    connectByIP.BackColor = System.Drawing.Color.LightBlue;
                    UpdateTemplateListData();
                    showTemplatesList();
               
                    Properties.Settings.Default.IPAddress = ip;
                    Properties.Settings.Default.Save();
                   
                }
                catch
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("连接失败!"));
                    comboBoxLogs.SelectedIndex = 0; 
                }
            }
            else
            {
                CM.Stop();
                CM.TCPDisconnect();
                comboBoxLogs.Items.Insert(0, logs.showInfo("已断开!"));
                comboBoxLogs.SelectedIndex = 0; 
                connectByIP.Text = "未连接";
            //    systemlistView.Clear();
                listView1.Clear();
                connectByIP.BackColor = System.Drawing.Color.Gainsboro;
            }
        }

       
        /// <summary>
        ///  退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitProgram_Click(object sender, EventArgs e)
        {
             ModbusManager CM = masterWay.CM;
             if (CM.TCPConnected)
             {
                 CM.Stop();
                 CM.TCPDisconnect();
                 listView1.Clear();
             }
            Close();
        }

        #endregion

        #region 变量控制部分  

        public string textName;
        public string dateName;
        public string serialName;

        public string TextName
        {
            get { return textName; }
            set { textName = value; }
        }
  
        public string DateName
        {
            get { return dateName; }
            set { dateName = value; }
        }
     
        public string SerialName
        {
            get { return serialName; }
            set { serialName = value; }
        }

        public void showErrorLog(string str)
        {
            this.comboBoxLogs.Items.Insert(0, logs.showError(str));
            comboBoxLogs.SelectedIndex = 0;
        }
        public void showInfoLog(string str)
        {
            this.comboBoxLogs.Items.Insert(0, logs.showInfo(str));
            comboBoxLogs.SelectedIndex = 0;
        }

        /// <summary>
        ///  变量一 弹窗 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int lastSelectVarNum;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            lastSelectVarNum = 1;
            if (this.tabControlNav.SelectedTab != tabPage2)
            {
                return;
            }
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        textBox1.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result != 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("选择变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        CM.WriteSingleCoil(21, true);
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result == 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showInfo("删除变量成功！"));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                            else
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("删除变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        textBox1.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[]{ (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        { 
                            result = CM.TemplateStatus;
                        }
                        InputText inputText = new InputText();
                        inputText.Owner = this;
                        inputText.ShowDialog();
                        inputText.StartPosition = FormStartPosition.CenterParent;
                        textBox1.Text = TextName;
                        break;
                    }
                case 2:
                    {
                        textBox1.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputDate inputDate = new InputDate();
                        inputDate.Owner = this;
                        inputDate.ShowDialog();
                        inputDate.StartPosition = FormStartPosition.CenterParent;
                        textBox1.Text = DateName;
                        break;
                    }
                case 3:
                    {
                        textBox1.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputSerial inputSerial = new InputSerial();
                        inputSerial.Owner = this;
                        inputSerial.ShowDialog();
                        inputSerial.StartPosition = FormStartPosition.CenterParent;
                        textBox1.Text = SerialName;                       
                        break;
                    }
            }
        }

        /// <summary>
        ///  变量二 弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lastSelectVarNum = 2;
            ModbusManager CM = masterWay.CM;
            if (this.tabControlNav.SelectedTab != tabPage2)
            {
                return;
            }
            switch (this.comboBox2.SelectedIndex)
            {
                case 0:
                    {
                        textBox2.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result != 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("选择变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        CM.WriteSingleCoil(21, true);
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result == 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showInfo("删除变量成功！"));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                            else
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("删除变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        textBox2.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputText inputText = new InputText();
                        inputText.Owner = this;
                        inputText.ShowDialog();
                        inputText.StartPosition = FormStartPosition.CenterParent;
                        textBox2.Text = TextName;
                        break;
                    }
                case 2:
                    {
                        textBox2.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputDate inputDate = new InputDate();
                        inputDate.Owner = this;
                        inputDate.ShowDialog();
                        inputDate.StartPosition = FormStartPosition.CenterParent;
                        textBox2.Text = DateName;
                        break;
                    }
                case 3:
                    {
                        textBox2.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputSerial inputSerial = new InputSerial();
                        inputSerial.Owner = this;
                        inputSerial.ShowDialog();
                        inputSerial.StartPosition = FormStartPosition.CenterParent;
                        textBox2.Text = SerialName;
                        break;
                    }
            }
        }

        /// <summary>
        ///  变量三 弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lastSelectVarNum = 3;
            ModbusManager CM = masterWay.CM;
            if (this.tabControlNav.SelectedTab != tabPage2)
            {
                return;
            }
            switch (this.comboBox3.SelectedIndex)
            {
                case 0:
                    {
                        textBox3.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result != 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("选择变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        CM.WriteSingleCoil(21, true);
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result == 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showInfo("删除变量成功！"));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                            else
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("删除变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        textBox3.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputText inputText = new InputText();
                        inputText.Owner = this;
                        inputText.ShowDialog();
                        inputText.StartPosition = FormStartPosition.CenterParent;
                        textBox3.Text = TextName;
                        break;
                    }
                case 2:
                    {
                        textBox3.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputDate inputDate = new InputDate();
                        inputDate.Owner = this;
                        inputDate.ShowDialog();
                        inputDate.StartPosition = FormStartPosition.CenterParent;
                        textBox3.Text = DateName;
                        break;
                    }
                case 3:
                    {
                        textBox3.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputSerial inputSerial = new InputSerial();
                        inputSerial.Owner = this;
                        inputSerial.ShowDialog();
                        inputSerial.StartPosition = FormStartPosition.CenterParent;
                        textBox3.Text = SerialName;
                        break;
                    }
            }
        }

        /// <summary>
        ///  变量四 弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            lastSelectVarNum = 4;
            ModbusManager CM = masterWay.CM;
            if (this.tabControlNav.SelectedTab != tabPage2)
            {
                return;
            }
            switch (this.comboBox4.SelectedIndex)
            {
                case 0:
                    {
                        textBox4.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result != 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("选择变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        CM.WriteSingleCoil(21, true);
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result == 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showInfo("删除变量成功！"));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                            else
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("删除变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        textBox4.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputText inputText = new InputText();
                        inputText.Owner = this;
                        inputText.ShowDialog();
                        inputText.StartPosition = FormStartPosition.CenterParent;
                        textBox4.Text = TextName;
                        break;
                    }
                case 2:
                    {
                        textBox4.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputDate inputDate = new InputDate();
                        inputDate.Owner = this;
                        inputDate.ShowDialog();
                        inputDate.StartPosition = FormStartPosition.CenterParent;
                        textBox4.Text = DateName;
                        break;
                    }
                case 3:
                    {
                        textBox4.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputSerial inputSerial = new InputSerial();
                        inputSerial.Owner = this;
                        inputSerial.ShowDialog();
                        inputSerial.StartPosition = FormStartPosition.CenterParent;
                        textBox4.Text = SerialName;
                        break;
                    }
            }
        }

        /// <summary>
        ///  变量五 弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            lastSelectVarNum = 5;
            ModbusManager CM = masterWay.CM;
            if (this.tabControlNav.SelectedTab != tabPage2)
            {
                return;
            }
            switch (this.comboBox5.SelectedIndex)
            {
                case 0:
                    {
                        textBox5.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result != 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("选择变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        CM.WriteSingleCoil(21, true);
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                            if (result == 1)
                            {
                                comboBoxLogs.Items.Insert(0, logs.showInfo("删除变量成功！"));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                            else
                            {
                                comboBoxLogs.Items.Insert(0, logs.showError("删除变量失败！错误码为:" + result));
                                comboBoxLogs.SelectedIndex = 0;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        textBox5.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputText inputText = new InputText();
                        inputText.Owner = this;
                        inputText.ShowDialog();
                        inputText.StartPosition = FormStartPosition.CenterParent;
                        textBox5.Text = TextName;
                        break;
                    }
                case 2:
                    {
                        textBox5.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputDate inputDate = new InputDate();
                        inputDate.Owner = this;
                        inputDate.ShowDialog();
                        inputDate.StartPosition = FormStartPosition.CenterParent;
                        textBox5.Text = DateName;
                        break;
                    }
                case 3:
                    {
                        textBox5.Clear();
                        CM.WriteMultipleRegisters(101, new ushort[] { (ushort)lastSelectVarNum });
                        CM.WriteSingleCoil(15, true);
                        ushort currentcount = CM.ExecutionCount;
                        ushort result;
                        if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                        {
                            result = CM.TemplateStatus;
                        }
                        InputSerial inputSerial = new InputSerial();
                        inputSerial.Owner = this;
                        inputSerial.ShowDialog();
                        inputSerial.StartPosition = FormStartPosition.CenterParent;
                        textBox5.Text = SerialName;
                        break;
                    }
            }
        }

        // 变量1与2之间间距修改
        private void spacing1v2_Leave(object sender, EventArgs e)
        {
            varSpacingNum = 1;
            varSpacingByRowAndVar(lastSelectRow, varSpacingNum);
        }
        // 变量2与3之间间距修改
        private void spacing2v3_Leave(object sender, EventArgs e)
        {
            varSpacingNum = 2;
            varSpacingByRowAndVar(lastSelectRow, varSpacingNum);
        }
        //变量3与4之间间距修改
        private void spacing3v4_Leave(object sender, EventArgs e)
        {
            varSpacingNum = 3;
            varSpacingByRowAndVar(lastSelectRow, varSpacingNum);
        }
        //变量4与5之间间距修改
        private void spacing4v5_Leave(object sender, EventArgs e)
        {
            varSpacingNum = 4;
            varSpacingByRowAndVar(lastSelectRow, varSpacingNum);
        }
        //修改变量间距写入模板类
        private void varSpacingByRowAndVar(int rowIndex, int varSpacingNum)
        {
            ModbusManager CM = masterWay.CM;
            if (varSpacingNum == 1)
            {
                CM.WriteMultipleRegisters(102, new ushort[] { spacing1v2.Text.ToNullableIntUShort() });
            }
            else if (varSpacingNum == 2)
            {
                CM.WriteMultipleRegisters(103, new ushort[] { spacing2v3.Text.ToNullableIntUShort() });
            }
            else if (varSpacingNum == 3)
            {
                CM.WriteMultipleRegisters(104, new ushort[] { spacing3v4.Text.ToNullableIntUShort() });
            }
            else if (varSpacingNum == 4)
            {
                CM.WriteMultipleRegisters(105, new ushort[] { spacing4v5.Text.ToNullableIntUShort() });
            }
            CM.WriteSingleCoil(16, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                if (result == 1)
                {
                    comboBoxLogs.Items.Insert(0, logs.showInfo("Success!"));
                    comboBoxLogs.SelectedIndex = 0;
                }
                else
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("变量间距更新失败！错误码为" + result));
                    comboBoxLogs.SelectedIndex = 0;
                } 
            }
        }
        #endregion 

        #region 行 选择 部分

        /// <summary>
        /// 行字号修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxFontSize_Leave(object sender, EventArgs e)
        {
            if (this.tabControlNav.SelectedIndex != 1)
            {
                return;
            }
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters(107, new ushort[] { comboBoxFontSize.Text.ToNullableIntUShort() });
            CM.WriteSingleCoil(19, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                if (result == 1)
                {
                    comboBoxLogs.Items.Insert(0, logs.showInfo("Success!"));
                    comboBoxLogs.SelectedIndex = 0;
                }
                else
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("更新失败！错误码为" + result));
                    comboBoxLogs.SelectedIndex = 0;
                } 
            }

        }

        /// <summary>
        /// 行间距修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxRowSpace_Leave(object sender, EventArgs e)
        {
            if (this.tabControlNav.SelectedIndex != 1)
            {
                return;
            }
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters(108, new ushort[] { textBoxRowSpace.Text.ToNullableIntUShort() });
            CM.WriteSingleCoil(20, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                if (result == 1)
                {
                    comboBoxLogs.Items.Insert(0, logs.showInfo("Success!"));
                    comboBoxLogs.SelectedIndex = 0;
                }
                else
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("行间距更新失败！错误码为" + result));
                    comboBoxLogs.SelectedIndex = 0;
                } 
            }
        }
        
        // 按行将模板数据读出
        private void inputDateByRow(ushort index)
        {
            ModbusManager CM = masterWay.CM;

            ushort[] row = CM.ReadInputRegisters((ushort)(200 + (index - 1) * 50), 15);
     
            foreach (VarByCurrentTemp var in varbyCurrentTempList)
            {
                if (var.VarType == "Text2D")
                {
                    var.VarType = "1";
                }
                if (var.VarType == "Date2D")
                {
                    var.VarType = "2";
                }
                if (var.VarType == "Serial2D")
                {
                    var.VarType = "3";
                }
                if (index == var.RowNum)
                {
                    // 变量一
                    if(var.VarNum == 1)
                    {
                         if (row[0].ToString() == var.VarType)
                         {
                             this.comboBox1.SelectedIndex = (int)row[0];
                             textBox1.Text = var.VarContent;
                             spacing1v2.Text = row[2].ToString();
                         }
                    }
                    // 变量二
                    if (var.VarNum == 2)
                    {
                        if (row[3].ToString() == var.VarType)
                        {
                            comboBox2.SelectedIndex = row[3];
                            textBox2.Text = var.VarContent;
                            spacing2v3.Text = row[5].ToString();
                        }
                    }
                    // 变量三
                    if (var.VarNum == 3)
                    {
                        if (row[6].ToString() == var.VarType)
                        {
                            comboBox3.SelectedIndex = row[6];
                            textBox3.Text = var.VarContent;
                            spacing3v4.Text = row[8].ToString();
                        }
                    }
                    // 变量四
                    if (var.VarNum == 4)
                    {
                        if (row[9].ToString() == var.VarType)
                        {
                            comboBox4.SelectedIndex = row[9];
                            textBox4.Text = var.VarContent;
                            spacing4v5.Text = row[11].ToString();
                        }
                    }
                    // 变量五
                    if (var.VarNum == 5)
                    {
                        if (row[12].ToString() == var.VarType)
                        {
                            comboBox5.SelectedIndex = row[12];
                            textBox5.Text = var.VarContent;
                        }
                    }  
                }
            }
            ushort[] fontfamily = CM.ReadInputRegisters((ushort)(218 + (index-1) * 50), 20);
            comboBoxFontFamily.Text = fontfamily.ToProfaceString();
            ushort[] fontsize_rowspacing = CM.ReadInputRegisters((ushort)(216 + (index-1) * 50), 2);
            comboBoxFontSize.Text = fontsize_rowspacing[0].ToString();
            textBoxRowSpace.Text = ((short)fontsize_rowspacing[1]).ToString();

            ///  TODO

            currentTemplate.rowVar[index -1].Font = fontfamily;
            currentTemplate.rowVar[index -1].FontSize = fontsize_rowspacing[0];
            currentTemplate.rowVar[index -1].RowSpacing = fontsize_rowspacing[1];  
        }

        private void clearVarByRowToShow()
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

    
        //选择第一行
        private void btnRow1_Click(object sender, EventArgs e)
        {
            lastSelectRow = 1;
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters((ushort)100, new ushort[] { (ushort)lastSelectRow });
            CM.WriteSingleCoil(14, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
            clearVarByRowToShow();
            inputDateByRow((ushort)lastSelectRow);
            btnRow1.BackColor = System.Drawing.Color.Turquoise;
            btnRow2.BackColor = System.Drawing.Color.Transparent;
            btnRow3.BackColor = System.Drawing.Color.Transparent;
            btnRow4.BackColor = System.Drawing.Color.Transparent;
            btnRow5.BackColor = System.Drawing.Color.Transparent;       
        }

        //选择第二行
        private void button2_Click(object sender, EventArgs e)
        {
            lastSelectRow = 2;
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters((ushort)100, new ushort[] { (ushort)lastSelectRow });
            CM.WriteSingleCoil(14, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
            clearVarByRowToShow();
            inputDateByRow((ushort)lastSelectRow);
            btnRow2.BackColor = System.Drawing.Color.Turquoise;
            btnRow1.BackColor = System.Drawing.Color.Transparent;
            btnRow3.BackColor = System.Drawing.Color.Transparent;
            btnRow4.BackColor = System.Drawing.Color.Transparent;
            btnRow5.BackColor = System.Drawing.Color.Transparent;    
        }

        // 选择第三行
        private void btnRow3_Click(object sender, EventArgs e)
        {
            lastSelectRow = 3;
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters((ushort)100, new ushort[] { (ushort)lastSelectRow });
            CM.WriteSingleCoil(14, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
            clearVarByRowToShow();
            inputDateByRow((ushort)lastSelectRow);
            btnRow3.BackColor = System.Drawing.Color.Turquoise;
            btnRow1.BackColor = System.Drawing.Color.Transparent;
            btnRow2.BackColor = System.Drawing.Color.Transparent;
            btnRow4.BackColor = System.Drawing.Color.Transparent;
            btnRow5.BackColor = System.Drawing.Color.Transparent;
        }

        //选择第四行
        private void btnRow4_Click(object sender, EventArgs e)
        {
            lastSelectRow = 4;
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters((ushort)100, new ushort[] { (ushort)lastSelectRow });
            CM.WriteSingleCoil(14, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
            clearVarByRowToShow();
            inputDateByRow((ushort)lastSelectRow);
            btnRow4.BackColor = System.Drawing.Color.Turquoise;
            btnRow1.BackColor = System.Drawing.Color.Transparent;
            btnRow2.BackColor = System.Drawing.Color.Transparent;
            btnRow3.BackColor = System.Drawing.Color.Transparent;
            btnRow5.BackColor = System.Drawing.Color.Transparent;
        }

        //选择第五行
        private void btnRow5_Click(object sender, EventArgs e)
        {
            lastSelectRow = 5;
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters((ushort)100, new ushort[] { (ushort)lastSelectRow });
            CM.WriteSingleCoil(14, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
            clearVarByRowToShow();
            inputDateByRow((ushort)lastSelectRow);
            btnRow5.BackColor = System.Drawing.Color.Turquoise;
            btnRow1.BackColor = System.Drawing.Color.Transparent;
            btnRow2.BackColor = System.Drawing.Color.Transparent;
            btnRow3.BackColor = System.Drawing.Color.Transparent;
            btnRow4.BackColor = System.Drawing.Color.Transparent;
        }

        
        #endregion 

        #region 主页操作部分
        //主页加载模板btn
        private void btnHomeLoad_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            if (!CM.Connected)
            {
                comboBoxLogs.Items.Insert(0, logs.showError("程序未连接！"));
                comboBoxLogs.SelectedIndex = 0;
                return;
            }
            if (listView1.SelectedItems.Count == 0)
            {
                comboBoxLogs.Items.Insert(0,logs.showInfo("请在模板列表中选择一项"));
                comboBoxLogs.SelectedIndex = 0;
                return;
            }
            string selectedtemp = null;
            selectedtemp = listView1.FocusedItem.Text;
            int num = int.Parse(selectedtemp);
            CM.WriteMultipleRegisters(0, new ushort[1] { (ushort)num });
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            CM.WriteSingleCoil(2, true);
            currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                if (result != 1)
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("选择模板失败， 错误码为：" + result));
                    comboBoxLogs.SelectedIndex = 0;
                }
            }
            CM.WriteSingleCoil(4, true);
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                if (result == 1)
                {
                    comboBoxLogs.Items.Insert(0, logs.showInfo("加载成功!"));
                    comboBoxLogs.SelectedIndex = 0;
                }
                else
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("加载失败， 错误码为：" + result));
                    comboBoxLogs.SelectedIndex = 0;
                }
            }
        }
            
        //新建模板(切换到新建模板界面)
        private void newTemplate_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            if (!CM.Connected)
            {
                comboBoxLogs.Items.Insert(0, logs.showError("程序未连接！"));
                comboBoxLogs.SelectedIndex = 0;
                return;
            }

            NewTemplate newTemp = new NewTemplate();
            newTemp.StartPosition = FormStartPosition.CenterParent;
            newTemp.ShowDialog();        
        }

        public List<ushort> availableTempNumber;
      
        List<TemplateVar> currentTemplateList;
        // 读取模板数据写入模板类
        public void UpdateTemplateListData()
        {
            //刷新可用模板变量序号
            availableTempNumber=new List<ushort> ();
            currentTemplateList = new List<TemplateVar>();
            ModbusManager CM = masterWay.CM;
            for (ushort i = 0; i < 100; i++)
            {
                ushort[] number;
                number = CM.ReadInputRegisters((ushort)(2000 + i * 20), 20);

                if (number != null && number[0] != 0)
                {
                    currentTemplate = new TemplateVar();
                    ushort[] nm = CM.ReadInputRegisters((ushort)(2000 + i * 20 + 1), 19);
                    string name = nm.ToProfaceString();

                    currentTemplate.Number = number[0];

                    currentTemplate.Name = name;
                    currentTemplateList.Add(currentTemplate);
                }
                else
                {
                    availableTempNumber.Add((ushort)(i+1));
                }
            }
        }
        //显示模板列表
        public void  showTemplatesList()
        {
          
            listView1.Clear();
            this.listView1.FullRowSelect = true;
            this.listView1.LargeImageList = imageList1;
            this.listView1.View = View.Details;
            this.listView1.BeginUpdate();
            listView1.Columns.Add("序号");
            listView1.Columns.Add("模板名");
            listView1.Columns[1].Width = 120;

            foreach (TemplateVar tv in currentTemplateList)
            {
                lvi = new ListViewItem(new string[] { tv.Number.ToString(), tv.Name });
                this.listView1.Items.Add(lvi);
            } 
            this.listView1.EndUpdate();
        }
        private void showDeleteTemplatesList()
        {

            this.listView1.FullRowSelect = true;
            this.listView1.LargeImageList = imageList1;
            this.listView1.View = View.Details;
            this.listView1.Clear();
            this.listView1.BeginUpdate();
            listView1.Columns.Add("序号");
            listView1.Columns.Add("模板名");
            listView1.Columns[1].Width = listView1.Width / 2;
            listView1.GridLines = true;

            foreach (TemplateVar tv in currentTemplateList)
            {
                int index = int.Parse(deleteNum);
                if (index != tv.Number)
                {
                    lvi = new ListViewItem(new string[] { tv.Number.ToString(), tv.Name });
                    this.listView1.Items.Add(lvi);
                } 
            }

            this.listView1.EndUpdate();
        }

        /// <summary>
        ///  显示当前模板列信息
        /// </summary>
        /// <param name="tempNum"></param>
        public ushort[] currentOffXY;
        public ushort[] currentParams;
        private void showTempLine(string tempNum)
        {
            ModbusManager CM = masterWay.CM;
            selectedTempNum.Text = tempNum;
            currentOffXY = CM.ReadInputRegisters(474, 2);
            currentParams = CM.ReadInputRegisters(476, 9);
            ushort[] angle = CM.ReadInputRegisters(485, 1);
            currentColumnAngle = (short)angle[0];
            spanAngle.Text = currentColumnAngle.ToString();
            ushort[] columnXY = CM.ReadInputRegisters(450, 24);
            if (columnXY[0] != 0 || columnXY[1] != 0)
            {
                this.line1.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[2] != 0 || columnXY[3] != 0)
            {
                this.line2.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[4] != 0 || columnXY[5] != 0)
            {
                this.line3.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[6] != 0 || columnXY[7] != 0)
            {
                this.line4.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[8] != 0 || columnXY[9] != 0)
            {
                this.line5.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[10] != 0 || columnXY[11] != 0)
            {
                this.line6.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[12] != 0 || columnXY[13] != 0)
            {
                this.line7.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[14] != 0 || columnXY[15] != 0)
            {
                this.line8.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[16] != 0 || columnXY[17] != 0)
            {
                this.line9.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[18] != 0 || columnXY[19] != 0)
            {
                this.line10.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[20] != 0 || columnXY[21] != 0)
            {
                this.line11.BackColor = System.Drawing.Color.Turquoise;
            }
            if (columnXY[22] != 0 || columnXY[23] != 0)
            {
                this.line12.BackColor = System.Drawing.Color.Turquoise;
            }
        }

        string currentTempName;
        private string gettenpName( int num )
        {
            ModbusManager CM = masterWay.CM;
            for (int i = 0; i < 100; i++)
            {
                ushort[] result =  CM.ReadInputRegisters((ushort)(2000 + (20*i)) , 1);
                if (num == result[0])
                {
                    ushort[] name = CM.ReadInputRegisters((ushort)(2001 + (20 * i)), 19);
                    currentTempName = name.ToProfaceString();   
                }
            }
            return currentTempName;
        }

        /// <summary>
        ///  显示当前模板详细信息
        /// </summary>
        /// <param name="arg"></param>
        List<VarByCurrentTemp> varbyCurrentTempList;
        ushort currentTempateNumber;
        private void showDetailInfoByTemp(string tempNum) 
        { 
            ModbusManager CM = masterWay.CM;
            currentTemplate = new TemplateVar();
            currentTemplate.Number = Convert.ToUInt16(tempNum);
            int num = int.Parse(tempNum);
            currentTempateNumber = (ushort)num;
            gettenpName(num);
            numOftemp.Text = "序号：" + num.ToString();
            nameOfCurrentTemp.Text = "名称：" + currentTempName;
            nameOfTemp.Text = currentTempName;
            CM.WriteMultipleRegisters(0, new ushort[1]{(ushort)num});
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            CM.WriteSingleCoil(2, true);
            currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
            showTempLine(tempNum);
            showParams();
            varbyCurrentTempList = masterWay.findAllVarByCurrentTemp();
            btnRow1_Click(null, null);
        }


        /// <summary>
        /// 显示参数
        /// </summary>
        private void showParams()
        {
            tbPower.Text = currentParams[0].ToString();
            tbPrintSpeed.Text = currentParams[1].ToString();
            tbFrequency.Text = currentParams[8].ToString();
        }
        // 编辑模板
        private void btnEditTemp_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            if (!CM.Connected)
            {
                comboBoxLogs.Items.Insert(0, logs.showError("程序未连接！"));
                comboBoxLogs.SelectedIndex = 0;
                return;
            }
           // showTemplatesList();
            if (listView1.SelectedItems.Count ==0)
            {
                comboBoxLogs.Items.Insert(0,logs.showInfo("请在模板列表中选择一项"));
                comboBoxLogs.SelectedIndex = 0;
                return;
            }
            string selectedtemp = null;
            selectedtemp = listView1.FocusedItem.Text;
            showDetailInfoByTemp(selectedtemp);
            // 跳到模板页面          
           this.tabControlNav.SelectedTab = tabPage2;
        }

        string deleteNum;
        //删除模板
        private void deleteTemp_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            if (!CM.Connected)
            {
                comboBoxLogs.Items.Insert(0, logs.showError("程序未连接！"));
                comboBoxLogs.SelectedIndex = 0;
                return;
            }
            if (listView1.SelectedItems.Count == 0)
            {
                comboBoxLogs.Items.Insert(0,logs.showInfo("请在模板列表中选择一项"));
                comboBoxLogs.SelectedIndex = 0;
                return;
            }
            deleteNum = listView1.FocusedItem.Text;
            int index = int.Parse(deleteNum);
            CM.WriteMultipleRegisters(0, new ushort[1] { (ushort)index });
            ushort currentcount = CM.ExecutionCount;
            CM.WriteSingleCoil(1, true);
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                comboBoxLogs.Items.Insert(0,logs.showInfo("删除成功！"));
                comboBoxLogs.SelectedIndex = 0;

                availableTempNumber.Add((ushort)index);



                UpdateTemplateListData();
                showDeleteTemplatesList();
            }
            else
            {
                comboBoxLogs.Items.Insert(0,logs.showError("删除失败！"));
                comboBoxLogs.SelectedIndex = 0;
            }
        }
        #endregion

        #region 模板页操作部分
        /// <summary>
        ///  二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQR_Click(object sender, EventArgs e)
        {
        //   QR qr = new QR();
     //       qr.Show();
        }

        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParams_Click(object sender, EventArgs e)
        {
            Param param = new Param();
            param.StartPosition = FormStartPosition.CenterParent;
            param.ShowDialog();
        }

        /// <summary>
        /// 偏移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skewing_Click(object sender, EventArgs e)
        {
            Offset offset = new Offset();
            offset.StartPosition = FormStartPosition.CenterParent;
            offset.ShowDialog();
        }

        ListViewItem systemLvi;
        Dictionary<string, string> systemParam;
        /// <summary>
        ///  系统参数显示
        /// </summary>
       
        #endregion

        int lastSelectTemplateNum;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem itm in this.listView1.Items)
            {
                itm.BackColor = SystemColors.Window;
                itm.ForeColor = Color.Black;
            }
            foreach (ListViewItem itm2 in this.listView1.SelectedItems)
            {
                itm2.BackColor = SystemColors.MenuHighlight;
                itm2.ForeColor = Color.White;
            }
            if (this.listView1.SelectedIndices.Count > 0)
            {
                //Graphics g = pictureBox1.CreateGraphics();
                //g.Clear(System.Drawing.Color.White);
                string selectedtemp = null;
                selectedtemp = listView1.FocusedItem.Text;
                currentTemplate.Number = Convert.ToUInt16(selectedtemp);
                ModbusManager CM = masterWay.CM;
                currentTemplate = new TemplateVar();
                lastSelectTemplateNum = int.Parse(selectedtemp);
                CM.WriteMultipleRegisters(0, new ushort[1] { (ushort)lastSelectTemplateNum });
                ushort currentcount = CM.ExecutionCount;
                ushort result;
                CM.WriteSingleCoil(2, true);
                currentcount = CM.ExecutionCount;
                if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
                {
                    result = CM.TemplateStatus;
                }
                 varbyCurrentTempList = masterWay.findAllVarByCurrentTemp();
                 displayTemp();
            }
        }

        /// <summary>
        ///  展示模板信息
        /// </summary>
        /// 
   
        private void displayTemp()
        {        
            ModbusManager CM = masterWay.CM;
            vainTempShowList();
            foreach (VarByCurrentTemp var in varbyCurrentTempList)
            {
                if (var.VarType == "Text2D")
                {
                    var.VarType = "1";
                }
                if (var.VarType == "Date2D")
                {
                    var.VarType = "2";
                }
                if (var.VarType == "Serial2D")
                {
                    var.VarType = "3";
                }

                if (1 == var.RowNum)
                {
                    ushort[] row = CM.ReadInputRegisters((ushort)(200), 50);
                    if (row.ToString() != "")
                    {
                        rpanel1.Visible = true;
                    }
                    // 变量一
                    if (var.VarNum == 1)
                    {
                        if (row[0].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv11.Text = "文本:" + var.VarName;
                                rv11c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv11.Text = "日期:" + var.VarName;
                                rv11c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv11.Text = "序列号:" + var.VarName;
                                rv11c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量二
                    if (var.VarNum == 2)
                    {
                        if (row[3].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv12.Text = "文本:" + var.VarName;
                                rv12c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv12.Text = "日期:" + var.VarName;
                                rv12c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv12.Text = "序列号:" + var.VarName;
                                rv12c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量三
                    if (var.VarNum == 3)
                    {
                        if (row[6].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv13.Text = "文本:" + var.VarName;
                                rv13c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv13.Text = "日期:" + var.VarName;
                                rv13c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv13.Text = "序列号:" + var.VarName;
                                rv13c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量四
                    if (var.VarNum == 4)
                    {
                        if (row[9].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv14.Text = "文本:" + var.VarName;
                                rv14c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv14.Text = "日期:" + var.VarName;
                                rv14c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv14.Text = "序列号:" + var.VarName;
                                rv14c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量五
                    if (var.VarNum == 5)
                    {
                        if (row[12].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv15.Text = "文本:" + var.VarName;
                                rv15c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv15.Text = "日期:" + var.VarName;
                                rv15c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv15.Text = "序列号:" + var.VarName;
                                rv15c.Text = var.VarContent;
                            }
                        }
                    }
                }
                // row 2
                if (2 == var.RowNum)
                {
                    ushort[] row = CM.ReadInputRegisters((ushort)(250), 50);
                    if (row.ToString() != "")
                    {
                        rpanel2.Visible = true;
                    }
                    // 变量一
                    if (var.VarNum == 1)
                    {
                        if (row[0].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv21.Text = "文本:" + var.VarName;
                                rv21c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv21.Text = "日期:" + var.VarName;
                                rv21c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv21.Text = "序列号:" + var.VarName;
                                rv21c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量二
                    if (var.VarNum == 2)
                    {
                        if (row[3].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv22.Text = "文本:" + var.VarName;
                                rv22c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv22.Text = "日期:" + var.VarName;
                                rv22c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv22.Text = "序列号:" + var.VarName;
                                rv22c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量三
                    if (var.VarNum == 3)
                    {
                        if (row[6].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv23.Text = "文本:" + var.VarName;
                                rv23c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv23.Text = "日期:" + var.VarName;
                                rv23c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv23.Text = "序列号:" + var.VarName;
                                rv23c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量四
                    if (var.VarNum == 4)
                    {
                        if (row[9].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv24.Text = "文本:" + var.VarName;
                                rv24c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv24.Text = "日期:" + var.VarName;
                                rv24c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv24.Text = "序列号:" + var.VarName;
                                rv24c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量五
                    if (var.VarNum == 5)
                    {
                        if (row[12].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv25.Text = "文本:" + var.VarName;
                                rv25c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv25.Text = "日期:" + var.VarName;
                                rv25c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv25.Text = "序列号:" + var.VarName;
                                rv25c.Text = var.VarContent;
                            }
                        }
                    }
                }
                /// row 3
                if (3 == var.RowNum)
                {
                    ushort[] row = CM.ReadInputRegisters((ushort)(300), 50);
                    if (row.ToString() != "")
                    {
                        rpanel3.Visible = true;
                    }
                    // 变量一
                    if (var.VarNum == 1)
                    {
                        if (row[0].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv31.Text = "文本:" + var.VarName;
                                rv31c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv31.Text = "日期:" + var.VarName;
                                rv31c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv31.Text = "序列号:" + var.VarName;
                                rv31c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量二
                    if (var.VarNum == 2)
                    {
                        if (row[3].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv32.Text = "文本:" + var.VarName;
                                rv32c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv32.Text = "日期:" + var.VarName;
                                rv32c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv32.Text = "序列号:" + var.VarName;
                                rv32c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量三
                    if (var.VarNum == 3)
                    {
                        if (row[6].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv33.Text = "文本:" + var.VarName;
                                rv33c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv33.Text = "日期:" + var.VarName;
                                rv33c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv33.Text = "序列号:" + var.VarName;
                                rv33c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量四
                    if (var.VarNum == 4)
                    {
                        if (row[9].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv34.Text = "文本:" + var.VarName;
                                rv34c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv34.Text = "日期:" + var.VarName;
                                rv34c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv34.Text = "序列号:" + var.VarName;
                                rv34c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量五
                    if (var.VarNum == 5)
                    {
                        if (row[12].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv35.Text = "文本:" + var.VarName;
                                rv35c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv35.Text = "日期:" + var.VarName;
                                rv35c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv35.Text = "序列号:" + var.VarName;
                                rv35c.Text = var.VarContent;
                            }
                        }
                    }
                }
                // row 4
                if (4 == var.RowNum)
                {
                    ushort[] row = CM.ReadInputRegisters((ushort)(350), 50);
                    if (row.ToString() != "")
                    {
                        rpanel4.Visible = true;
                    }
                    // 变量一
                    if (var.VarNum == 1)
                    {
                        if (var.VarType == "1")
                        {
                            rv41.Text = "文本:" + var.VarName;
                            rv41c.Text = var.VarContent;
                        }
                        if (var.VarType == "2")
                        {
                            rv41.Text = "日期:" + var.VarName;
                            rv41c.Text = var.VarContent;
                        }
                        if (var.VarType == "3")
                        {
                            rv41.Text = "序列号:" + var.VarName;
                            rv41c.Text = var.VarContent;
                        }
                    }
                    // 变量二
                    if (var.VarNum == 2)
                    {
                        if (row[3].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv42.Text = "文本:" + var.VarName;
                                rv42c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv42.Text = "日期:" + var.VarName;
                                rv42c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv42.Text = "序列号:" + var.VarName;
                                rv42c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量三
                    if (var.VarNum == 3)
                    {
                        if (row[6].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv43.Text = "文本:" + var.VarName;
                                rv43c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv43.Text = "日期:" + var.VarName;
                                rv43c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv43.Text = "序列号:" + var.VarName;
                                rv43c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量四
                    if (var.VarNum == 4)
                    {
                        if (row[9].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv44.Text = "文本:" + var.VarName;
                                rv44c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv44.Text = "日期:" + var.VarName;
                                rv44c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv44.Text = "序列号:" + var.VarName;
                                rv44c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量五
                    if (var.VarNum == 5)
                    {
                        if (row[12].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv45.Text = "文本:" + var.VarName;
                                rv45c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv45.Text = "日期:" + var.VarName;
                                rv45c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv45.Text = "序列号:" + var.VarName;
                                rv45c.Text = var.VarContent;
                            }
                        }
                    }
                }
                // row 5
                if (5 == var.RowNum)
                {
                    ushort[] row = CM.ReadInputRegisters((ushort)(400), 50);
                    if (row.ToString() != "")
                    {
                        rpanel5.Visible = true;
                    }
                    // 变量一
                    if (var.VarNum == 1)
                    {
                        if (row[0].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv51.Text = "文本:" + var.VarName;
                                rv51c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv51.Text = "日期:" + var.VarName;
                                rv51c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv51.Text = "序列号:" + var.VarName;
                                rv51c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量二
                    if (var.VarNum == 2)
                    {
                        if (row[3].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv52.Text = "文本:" + var.VarName;
                                rv52c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv52.Text = "日期:" + var.VarName;
                                rv52c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv52.Text = "序列号:" + var.VarName;
                                rv52c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量三
                    if (var.VarNum == 3)
                    {
                        if (row[6].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv53.Text = "文本:" + var.VarName;
                                rv53c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv53.Text = "日期:" + var.VarName;
                                rv53c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv53.Text = "序列号:" + var.VarName;
                                rv53c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量四
                    if (var.VarNum == 4)
                    {
                        if (row[9].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv54.Text = "文本:" + var.VarName;
                                rv54c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv54.Text = "日期:" + var.VarName;
                                rv54c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv54.Text = "序列号:" + var.VarName;
                                rv54c.Text = var.VarContent;
                            }
                        }
                    }
                    // 变量五
                    if (var.VarNum == 5)
                    {
                        if (row[12].ToString() == var.VarType)
                        {
                            if (var.VarType == "1")
                            {
                                rv55.Text = "文本:" + var.VarName;
                                rv55c.Text = var.VarContent;
                            }
                            if (var.VarType == "2")
                            {
                                rv55.Text = "日期:" + var.VarName;
                                rv55c.Text = var.VarContent;
                            }
                            if (var.VarType == "3")
                            {
                                rv55.Text = "序列号:" + var.VarName;
                                rv55c.Text = var.VarContent;
                            }
                        }
                    }
                }
            }
        }

        private void vainTempShowList(){
            rv11.Text = ""; rv12.Text = ""; rv13.Text = ""; rv14.Text = ""; rv15.Text = "";
            rv21.Text = ""; rv22.Text = ""; rv23.Text = ""; rv24.Text = ""; rv25.Text = "";
            rv31.Text = ""; rv32.Text = ""; rv33.Text = ""; rv34.Text = ""; rv35.Text = "";
            rv41.Text = ""; rv42.Text = ""; rv43.Text = ""; rv44.Text = ""; rv45.Text = "";
            rv51.Text = ""; rv52.Text = ""; rv53.Text = ""; rv54.Text = ""; rv55.Text = "";
            rv11c.Text = ""; rv12c.Text = ""; rv13c.Text = ""; rv14c.Text = ""; rv15c.Text = "";
            rv21c.Text = ""; rv22c.Text = ""; rv23c.Text = ""; rv24c.Text = ""; rv25c.Text = "";
            rv31c.Text = ""; rv32c.Text = ""; rv33c.Text = ""; rv34c.Text = ""; rv35c.Text = "";
            rv41c.Text = ""; rv42c.Text = ""; rv43c.Text = ""; rv44c.Text = ""; rv45c.Text = "";
            rv51c.Text = ""; rv52c.Text = ""; rv53c.Text = ""; rv54c.Text = ""; rv55c.Text = "";
            rpanel1.Visible = false;
            rpanel2.Visible = false;
            rpanel3.Visible = false;
            rpanel4.Visible = false;
            rpanel5.Visible = false;
        }

        //private void tempView()
        //{
        //               //if (1 == var.RowNum)
        //        //{
        //        //    ushort[] row = CM.ReadInputRegisters((ushort)(200), 50);
        //        //    // 变量一
        //        //    if (var.VarNum == 1)
        //        //    {
        //        //        if (row[0].ToString() == var.VarType)
        //        //        {
        //        //            row1var1 =  " Var1：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量二
        //        //    if (var.VarNum == 2)
        //        //    {
        //        //        if (row[3].ToString() == var.VarType)
        //        //        {
        //        //            row1var2 = "Var2：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量三
        //        //    if (var.VarNum == 3)
        //        //    {
        //        //        if (row[6].ToString() == var.VarType)
        //        //        {
        //        //            row1var3 = "Var3：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量四
        //        //    if (var.VarNum == 4)
        //        //    {
        //        //        if (row[9].ToString() == var.VarType)
        //        //        {
        //        //            row1var4 = "Var4：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量五
        //        //    if (var.VarNum == 5)
        //        //    {
        //        //        if (row[12].ToString() == var.VarType)
        //        //        {
        //        //            row1var5 = "Var5：" + var.VarContent;
        //        //        }
        //        //    }
        //        //}
        //        //// row 2
        //        //if (2 == var.RowNum)
        //        //{
        //        //    ushort[] row = CM.ReadInputRegisters((ushort)(250), 50);
        //        //    // 变量一
        //        //    if (var.VarNum == 1)
        //        //    {
        //        //        if (row[0].ToString() == var.VarType)
        //        //        {
        //        //            row2var1 =  "Var1：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量二
        //        //    if (var.VarNum == 2)
        //        //    {
        //        //        if (row[3].ToString() == var.VarType)
        //        //        {
        //        //            row2var2 =  "Var2：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量三
        //        //    if (var.VarNum == 3)
        //        //    {
        //        //        if (row[6].ToString() == var.VarType)
        //        //        {
        //        //            row2var3 = "Var3：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量四
        //        //    if (var.VarNum == 4)
        //        //    {
        //        //        if (row[9].ToString() == var.VarType)
        //        //        {
        //        //            row2var4 = "Var4：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量五
        //        //    if (var.VarNum == 5)
        //        //    {
        //        //        if (row[12].ToString() == var.VarType)
        //        //        {
        //        //            row2var5 ="Var5：" + var.VarContent;
        //        //        }
        //        //    }
        //        //}
        //        ///// row 3
        //        //if (3 == var.RowNum)
        //        //{
        //        //    ushort[] row = CM.ReadInputRegisters((ushort)(300), 50);
        //        //    // 变量一
        //        //    if (var.VarNum == 1)
        //        //    {
        //        //        if (row[0].ToString() == var.VarType)
        //        //        {
        //        //            row3var1 =  "Var1：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量二
        //        //    if (var.VarNum == 2)
        //        //    {
        //        //        if (row[3].ToString() == var.VarType)
        //        //        {
        //        //            row3var2 ="Var2：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量三
        //        //    if (var.VarNum == 3)
        //        //    {
        //        //        if (row[6].ToString() == var.VarType)
        //        //        {
        //        //            row3var3 =  "Var3：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量四
        //        //    if (var.VarNum == 4)
        //        //    {
        //        //        if (row[9].ToString() == var.VarType)
        //        //        {
        //        //            row3var4 = "Var4：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量五
        //        //    if (var.VarNum == 5)
        //        //    {
        //        //        if (row[12].ToString() == var.VarType)
        //        //        {
        //        //            row1var5 = "Var5：" + var.VarContent;
        //        //        }
        //        //    }
        //        //}
        //        //// row 4
        //        //if (4 == var.RowNum)
        //        //{
        //        //    ushort[] row = CM.ReadInputRegisters((ushort)(350), 50);
        //        //    // 变量一
        //        //    if (var.VarNum == 1)
        //        //    {
        //        //        if (row[0].ToString() == var.VarType)
        //        //        {
        //        //            row4var1 = "Var1：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量二
        //        //    if (var.VarNum == 2)
        //        //    {
        //        //        if (row[3].ToString() == var.VarType)
        //        //        {
        //        //            row4var2 = "Var2：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量三
        //        //    if (var.VarNum == 3)
        //        //    {
        //        //        if (row[6].ToString() == var.VarType)
        //        //        {
        //        //            row4var3 = "Var3：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量四
        //        //    if (var.VarNum == 4)
        //        //    {
        //        //        if (row[9].ToString() == var.VarType)
        //        //        {
        //        //            row4var4 = "Var4：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量五
        //        //    if (var.VarNum == 5)
        //        //    {
        //        //        if (row[12].ToString() == var.VarType)
        //        //        {
        //        //            row4var5 = "Var5：" + var.VarContent;
        //        //        }
        //        //    }
        //        //}
        //        //// row 5
        //        //if (5 == var.RowNum)
        //        //{
        //        //    ushort[] row = CM.ReadInputRegisters((ushort)(450), 50);
        //        //    // 变量一
        //        //    if (var.VarNum == 1)
        //        //    {
        //        //        if (row[0].ToString() == var.VarType)
        //        //        {
        //        //            row5var1 = "Var1：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量二
        //        //    if (var.VarNum == 2)
        //        //    {
        //        //        if (row[3].ToString() == var.VarType)
        //        //        {
        //        //            row5var2 =  "Var2：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量三
        //        //    if (var.VarNum == 3)
        //        //    {
        //        //        if (row[6].ToString() == var.VarType)
        //        //        {
        //        //            row5var3 = "Var3：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量四
        //        //    if (var.VarNum == 4)
        //        //    {
        //        //        if (row[9].ToString() == var.VarType)
        //        //        {
        //        //            row5var4 = "Var4：" + var.VarContent;
        //        //        }
        //        //    }
        //        //    // 变量五
        //        //    if (var.VarNum == 5)
        //        //    {
        //        //        if (row[12].ToString() == var.VarType)
        //        //        {
        //        //            row5var5 ="Var5：" + var.VarContent;
        //        //        }
        //        //    }
        //        //}
             
        //    }
        //    //ushort[] fontfamily = CM.ReadInputRegisters((ushort)(218 + (1 - 1) * 50), 20);
        //    //comboBoxFontFamily.Text = fontfamily.ToProfaceString();
        //    //ushort[] fontsize_rowspacing = CM.ReadInputRegisters((ushort)(216 + (1 - 1) * 50), 2);
        //    //comboBoxFontSize.Text = fontsize_rowspacing[0].ToString();
        //    //textBoxRowSpace.Text = ((short)fontsize_rowspacing[1]).ToString();

        //    /////  TODO

        //    //currentTemplate.rowVar[1 - 1].Font = fontfamily;
        //    //currentTemplate.rowVar[1 - 1].FontSize = fontsize_rowspacing[0];
        //    //currentTemplate.rowVar[1 - 1].RowSpacing = fontsize_rowspacing[1];

        //    ////行1内容
        //    //string[] row1 = new string[] { row1var1, row1var2, row1var3, row1var4, row1var5};
        //    //string[] row2 = new string[] { row2var1, row2var2, row2var3, row2var4, row2var5 };
        //    //string[] row3 = new string[] {   row3var1, row3var2, row3var3, row3var4, row3var5 };
        //    //string[] row4 = new string[] { row4var1, row4var2, row4var3, row4var4, row4var5 };
        //    //string[] row5 = new string[] {  row5var1, row5var2, row5var3, row5var4, row5var5 };
        //    ////变量间距
        //    //ushort[] spacing1 = new ushort[] { 30, 30, 30, 30, 30 };
        //    //ushort[] spacing2 = new ushort[] { 30, 30, 30, 30, 30 };
        //    //ushort[] spacing3 = new ushort[] { 30, 30, 30, 30, 30 };
        //    //ushort[] spacing4 = new ushort[] { 30, 30, 30, 30, 30 };
        //    //ushort[] spacing5 = new ushort[] { 30, 30, 30, 30, 30 };
        //    //

        //    //List<string[]> rowlist = new List<string[]>();
        //    //rowlist.Add(row1);
        //    //rowlist.Add(row2);
        //    //rowlist.Add(row3);
        //    //rowlist.Add(row4);
        //    //rowlist.Add(row5);

        //    //List<ushort[]> spacingList = new List<ushort[]>();
        //    //spacingList.Add(spacing1);
        //    //spacingList.Add(spacing2);
        //    //spacingList.Add(spacing3);
        //    //spacingList.Add(spacing4);
        //    //spacingList.Add(spacing5);

        //    //行间距
        //    //ushort[] rowSpacing = new ushort[] { 30, 30, 30, 30, 30 };

        //    //Bitmap bmp = new Bitmap(800,500);
        //    //Graphics g = Graphics.FromImage(bmp);
        //    //Font drawFont = new Font("Simple Straight 3", 16);
        //    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //    //SolidBrush drawFontBrush = new SolidBrush(Color.Black);
        // //   SolidBrush drawLineBrush = new SolidBrush(Color.Gray);

        //    //float yp = 0;
        //    //float rp = 0;
        //    //for (int ri = 0; ri < rowlist.Count; ri++)
        //    //{
        //    //    float tp = 0;
        //    //    for (int i = 0; i < rowlist[ri].Length; i++)
        //    //    {
        //    //        g.DrawString(rowlist[ri][i], drawFont, drawFontBrush, new PointF(tp, yp));
        //    //        g.DrawString(rowlist[ri][i], drawFont, drawFontBrush, new PointF(tp, yp));
        //    //        g.DrawString(rowlist[ri][i], drawFont, drawFontBrush, new PointF(tp, yp));
        //    //        g.DrawString(rowlist[ri][i], drawFont, drawFontBrush, new PointF(tp, yp));
        //    //        g.DrawString(rowlist[ri][i], drawFont, drawFontBrush, new PointF(tp, yp));
        //    //        g.DrawString(rowlist[ri][i], drawFont, drawFontBrush, new PointF(tp, yp));
        //    //        g.DrawString(rowlist[ri][i], drawFont, drawFontBrush, new PointF(tp, yp));
        //    //        g.DrawString(rowlist[ri][i], drawFont, drawFontBrush, new PointF(tp, yp));
        //    //        SizeF sizef = g.MeasureString(rowlist[ri][i], drawFont);

        //    //        //if (i < rowlist[ri].Length - 1)
        //    //        //{
        //    //        //    g.DrawLine(new Pen(drawLineBrush), tp + sizef.Width, yp, tp + sizef.Width, sizef.Height + yp);
        //    //        //}
        //    //        //if (i > 0)
        //    //        //{
        //    //        //    g.DrawLine(new Pen(drawLineBrush), tp, yp, tp, sizef.Height + yp);
        //    //        //}
        //    //        rp = sizef.Height;
        //    //        tp +=  sizef.Width+spacingList[ri][i];
        //    //    }
                
        //    //    yp+=rp+rowSpacing[ri];
        //    //}

        //    //g.Dispose();
        //  //  pictureBox1.Image = bmp;
        //}

        // 匹配字体                                                                                                           


        private String fontEqual(String font)
        {
            System.Drawing.Text.InstalledFontCollection ifc = new System.Drawing.Text.InstalledFontCollection();
            FontFamily[] ffs = ifc.Families;
            foreach (FontFamily ff in ffs)
            {
                if (ff.Name.Equals(font))
                {
                    return ff.Name.ToString();
                }
            }
            return "没有匹配到相同字体";
        }

        /// <summary>
        ///  消息comboBox重写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxLogs_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = e.Bounds;
           
            //  Size imageSize = imageList1.ImageSize; 
            if (e.Index >= 0)
            {
                Font fn = new Font("Microsoft YaHei UI", 25);
                string s = (string)comboBoxLogs.Items[e.Index];
             //   comboBoxLogs.SelectedIndex = e.Index;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                if (e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect))
                {
                    //画条目背景 
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r);
                    //绘制图像 
                    //  imageList1.Draw(e.Graphics, r.Left, r.Top, e.Index); 
                   // e.Graphics.DrawRectangle(new Pen(Color.Red), r.Left, r.Top, 20, 20);
                    if (s.Split(' ')[0].Equals("信息"))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Green), r.Left, r.Top, 30, 30);
                    }
                    if (s.Split(' ')[0].Equals("错误"))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Red), r.Left, r.Top, 30, 30);
                    }
                    if (s.Split(' ')[0].Equals("警告"))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), r.Left, r.Top, 30, 30);
                    }
                    e.Graphics.DrawString(s, fn, new SolidBrush(Color.Black), r.Left + 30, r.Top);
                    //显示取得焦点时的虚线框 
                    e.DrawFocusRectangle();
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), r);
                    if (s.Split(' ')[0].Equals("信息"))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Green), r.Left, r.Top, 30, 30);
                    }
                    if (s.Split(' ')[0].Equals("错误"))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Red), r.Left, r.Top, 30, 30);
                    }
                    if (s.Split(' ')[0].Equals("警告"))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), r.Left, r.Top, 30, 30);
                    }
                    e.Graphics.DrawString(s, fn, new SolidBrush(Color.Black), r.Left + 30, r.Top);
                    e.DrawFocusRectangle();
                }
            }
        }

        /// <summary>
        /// 生成列选择
        /// </summary>
        /// <param name="index"></param>
        public void GenerateColumn(int index)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters(109, new ushort[] { (ushort)index });
            ushort currentcount = CM.ExecutionCount;
            CM.WriteSingleCoil(17, true);
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
               
            }
        }

        public void GenerateColumnColor(int index)
        {
            if (index == 2)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 3)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 4)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 5)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
                line5.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 6)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
                line5.BackColor = System.Drawing.Color.Turquoise;
                line6.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 7)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
                line5.BackColor = System.Drawing.Color.Turquoise;
                line6.BackColor = System.Drawing.Color.Turquoise;
                line7.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 8)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
                line5.BackColor = System.Drawing.Color.Turquoise;
                line6.BackColor = System.Drawing.Color.Turquoise;
                line7.BackColor = System.Drawing.Color.Turquoise;
                line8.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 9)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
                line5.BackColor = System.Drawing.Color.Turquoise;
                line6.BackColor = System.Drawing.Color.Turquoise;
                line7.BackColor = System.Drawing.Color.Turquoise;
                line8.BackColor = System.Drawing.Color.Turquoise;
                line9.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 10)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
                line5.BackColor = System.Drawing.Color.Turquoise;
                line6.BackColor = System.Drawing.Color.Turquoise;
                line7.BackColor = System.Drawing.Color.Turquoise;
                line8.BackColor = System.Drawing.Color.Turquoise;
                line9.BackColor = System.Drawing.Color.Turquoise;
                line10.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 11)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
                line5.BackColor = System.Drawing.Color.Turquoise;
                line6.BackColor = System.Drawing.Color.Turquoise;
                line7.BackColor = System.Drawing.Color.Turquoise;
                line8.BackColor = System.Drawing.Color.Turquoise;
                line9.BackColor = System.Drawing.Color.Turquoise;
                line10.BackColor = System.Drawing.Color.Turquoise;
                line11.BackColor = System.Drawing.Color.Turquoise;
            }
            else if (index == 12)
            {
                line1.BackColor = System.Drawing.Color.Turquoise;
                line2.BackColor = System.Drawing.Color.Turquoise;
                line3.BackColor = System.Drawing.Color.Turquoise;
                line4.BackColor = System.Drawing.Color.Turquoise;
                line5.BackColor = System.Drawing.Color.Turquoise;
                line6.BackColor = System.Drawing.Color.Turquoise;
                line7.BackColor = System.Drawing.Color.Turquoise;
                line8.BackColor = System.Drawing.Color.Turquoise;
                line9.BackColor = System.Drawing.Color.Turquoise;
                line10.BackColor = System.Drawing.Color.Turquoise;
                line11.BackColor = System.Drawing.Color.Turquoise;
                line12.BackColor = System.Drawing.Color.Turquoise;
            }  
        }
        /// <summary>
        /// 选择列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        short currentColumnX;
        short currentColumnY;
        short currentColumnTwoX;
        short currentColumnTwoY;
        short currentColumnAngle;
        public void SelectColumn(int index)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters(109, new ushort[] { (ushort)index });
            ushort currentcount = CM.ExecutionCount;
            CM.WriteSingleCoil(17, true);
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                ushort[] arg = CM.ReadInputRegisters(180, 4);
                currentColumnX = (short)arg[0];
                currentColumnY = (short)arg[1];
                currentColumnTwoX = (short)arg[2];
                currentColumnTwoY = (short) arg[3];

                comboBoxLogs.Items.Insert(0, logs.showInfo("当前选择的列号是 ： 第" + index + "列！"));
                comboBoxLogs.SelectedIndex = 0;
            }
            else
            {
                comboBoxLogs.Items.Insert(0, logs.showError("选择失败！"));
                comboBoxLogs.SelectedIndex = 0;
            }
        }

        #region 列选择

        int lastSelectColumn;
        private void selectLine1_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 1 ;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();        
            selectLine1.BackColor = System.Drawing.Color.Turquoise;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;


            selectLine1.Text = "OK";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择"; 
            selectLine7.Text = "选择"; 
            selectLine8.Text = "选择"; 
            selectLine9.Text = "选择"; 
            selectLine10.Text = "选择"; 
            selectLine11.Text = "选择"; 
            selectLine12.Text = "选择"; 
        }

        private void selectLine2_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 2;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Turquoise;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;


            selectLine1.Text = "选择";
            selectLine2.Text = "OK";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine3_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 3;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Turquoise;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;

            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "OK";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine4_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 4;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Turquoise;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;
            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "OK";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine5_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 5;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Turquoise;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;

            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "OK";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine6_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 6;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Turquoise;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;
            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "OK";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine7_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 7;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Turquoise;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;

            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "OK";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine8_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 8;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Turquoise;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;

            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "OK";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine9_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 9;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Turquoise;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;
            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "OK";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine10_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 10;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Turquoise;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Transparent;

            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "OK";
            selectLine11.Text = "选择";
            selectLine12.Text = "选择"; 
        }

        private void selectLine11_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 11;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Turquoise;
            selectLine12.BackColor = System.Drawing.Color.Transparent;

            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "OK";
            selectLine12.Text = "选择"; 
        }

        private void selectLine12_Click(object sender, EventArgs e)
        {
            lastSelectColumn = 12;
            SelectColumn(lastSelectColumn);
            lineX.Text = currentColumnX.ToString();
            lineY.Text = currentColumnY.ToString();
            twoLineX.Text = currentColumnTwoX.ToString();
            twoLineY.Text = currentColumnTwoY.ToString();

            selectLine1.BackColor = System.Drawing.Color.Transparent;
            selectLine2.BackColor = System.Drawing.Color.Transparent;
            selectLine3.BackColor = System.Drawing.Color.Transparent;
            selectLine4.BackColor = System.Drawing.Color.Transparent;
            selectLine5.BackColor = System.Drawing.Color.Transparent;
            selectLine6.BackColor = System.Drawing.Color.Transparent;
            selectLine7.BackColor = System.Drawing.Color.Transparent;
            selectLine8.BackColor = System.Drawing.Color.Transparent;
            selectLine9.BackColor = System.Drawing.Color.Transparent;
            selectLine10.BackColor = System.Drawing.Color.Transparent;
            selectLine11.BackColor = System.Drawing.Color.Transparent;
            selectLine12.BackColor = System.Drawing.Color.Turquoise;

            selectLine1.Text = "选择";
            selectLine2.Text = "选择";
            selectLine3.Text = "选择";
            selectLine4.Text = "选择";
            selectLine5.Text = "选择";
            selectLine6.Text = "选择";
            selectLine7.Text = "选择";
            selectLine8.Text = "选择";
            selectLine9.Text = "选择";
            selectLine10.Text = "选择";
            selectLine11.Text = "选择";
            selectLine12.Text = "OK"; 
        }
        #endregion

        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteLine_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters(110, new ushort[]{
               (ushort) 0, (ushort) 0
            });

            CM.WriteSingleCoil(6, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result = CM.TemplateStatus;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                if (result == 1)
                {
                    lineX.Text = "0";
                    lineY.Text = "0";
                    comboBoxLogs.Items.Insert(0, logs.showInfo("删除列" + lastSelectColumn + " 成功！"));
                    comboBoxLogs.SelectedIndex = 0;
                    if (lastSelectColumn == 1)
                    {
                        line1.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 2)
                    {
                        line2.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 3)
                    {
                        line3.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 4)
                    {
                        line4.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 5)
                    {
                        line5.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 6)
                    {
                        line6.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 7)
                    {
                        line7.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 8)
                    {
                        line8.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 9)
                    {
                        line9.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 10)
                    {
                        line10.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 11)
                    {
                        line11.BackColor = System.Drawing.Color.Transparent;
                    }
                    if (lastSelectColumn == 12)
                    {
                        line12.BackColor = System.Drawing.Color.Transparent;
                    }
                }
                else
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("删除列" + lastSelectColumn + " 失败！"));
                    comboBoxLogs.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 生成列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateLine_Click(object sender, EventArgs e)
        {
            GenerateColumn gc = new GenerateColumn();
            gc.Owner = this;
            gc.StartPosition = FormStartPosition.CenterParent;
            gc.ShowDialog();
        }
       
        /// <summary>
        /// 设置列的x/y坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setColumn_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
              
             short x = short.Parse(lineX.Text);
             short y = short.Parse(lineY.Text);
             if (x.ToString() == "" || y.ToString() == "")
             {
                 comboBoxLogs.Items.Insert(0, logs.showError("x,y不能为空值！"));
                 comboBoxLogs.SelectedIndex = 0;
                 return;
             }
            CM.WriteMultipleRegisters(110, new ushort[]{
               (ushort) x, (ushort) y
            });

            ushort currentcount = CM.ExecutionCount;
            CM.WriteSingleCoil(6, true);
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                comboBoxLogs.Items.Insert(0, logs.showInfo("设置成功！"));
                comboBoxLogs.SelectedIndex = 0;
                if ((x != 0 || y !=0) && lastSelectColumn == 1)
                {
                    line1.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 2)
                {
                    line2.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 3)
                {
                    line3.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 4)
                {
                    line4.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 5)
                {
                    line5.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 6)
                {
                    line6.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 7)
                {
                    line7.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 8)
                {
                    line8.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 9)
                {
                    line9.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 10)
                {
                    line10.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 11)
                {
                    line11.BackColor = System.Drawing.Color.Turquoise;
                }
                if ((x != 0 || y != 0) && lastSelectColumn == 12)
                {
                    line12.BackColor = System.Drawing.Color.Turquoise;
                }
            }
            else
            {
                comboBoxLogs.Items.Insert(0, logs.showError("设置失败！"));
                comboBoxLogs.SelectedIndex = 0;  
            }       
        }

        /// <summary>
        /// 设置二头列的坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setTwoColumn_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            short x = short.Parse(twoLineX.Text);
            short y = short.Parse(twoLineY.Text);
            CM.WriteMultipleRegisters(110, new ushort[]{
               (ushort)x, (ushort)y
            });

            ushort currentcount = CM.ExecutionCount;
            CM.WriteSingleCoil(9, true);
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                comboBoxLogs.Items.Insert(0, logs.showInfo("设置成功！"));
                comboBoxLogs.SelectedIndex = 0;
            }
            else
            {
                comboBoxLogs.Items.Insert(0, logs.showError("设置失败！"));
                comboBoxLogs.SelectedIndex = 0;
            }       
        }
        
        /// <summary>
        /// 设置角度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setAngle_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            short angle = short.Parse(spanAngle.Text);
            CM.WriteMultipleRegisters(485, new ushort[]{(ushort)angle});

            ushort currentcount = CM.ExecutionCount;
            CM.WriteSingleCoil(11, true);
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                comboBoxLogs.Items.Insert(0, logs.showInfo("设置角度成功！"));
                comboBoxLogs.SelectedIndex = 0;
            }
            else
            {
                comboBoxLogs.Items.Insert(0, logs.showError("设置角度失败！"));
                comboBoxLogs.SelectedIndex = 0;
            }
        }

        public String inputResult;
       
        private void spacing1v2_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            spacing1v2.Text = inputResult;
        }

        private void spacing2v3_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            spacing2v3.Text = inputResult;
        }

        private void spacing3v4_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            spacing3v4.Text = inputResult;
        }

        private void spacing4v5_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            spacing4v5.Text = inputResult;
        }

        private void lineX_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            lineX.Text = inputResult;
        }

        private void lineY_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            lineY.Text = inputResult;
        }

        private void twoLineX_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            twoLineX.Text = inputResult;
        }

        private void twoLineY_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            twoLineY.Text = inputResult;
        }

        private void spanAngle_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            spanAngle.Text = inputResult;
        }

        private void IPTextbox_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            IPTextbox.Text = inputResult;
        }

        private void comboBoxFontSize_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            comboBoxFontSize.Text = inputResult;
        }

        private void textBoxRowSpace_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            textBoxRowSpace.Text = inputResult;
        }

        private void tabControlNav_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            switch (this.tabControlNav.SelectedIndex)
            {
                case 0:
                    listView1.Items[0].Selected = true;
                    break;
                case 1:
                    if (CM.Connected)
                    {
                        this.tabControlNav.SelectedTab = tabPage2;
                        //MessageBox.Show("tabPage2 is Selected");
                    }
                    break;
                case 2:
                    this.tabControlNav.SelectedTab = tabPage3;
                //    MessageBox.Show("tabPage2 is Selected");
                    break;
            }
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!this.exitProgram.Visible)
            {
                this.exitProgram.Visible = true;
            }
            else
            {
                this.exitProgram.Visible = false;
            }

        }

        /// <summary>
        /// 更新模板名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateTempName_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters(1, nameOfTemp.Text.ToProfaceUshort(19));
            CM.WriteSingleCoil(3, true);
            ushort result = CM.TemplateStatus;
            ushort currentcount = CM.ExecutionCount;

            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                if (result == 1)
                {
                    UpdateTemplateListData();
                    showTemplatesList();
                    comboBoxLogs.Items.Insert(0, logs.showInfo("更新模板名成功！"));
                    comboBoxLogs.SelectedIndex = 0;
                }
                else
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("更新模板名失败!错误码为" + result));
                    comboBoxLogs.SelectedIndex = 0;
                }        
            }
        }

        private void tabControlNav_SelectedIndexChanged_1(object sender, EventArgs e)
        {
     
            if (tabControlNav.SelectedIndex == 2)
            {
                ModbusManager CM = masterWay.CM;
                if (CM.Connected)
                {
                    ushort[] param = CM.ReadInputRegisters((ushort)(50000), 6);
                    if (param[0] == 2)
                    {
                        interLockSwitch.Text = "断 开";
                    }
                    else
                    {
                        interLockSwitch.Text = "正 常";
                    }
                    if (param[1] == 2)
                    {
                        USBconnectState.Text = "断 开";
                    }
                    else
                    {
                        USBconnectState.Text = "正 常";
                    }
                    if (param[2] == 2)
                    {
                        controlCard.Text = "正 常";
                    }
                    else
                    {
                        controlCard.Text = "急 停";
                    }
                    if (param[3] == 1)
                    {
                        mirror.Text = "报 错";
                    }
                    else
                    {
                        mirror.Text = "正 常";
                    }

                    ushort[] co2 = CM.ReadInputRegisters((ushort)50029, 4);
                    if (co2[0] == 1)
                    {
                        modbusTcp.Text = "开";
                    }
                    else
                    {
                        modbusTcp.Text = "关";
                    }
                    if (co2[1] == 1)
                    {
                        modbusRtu.Text = "开";
                    }
                    else
                    {
                        modbusRtu.Text = "关";
                    }
                    if (co2[2] == 1)
                    {
                        co21.Text = "开";
                    }
                    else
                    {
                        co21.Text = "关";
                    }
                    laser.Text = ((YLPStatus)co2[3]).ToString();

                    ushort[] systemTime = CM.ReadInputRegisters((ushort)(50006), 6);
                    string time = systemTime[0].ToString() + "/" + systemTime[1].ToString() + "/" + systemTime[2].ToString() + " " +
                        systemTime[3].ToString() + ":" + systemTime[4].ToString() + ":" + systemTime[5].ToString();
                    systemTime1.Text = time;

                    ushort[] lightTime = CM.ReadInputRegisters((ushort)(50012), 4);
                    string ltime = lightTime[0].ToString() + "天" + lightTime[1].ToString() + ":" + lightTime[0].ToString() + ":" + lightTime[0].ToString();
                    timeOflight.Text = ltime;

                    ushort[] code = CM.ReadInputRegisters((ushort)(50018), 1);
                    label38.Text = code[0].ToString();
                }
                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteMultipleRegisters(476, new ushort[]{
                tbPower.Text.ToNullableIntUShort(),
                tbPrintSpeed.Text.ToNullableIntUShort(),            
            });
            CM.WriteMultipleRegisters(484, new ushort[]{
                 tbFrequency.Text.ToNullableIntUShort()
            });
            CM.WriteSingleCoil(10, true);
            ushort currentcount = CM.ExecutionCount;
            ushort result;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
                if (result == 1)
                {
                    comboBoxLogs.Items.Insert(0, logs.showInfo("跟新参数成功！"));
                    comboBoxLogs.SelectedIndex = 0;
                }
                else
                {
                    comboBoxLogs.Items.Insert(0, logs.showError("跟新参数失败!错误码为" + result));
                    comboBoxLogs.SelectedIndex = 0;
                }
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            if (tabControl2.SelectedIndex == 0)
            {
                ushort[] param = CM.ReadInputRegisters((ushort)(50000), 6);
                if (param[0] == 2)
                {
                    interLockSwitch.Text = "断 开";
                }
                else
                {
                    interLockSwitch.Text = "正 常";
                }
                if (param[1] == 2)
                {
                    USBconnectState.Text = "断 开";
                }
                else
                {
                    USBconnectState.Text = "正 常";
                }
                if (param[2] == 2)
                {
                    controlCard.Text = "正 常";
                }
                else
                {
                    controlCard.Text = "急 停";
                }
                if (param[3] == 1)
                {
                    mirror.Text = "报 错";
                }
                else
                {
                    mirror.Text = "正 常";
                }
                ushort[] co2 = CM.ReadInputRegisters((ushort)50029, 4);
                if (co2[0] == 1)
                {
                    modbusTcp.Text = "开";
                }
                else
                {
                    modbusTcp.Text = "关";
                }
                if (co2[1] == 1)
                {
                    modbusRtu.Text = "开";
                }
                else
                {
                    modbusRtu.Text = "关";
                }
                if (co2[2] == 1)
                {
                    co21.Text = "开";
                }
                else
                {
                    co21.Text = "关";
                }
                laser.Text = ((YLPStatus)co2[3]).ToString();

                ushort[] systemTime = CM.ReadInputRegisters((ushort)(50006), 6);
                string time = systemTime[0].ToString() + "/" + systemTime[1].ToString() + "/" + systemTime[2].ToString() + " " +
                    systemTime[3].ToString() + ":" + systemTime[4].ToString() + ":" + systemTime[5].ToString();
                systemTime1.Text = time;

                ushort[] lightTime = CM.ReadInputRegisters((ushort)(50012), 4);
                string ltime = lightTime[0].ToString() + "天" + lightTime[1].ToString() + ":" + lightTime[0].ToString() + ":" + lightTime[0].ToString();
                timeOflight.Text = ltime;

                ushort[] code = CM.ReadInputRegisters((ushort)(50018), 1);
                label38.Text = code[0].ToString();


                //ushort[] executeCount = CM.ReadInputRegisters((ushort)(50023), 2);
                //systemParam.Add("命令执行次数", executeCount[0].ToString());
                //systemParam.Add("命令正在执行", executeCount[1].ToString());

                //ushort[] codeResult = CM.ReadInputRegisters((ushort)(50025), 3);
                //systemParam.Add("打码触发结果", codeResult[0].ToString());
                //systemParam.Add("打码执行结果", codeResult[1].ToString());
                //systemParam.Add("打码次数总和", codeResult[2].ToString());

                //ushort[] laserStatus = CM.ReadInputRegisters((ushort)(50028), 5);
                //systemParam.Add("打印延迟", laserStatus[0].ToString());    
            }
            if (tabControl2.SelectedIndex == 1)
            {
                ushort[] result = CM.ReadInputRegisters(50033, 15);
                if (result[0] == 2)
                {
                    button14.Text = "丢 失";
                }
                else
                {
                    button14.Text = "正 常";
                }
                if (result[1] == 2)
                {
                    button17.Text = "关";
                }
                else
                {
                    button17.Text = "开";
                }
                if (result[2] == 2)
                {
                    button18.Text = "关";
                }
                else
                {
                    button18.Text = "开";
                }
                if (result[3] == 2)
                {
                    button16.Text = "关";
                }
                else
                {
                    button16.Text = "开";
                }
                label55.Text = result[4].ToString();
                label57.Text = result[5].ToString();
                label54.Text = result[6].ToNullableDouble().ToString();
                label56.Text = result[7].ToNullableDouble().ToString();
                //label55.Text = result[8].ToString();
                label58.Text = result[9].ToNullableDouble().ToString();
                label59.Text = result[10].ToNullableDouble().ToString();
                label60.Text = result[11].ToNullableDouble().ToString();
                label61.Text = result[12].ToNullableDouble().ToString();

                if (result[13] == 2)
                {
                    button15.Text = "关";
                }
                else
                {
                    button15.Text = "开";
                }
                if (result[14] == 2)
                {
                    button21.Text = "正 常";
                }
                else
                {
                    button21.Text = "报 警";
                }

                ushort[] resultOfWarn = CM.ReadInputRegisters(50048, 50);
                label33.Text = resultOfWarn.ToProfaceString();
            }
            if (tabControl2.SelectedIndex == 2)
            {
                ushort[] result = CM.ReadInputRegisters(50200, 14);
                label79.Text = result[0].ToNullableDouble().ToString();
                label80.Text = result[1].ToNullableDouble().ToString();
                label81.Text = result[2].ToNullableDouble().ToString();
                if (result[3] == 1)
                {
                    button1.Text = " 手 动";
                }
                else
                {
                    button1.Text = "自 动";
                }
                if (result[4] == 1)
                {
                    button27.Text = " 开";
                }
                else
                {
                    button27.Text = "关";
                }
                if (result[5] == 1)
                {
                    button28.Text = " 开";
                }
                else
                {
                    button28.Text = "关";
                }
                if (result[6] == 1)
                {
                    button29.Text = " 开";
                }
                else
                {
                    button29.Text = "关";
                }
                // ----
                if (result[7] == 1)
                {
                    button30.Text = " 开";
                }
                else
                {
                    button30.Text = "关";
                }
                if (result[9] == 1)
                {
                    button31.Text = " 开";
                }
                else
                {
                    button31.Text = "关";
                }
                if (result[10] == 1)
                {
                    button32.Text = " 开";
                }
                else
                {
                    button32.Text = "关";
                }
                //
                ushort[] resultShui = CM.ReadInputRegisters(50210, 4);
                if (resultShui[0] == 1)
                {
                    button23.Text = " 开";
                }
                else
                {
                    button23.Text = "关";
                } if (resultShui[1] == 1)
                {
                    button24.Text = " 开";
                }
                else
                {
                    button24.Text = "关";
                } if (resultShui[2] == 1)
                {
                    button25.Text = " 开";
                }
                else
                {
                    button25.Text = "关";
                } if (resultShui[3] == 1)
                {
                    button26.Text = " 开";
                }
                else
                {
                    button26.Text = "关";
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteSingleCoil(50001, true);
            ushort result;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteSingleCoil(50002, true);
            ushort result;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteSingleCoil(50005, true);
            ushort result;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label37.Text = "0";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteSingleCoil(50003, true);
            ushort result;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteSingleCoil(50014, true);
            ushort result;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteSingleCoil(50011, true);
            ushort result;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteSingleCoil(50012, true);
            ushort result;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            CM.WriteSingleCoil(50013, true);
            ushort result;
            ushort currentcount = CM.ExecutionCount;
            if (SpinWait.SpinUntil(() => CM.ExecutionCount == currentcount + 1, 5000))
            {
                result = CM.TemplateStatus;
            }
        }

        /// <summary>
        /// 偏移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3)
            {
                ModbusManager CM = masterWay.CM;
                ushort[] offresult = CM.ReadInputRegisters(474, 2);
                currentX = (short)offresult[0];
                currentY = (short)offresult[1];
                tbOff_X.Text = offresult[0].ToNullableDouble().ToString();
                tbOff_Y.Text = offresult[1].ToNullableDouble().ToString();
            }

        }

        short currentX;
        short currentY;
        short space;
        private void set(short x, short y)
        {       
            ModbusManager CM = masterWay.CM;
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
                    ushort[] XY = CM.ReadInputRegisters(474, 2);
                    currentX +=(short)XY[0];
                    currentY += (short)XY[1];
                    tbOff_X.Text = currentX.ToString();
                    tbOff_Y.Text = currentY.ToString();
                    showInfoLog("设置成功");
                }
                else
                {
                    showErrorLog("设置失败！错误码为" + result);
                }
            }
        }

        private void OffsetUp_Click(object sender, EventArgs e)
        {
            ModbusManager CM = masterWay.CM;
            if (stepSapce.Text == "")
            {
                space = 0;
            }
            else
            {
                space = Convert.ToInt16(stepSapce.Text);
            }  
            short x = CM.OffsetX;
            short y = CM.OffsetY;
            y += (short)space ;
            set (x, y );
        }

        private void OffsetRight_Click(object sender, EventArgs e)
        {
            if (stepSapce.Text == "")
            {
                space = 0;
            }
            else
            {
                space = Convert.ToInt16(stepSapce.Text);
            }  
            ModbusManager CM = masterWay.CM;
            short x = CM.OffsetX;
            short y = CM.OffsetY;
            x += (short)space;
            set(x, y);
        }

        private void Offsetleft_Click(object sender, EventArgs e)
        {
            if (stepSapce.Text == "")
            {
                space = 0;
            }
            else
            {
                space = Convert.ToInt16(stepSapce.Text);
            }  
            ModbusManager CM = masterWay.CM;
         
            short x = CM.OffsetX;
            short y = CM.OffsetY;
            x -= space;
            set(x, y);
        }

        private void Offsetbottom_Click(object sender, EventArgs e)
        {
            if (stepSapce.Text == "")
            {
                space = 0;
            }
            else
            {
                space = Convert.ToInt16(stepSapce.Text);
            }  
            ModbusManager CM = masterWay.CM;
           
            short x = CM.OffsetX;
            short y = CM.OffsetY;
            y -= space;
            set(x, y);
        }

        private void nameOfTemp_Click(object sender, EventArgs e)
        {
            keyboard numkey = new keyboard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            nameOfTemp.Text = inputResult;
        }

        private void tbPower_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            tbPower.Text = inputResult;
        }

        private void tbPrintSpeed_Click(object sender, EventArgs e)
        {
            
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            tbPrintSpeed.Text = inputResult;
            
        }

        private void tbFrequency_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            tbFrequency.Text = inputResult;
        }

        private void stepSapce_Click(object sender, EventArgs e)
        {
            NumOfKeyBoard numkey = new NumOfKeyBoard(homeSympol);
            numkey.Owner = this;
            numkey.StartPosition = FormStartPosition.CenterParent;
            numkey.ShowDialog();
            stepSapce.Text = inputResult;
        }


    }
}

