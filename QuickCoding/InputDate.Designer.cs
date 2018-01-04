namespace HDNing
{
    partial class InputDate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DbtnNew = new System.Windows.Forms.Button();
            this.DtxbName = new System.Windows.Forms.TextBox();
            this.DtxbDecollator = new System.Windows.Forms.TextBox();
            this.DtxbSpacing = new System.Windows.Forms.TextBox();
            this.DcheckWidth = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DtxbDateFormat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "分隔符：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 152);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 35);
            this.label3.TabIndex = 0;
            this.label3.Text = "日期格式：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 215);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 35);
            this.label4.TabIndex = 0;
            this.label4.Text = "字符间隔：";
            // 
            // DbtnNew
            // 
            this.DbtnNew.BackColor = System.Drawing.Color.LightGreen;
            this.DbtnNew.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F);
            this.DbtnNew.Location = new System.Drawing.Point(71, 329);
            this.DbtnNew.Margin = new System.Windows.Forms.Padding(8);
            this.DbtnNew.Name = "DbtnNew";
            this.DbtnNew.Size = new System.Drawing.Size(111, 49);
            this.DbtnNew.TabIndex = 1;
            this.DbtnNew.Text = "√";
            this.DbtnNew.UseVisualStyleBackColor = false;
            this.DbtnNew.Click += new System.EventHandler(this.DbtnNew_Click);
            // 
            // DtxbName
            // 
            this.DtxbName.Location = new System.Drawing.Point(150, 10);
            this.DtxbName.Margin = new System.Windows.Forms.Padding(8);
            this.DtxbName.Name = "DtxbName";
            this.DtxbName.Size = new System.Drawing.Size(260, 41);
            this.DtxbName.TabIndex = 2;
            this.DtxbName.Click += new System.EventHandler(this.DtxbName_Click);
            // 
            // DtxbDecollator
            // 
            this.DtxbDecollator.Location = new System.Drawing.Point(150, 73);
            this.DtxbDecollator.Margin = new System.Windows.Forms.Padding(8);
            this.DtxbDecollator.Name = "DtxbDecollator";
            this.DtxbDecollator.Size = new System.Drawing.Size(95, 41);
            this.DtxbDecollator.TabIndex = 2;
            this.DtxbDecollator.Text = "/";
            this.DtxbDecollator.Click += new System.EventHandler(this.DtxbDecollator_Click);
            // 
            // DtxbSpacing
            // 
            this.DtxbSpacing.Location = new System.Drawing.Point(153, 212);
            this.DtxbSpacing.Margin = new System.Windows.Forms.Padding(8);
            this.DtxbSpacing.Name = "DtxbSpacing";
            this.DtxbSpacing.Size = new System.Drawing.Size(95, 41);
            this.DtxbSpacing.TabIndex = 2;
            this.DtxbSpacing.Text = "1";
            this.DtxbSpacing.Click += new System.EventHandler(this.DtxbSpacing_Click);
            // 
            // DcheckWidth
            // 
            this.DcheckWidth.AutoSize = true;
            this.DcheckWidth.Location = new System.Drawing.Point(157, 290);
            this.DcheckWidth.Margin = new System.Windows.Forms.Padding(8);
            this.DcheckWidth.Name = "DcheckWidth";
            this.DcheckWidth.Size = new System.Drawing.Size(15, 14);
            this.DcheckWidth.TabIndex = 3;
            this.DcheckWidth.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 278);
            this.label5.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 35);
            this.label5.TabIndex = 0;
            this.label5.Text = "是否等宽：";
            // 
            // DtxbDateFormat
            // 
            this.DtxbDateFormat.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F);
            this.DtxbDateFormat.FormattingEnabled = true;
            this.DtxbDateFormat.Items.AddRange(new object[] {
            "年月日(两位)(补零)",
            "年月日(两位位)",
            "年月日(四位)(补零)",
            "年月日(四位)",
            "日月年(两位)(补零)",
            "日月年(两位)",
            "日月年(四位)(补零)",
            "日月年(四位)",
            "月日年(两位)(补零)",
            "月日年(两位)",
            "月日年(四位)(补零)",
            "月日年(四位)",
            "年月日(中文)(两位)(补零)",
            "年月日(中文)(两位)",
            "年月日(中文)(四位)(补零)",
            "年月日(中文)(四位)"});
            this.DtxbDateFormat.Location = new System.Drawing.Point(153, 152);
            this.DtxbDateFormat.Margin = new System.Windows.Forms.Padding(8);
            this.DtxbDateFormat.Name = "DtxbDateFormat";
            this.DtxbDateFormat.Size = new System.Drawing.Size(260, 35);
            this.DtxbDateFormat.TabIndex = 4;
            this.DtxbDateFormat.Text = "请选择...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F);
            this.label6.Location = new System.Drawing.Point(248, 228);
            this.label6.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "范围(0.5-2)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F);
            this.label7.Location = new System.Drawing.Point(248, 84);
            this.label7.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "类型(/ -)";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Pink;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F);
            this.button1.Location = new System.Drawing.Point(245, 329);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 49);
            this.button1.TabIndex = 1;
            this.button1.Text = "x";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.cancel_Click);
            // 
            // InputDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(426, 395);
            this.Controls.Add(this.DtxbDateFormat);
            this.Controls.Add(this.DcheckWidth);
            this.Controls.Add(this.DtxbSpacing);
            this.Controls.Add(this.DtxbDecollator);
            this.Controls.Add(this.DtxbName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DbtnNew);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "InputDate";
            this.Text = " 新建日期";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button DbtnNew;
        private System.Windows.Forms.TextBox DtxbName;
        private System.Windows.Forms.TextBox DtxbDecollator;
        private System.Windows.Forms.TextBox DtxbSpacing;
        private System.Windows.Forms.CheckBox DcheckWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox DtxbDateFormat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}