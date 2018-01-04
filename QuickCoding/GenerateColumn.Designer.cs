namespace QuickCoding
{
    partial class GenerateColumn
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.columnCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.columnSpacing = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.LightGreen;
            this.buttonOK.Location = new System.Drawing.Point(17, 102);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(69, 40);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "√";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "列数:";
            // 
            // columnCount
            // 
            this.columnCount.Location = new System.Drawing.Point(92, 9);
            this.columnCount.Name = "columnCount";
            this.columnCount.Size = new System.Drawing.Size(100, 32);
            this.columnCount.TabIndex = 2;
            this.columnCount.Click += new System.EventHandler(this.columnCount_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "列间距:";
            // 
            // columnSpacing
            // 
            this.columnSpacing.Location = new System.Drawing.Point(92, 52);
            this.columnSpacing.Name = "columnSpacing";
            this.columnSpacing.Size = new System.Drawing.Size(100, 32);
            this.columnSpacing.TabIndex = 2;
            this.columnSpacing.Click += new System.EventHandler(this.columnSpacing_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Pink;
            this.buttonCancel.Location = new System.Drawing.Point(120, 102);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 40);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "x";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // GenerateColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(211, 157);
            this.Controls.Add(this.columnSpacing);
            this.Controls.Add(this.columnCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "GenerateColumn";
            this.Text = "GenerateColumn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox columnCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox columnSpacing;
        private System.Windows.Forms.Button buttonCancel;

    }
}