namespace QuickCoding
{
    partial class Param
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
            this.tbPower = new System.Windows.Forms.TextBox();
            this.tbPrintSpeed = new System.Windows.Forms.TextBox();
            this.tbFrequency = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "功率(%)：";
            // 
            // tbPower
            // 
            this.tbPower.Location = new System.Drawing.Point(150, 6);
            this.tbPower.Margin = new System.Windows.Forms.Padding(5);
            this.tbPower.Name = "tbPower";
            this.tbPower.Size = new System.Drawing.Size(177, 45);
            this.tbPower.TabIndex = 1;
            this.tbPower.Click += new System.EventHandler(this.tbPower_Click);
            // 
            // tbPrintSpeed
            // 
            this.tbPrintSpeed.Location = new System.Drawing.Point(150, 61);
            this.tbPrintSpeed.Margin = new System.Windows.Forms.Padding(5);
            this.tbPrintSpeed.Name = "tbPrintSpeed";
            this.tbPrintSpeed.Size = new System.Drawing.Size(177, 45);
            this.tbPrintSpeed.TabIndex = 1;
            this.tbPrintSpeed.Click += new System.EventHandler(this.tbPrintSpeed_Click);
            // 
            // tbFrequency
            // 
            this.tbFrequency.Location = new System.Drawing.Point(150, 113);
            this.tbFrequency.Margin = new System.Windows.Forms.Padding(5);
            this.tbFrequency.Name = "tbFrequency";
            this.tbFrequency.Size = new System.Drawing.Size(177, 45);
            this.tbFrequency.TabIndex = 1;
            this.tbFrequency.Click += new System.EventHandler(this.tbFrequency_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightGreen;
            this.btnOK.Location = new System.Drawing.Point(20, 172);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(135, 62);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "√";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Pink;
            this.btnCancel.Location = new System.Drawing.Point(174, 172);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 62);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "x";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 39);
            this.label2.TabIndex = 0;
            this.label2.Text = "打印速度：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 116);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 39);
            this.label9.TabIndex = 0;
            this.label9.Text = "频率：";
            // 
            // Param
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 38F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(337, 248);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbFrequency);
            this.Controls.Add(this.tbPrintSpeed);
            this.Controls.Add(this.tbPower);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F);
            this.Margin = new System.Windows.Forms.Padding(9);
            this.Name = "Param";
            this.Text = "参数";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPower;
        private System.Windows.Forms.TextBox tbPrintSpeed;
        private System.Windows.Forms.TextBox tbFrequency;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
    }
}