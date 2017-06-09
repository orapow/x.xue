namespace xx.tool
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_trans = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtb_cot = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_trans
            // 
            this.bt_trans.Location = new System.Drawing.Point(12, 12);
            this.bt_trans.Name = "bt_trans";
            this.bt_trans.Size = new System.Drawing.Size(75, 23);
            this.bt_trans.TabIndex = 2;
            this.bt_trans.Text = "转换";
            this.bt_trans.UseVisualStyleBackColor = true;
            this.bt_trans.Click += new System.EventHandler(this.btn_trans_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 355);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 105);
            this.label1.TabIndex = 3;
            this.label1.Text = "操作步骤：\r\n1、将word里的试题选中并复制\r\n2、点击 转换 按钮\r\n3、将转换后的内容复制\r\n4、切换后台录入界面 将内容粘贴到 试题栏";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rtb_cot);
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 311);
            this.panel1.TabIndex = 4;
            // 
            // rtb_cot
            // 
            this.rtb_cot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_cot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_cot.EnableAutoDragDrop = true;
            this.rtb_cot.HideSelection = false;
            this.rtb_cot.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rtb_cot.Location = new System.Drawing.Point(0, 0);
            this.rtb_cot.Margin = new System.Windows.Forms.Padding(0);
            this.rtb_cot.Name = "rtb_cot";
            this.rtb_cot.Size = new System.Drawing.Size(590, 309);
            this.rtb_cot.TabIndex = 1;
            this.rtb_cot.Text = "";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 469);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_trans);
            this.Name = "Main";
            this.Text = "试题转换器";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bt_trans;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtb_cot;
    }
}

