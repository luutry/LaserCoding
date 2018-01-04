namespace QuickCoding
{
    partial class EditText
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TbtnNew = new System.Windows.Forms.Button();
            this.TtxbName = new System.Windows.Forms.TextBox();
            this.TtxbContent = new System.Windows.Forms.TextBox();
            this.TtxbSpacing = new System.Windows.Forms.TextBox();
            this.TcheckBoxWidth = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "内容：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 159);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "字符间隔：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.label5.Location = new System.Drawing.Point(165, 167);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "范围(05-2)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 205);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "等宽：";
            // 
            // TbtnNew
            // 
            this.TbtnNew.BackColor = System.Drawing.Color.Transparent;
            this.TbtnNew.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.TbtnNew.Location = new System.Drawing.Point(169, 209);
            this.TbtnNew.Margin = new System.Windows.Forms.Padding(5);
            this.TbtnNew.Name = "TbtnNew";
            this.TbtnNew.Size = new System.Drawing.Size(95, 43);
            this.TbtnNew.TabIndex = 1;
            this.TbtnNew.Text = "确 定";
            this.TbtnNew.UseVisualStyleBackColor = false;
            this.TbtnNew.Click += new System.EventHandler(this.TbtnNew_Click);
            // 
            // TtxbName
            // 
            this.TtxbName.Location = new System.Drawing.Point(89, 6);
            this.TtxbName.Margin = new System.Windows.Forms.Padding(5);
            this.TtxbName.Name = "TtxbName";
            this.TtxbName.Size = new System.Drawing.Size(163, 28);
            this.TtxbName.TabIndex = 2;
            // 
            // TtxbContent
            // 
            this.TtxbContent.Location = new System.Drawing.Point(89, 53);
            this.TtxbContent.Margin = new System.Windows.Forms.Padding(5);
            this.TtxbContent.Multiline = true;
            this.TtxbContent.Name = "TtxbContent";
            this.TtxbContent.Size = new System.Drawing.Size(164, 90);
            this.TtxbContent.TabIndex = 2;
            // 
            // TtxbSpacing
            // 
            this.TtxbSpacing.Location = new System.Drawing.Point(89, 159);
            this.TtxbSpacing.Margin = new System.Windows.Forms.Padding(5);
            this.TtxbSpacing.Name = "TtxbSpacing";
            this.TtxbSpacing.Size = new System.Drawing.Size(76, 28);
            this.TtxbSpacing.TabIndex = 2;
            this.TtxbSpacing.Text = "1";
            // 
            // TcheckBoxWidth
            // 
            this.TcheckBoxWidth.AutoSize = true;
            this.TcheckBoxWidth.Location = new System.Drawing.Point(89, 209);
            this.TcheckBoxWidth.Margin = new System.Windows.Forms.Padding(5);
            this.TcheckBoxWidth.Name = "TcheckBoxWidth";
            this.TcheckBoxWidth.Size = new System.Drawing.Size(15, 14);
            this.TcheckBoxWidth.TabIndex = 3;
            this.TcheckBoxWidth.UseVisualStyleBackColor = true;
            // 
            // EditText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 265);
            this.Controls.Add(this.TcheckBoxWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TtxbSpacing);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TtxbContent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TtxbName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TbtnNew);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "EditText";
            this.Text = "编辑文本";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button TbtnNew;
        private System.Windows.Forms.TextBox TtxbName;
        private System.Windows.Forms.TextBox TtxbContent;
        private System.Windows.Forms.TextBox TtxbSpacing;
        private System.Windows.Forms.CheckBox TcheckBoxWidth;

    }
}