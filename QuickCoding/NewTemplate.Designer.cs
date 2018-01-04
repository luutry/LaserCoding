namespace QuickCoding
{
    partial class NewTemplate
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
            this.tempName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F);
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "模板名";
            // 
            // tempName
            // 
            this.tempName.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.tempName.Location = new System.Drawing.Point(12, 47);
            this.tempName.Name = "tempName";
            this.tempName.Size = new System.Drawing.Size(167, 38);
            this.tempName.TabIndex = 1;
            this.tempName.Click += new System.EventHandler(this.tempName_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightGreen;
            this.btnOK.Location = new System.Drawing.Point(12, 101);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 45);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "√";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.Pink;
            this.cancel.Location = new System.Drawing.Point(101, 101);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(78, 45);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "x";
            this.cancel.UseVisualStyleBackColor = false;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // NewTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(191, 156);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tempName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "NewTemplate";
            this.Text = "NewTemplate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tempName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button cancel;
    }
}