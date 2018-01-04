namespace HDNing
{
    partial class InputSerial
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.StxbName = new System.Windows.Forms.TextBox();
            this.StxbStart = new System.Windows.Forms.TextBox();
            this.StxbEnd = new System.Windows.Forms.TextBox();
            this.StxbAdd = new System.Windows.Forms.TextBox();
            this.StxbRepeat = new System.Windows.Forms.TextBox();
            this.StxbMin = new System.Windows.Forms.TextBox();
            this.StxbSpacing = new System.Windows.Forms.TextBox();
            this.SbtnNew = new System.Windows.Forms.Button();
            this.ScheckWidth = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 132);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "终止值：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 195);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 35);
            this.label3.TabIndex = 0;
            this.label3.Text = "增量：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 258);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 35);
            this.label4.TabIndex = 0;
            this.label4.Text = "重复次数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 322);
            this.label5.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 35);
            this.label5.TabIndex = 0;
            this.label5.Text = "最少位数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 35);
            this.label6.TabIndex = 0;
            this.label6.Text = "起始值：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 388);
            this.label7.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 35);
            this.label7.TabIndex = 0;
            this.label7.Text = "字符间隔：";
            // 
            // StxbName
            // 
            this.StxbName.Location = new System.Drawing.Point(181, 10);
            this.StxbName.Margin = new System.Windows.Forms.Padding(8);
            this.StxbName.Name = "StxbName";
            this.StxbName.Size = new System.Drawing.Size(173, 41);
            this.StxbName.TabIndex = 1;
            this.StxbName.Click += new System.EventHandler(this.StxbName_Click);
            // 
            // StxbStart
            // 
            this.StxbStart.Location = new System.Drawing.Point(181, 68);
            this.StxbStart.Margin = new System.Windows.Forms.Padding(8);
            this.StxbStart.Name = "StxbStart";
            this.StxbStart.Size = new System.Drawing.Size(173, 41);
            this.StxbStart.TabIndex = 1;
            this.StxbStart.Text = "10";
            this.StxbStart.Click += new System.EventHandler(this.StxbStart_Click);
            // 
            // StxbEnd
            // 
            this.StxbEnd.Location = new System.Drawing.Point(181, 132);
            this.StxbEnd.Margin = new System.Windows.Forms.Padding(8);
            this.StxbEnd.Name = "StxbEnd";
            this.StxbEnd.Size = new System.Drawing.Size(173, 41);
            this.StxbEnd.TabIndex = 1;
            this.StxbEnd.Text = "20";
            this.StxbEnd.Click += new System.EventHandler(this.StxbEnd_Click);
            // 
            // StxbAdd
            // 
            this.StxbAdd.Location = new System.Drawing.Point(181, 195);
            this.StxbAdd.Margin = new System.Windows.Forms.Padding(8);
            this.StxbAdd.Name = "StxbAdd";
            this.StxbAdd.Size = new System.Drawing.Size(173, 41);
            this.StxbAdd.TabIndex = 1;
            this.StxbAdd.Text = "2";
            this.StxbAdd.Click += new System.EventHandler(this.StxbAdd_Click);
            // 
            // StxbRepeat
            // 
            this.StxbRepeat.Location = new System.Drawing.Point(181, 258);
            this.StxbRepeat.Margin = new System.Windows.Forms.Padding(8);
            this.StxbRepeat.Name = "StxbRepeat";
            this.StxbRepeat.Size = new System.Drawing.Size(173, 41);
            this.StxbRepeat.TabIndex = 1;
            this.StxbRepeat.Text = "5";
            this.StxbRepeat.Click += new System.EventHandler(this.StxbRepeat_Click);
            // 
            // StxbMin
            // 
            this.StxbMin.Location = new System.Drawing.Point(181, 322);
            this.StxbMin.Margin = new System.Windows.Forms.Padding(8);
            this.StxbMin.Name = "StxbMin";
            this.StxbMin.Size = new System.Drawing.Size(173, 41);
            this.StxbMin.TabIndex = 1;
            this.StxbMin.Text = "4";
            this.StxbMin.Click += new System.EventHandler(this.StxbMin_Click);
            // 
            // StxbSpacing
            // 
            this.StxbSpacing.Location = new System.Drawing.Point(182, 388);
            this.StxbSpacing.Margin = new System.Windows.Forms.Padding(8);
            this.StxbSpacing.Name = "StxbSpacing";
            this.StxbSpacing.Size = new System.Drawing.Size(172, 41);
            this.StxbSpacing.TabIndex = 1;
            this.StxbSpacing.Text = "1";
            this.StxbSpacing.Click += new System.EventHandler(this.StxbSpacing_Click);
            // 
            // SbtnNew
            // 
            this.SbtnNew.BackColor = System.Drawing.Color.LightGreen;
            this.SbtnNew.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F);
            this.SbtnNew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SbtnNew.Location = new System.Drawing.Point(50, 508);
            this.SbtnNew.Margin = new System.Windows.Forms.Padding(8);
            this.SbtnNew.Name = "SbtnNew";
            this.SbtnNew.Size = new System.Drawing.Size(108, 55);
            this.SbtnNew.TabIndex = 2;
            this.SbtnNew.Text = "√";
            this.SbtnNew.UseVisualStyleBackColor = false;
            this.SbtnNew.Click += new System.EventHandler(this.SbtnNew_Click);
            // 
            // ScheckWidth
            // 
            this.ScheckWidth.AutoSize = true;
            this.ScheckWidth.Location = new System.Drawing.Point(181, 458);
            this.ScheckWidth.Margin = new System.Windows.Forms.Padding(8);
            this.ScheckWidth.Name = "ScheckWidth";
            this.ScheckWidth.Size = new System.Drawing.Size(15, 14);
            this.ScheckWidth.TabIndex = 3;
            this.ScheckWidth.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 447);
            this.label8.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 35);
            this.label8.TabIndex = 0;
            this.label8.Text = "是否等宽：";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Pink;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F);
            this.button1.Location = new System.Drawing.Point(210, 508);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 55);
            this.button1.TabIndex = 2;
            this.button1.Text = "x";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.cancel_Click);
            // 
            // InputSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(376, 591);
            this.Controls.Add(this.ScheckWidth);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SbtnNew);
            this.Controls.Add(this.StxbSpacing);
            this.Controls.Add(this.StxbMin);
            this.Controls.Add(this.StxbRepeat);
            this.Controls.Add(this.StxbAdd);
            this.Controls.Add(this.StxbEnd);
            this.Controls.Add(this.StxbStart);
            this.Controls.Add(this.StxbName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "InputSerial";
            this.Text = "新建序列号";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox StxbName;
        private System.Windows.Forms.TextBox StxbStart;
        private System.Windows.Forms.TextBox StxbEnd;
        private System.Windows.Forms.TextBox StxbAdd;
        private System.Windows.Forms.TextBox StxbRepeat;
        private System.Windows.Forms.TextBox StxbMin;
        private System.Windows.Forms.TextBox StxbSpacing;
        private System.Windows.Forms.Button SbtnNew;
        private System.Windows.Forms.CheckBox ScheckWidth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}