namespace AcceptanceServer
{
    partial class lbUser
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lbUser));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabpage33 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DGType = new System.Windows.Forms.DataGridView();
            this.Times = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.infoGread = new System.Windows.Forms.DataGridView();
            this.InfoTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infoPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgUser = new System.Windows.Forms.DataGridView();
            this.UName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.other = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.tabpage33.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGType)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoGread)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(957, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // tabpage33
            // 
            this.tabpage33.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabpage33.Controls.Add(this.tabPage1);
            this.tabpage33.Controls.Add(this.tabPage2);
            this.tabpage33.Controls.Add(this.tabPage3);
            this.tabpage33.Location = new System.Drawing.Point(12, 109);
            this.tabpage33.Name = "tabpage33";
            this.tabpage33.SelectedIndex = 0;
            this.tabpage33.Size = new System.Drawing.Size(933, 414);
            this.tabpage33.TabIndex = 3;
            this.tabpage33.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(925, 388);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "运行状态";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DGType
            // 
            this.DGType.AllowUserToAddRows = false;
            this.DGType.AllowUserToDeleteRows = false;
            this.DGType.BackgroundColor = System.Drawing.Color.PapayaWhip;
            this.DGType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DGType.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DGType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Times,
            this.Point});
            this.DGType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGType.Location = new System.Drawing.Point(3, 3);
            this.DGType.Name = "DGType";
            this.DGType.RowHeadersVisible = false;
            this.DGType.RowTemplate.Height = 23;
            this.DGType.Size = new System.Drawing.Size(919, 382);
            this.DGType.TabIndex = 0;
            // 
            // Times
            // 
            this.Times.DataPropertyName = "Times";
            this.Times.HeaderText = "时间";
            this.Times.Name = "Times";
            this.Times.ReadOnly = true;
            this.Times.Width = 150;
            // 
            // Point
            // 
            this.Point.DataPropertyName = "Point";
            this.Point.HeaderText = "提示";
            this.Point.Name = "Point";
            this.Point.ReadOnly = true;
            this.Point.Width = 750;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.infoGread);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(925, 388);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "详细信息（开发）";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // infoGread
            // 
            this.infoGread.AllowUserToAddRows = false;
            this.infoGread.AllowUserToDeleteRows = false;
            this.infoGread.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.infoGread.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.infoGread.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InfoTime,
            this.infoPoint});
            this.infoGread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoGread.Location = new System.Drawing.Point(3, 3);
            this.infoGread.Name = "infoGread";
            this.infoGread.RowHeadersVisible = false;
            this.infoGread.RowTemplate.Height = 23;
            this.infoGread.Size = new System.Drawing.Size(919, 382);
            this.infoGread.TabIndex = 1;
            // 
            // InfoTime
            // 
            this.InfoTime.DataPropertyName = "InfoTime";
            this.InfoTime.HeaderText = "时间";
            this.InfoTime.Name = "InfoTime";
            this.InfoTime.ReadOnly = true;
            this.InfoTime.Width = 150;
            // 
            // infoPoint
            // 
            this.infoPoint.DataPropertyName = "infoPoint";
            this.infoPoint.HeaderText = "消息";
            this.infoPoint.Name = "infoPoint";
            this.infoPoint.ReadOnly = true;
            this.infoPoint.Width = 700;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgUser);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(925, 388);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "在线用户信息";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgUser
            // 
            this.dgUser.AllowUserToAddRows = false;
            this.dgUser.AllowUserToDeleteRows = false;
            this.dgUser.BackgroundColor = System.Drawing.Color.PaleGreen;
            this.dgUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UName,
            this.other});
            this.dgUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUser.Location = new System.Drawing.Point(3, 3);
            this.dgUser.Name = "dgUser";
            this.dgUser.RowHeadersVisible = false;
            this.dgUser.RowTemplate.Height = 23;
            this.dgUser.Size = new System.Drawing.Size(919, 382);
            this.dgUser.TabIndex = 0;
            // 
            // UName
            // 
            this.UName.DataPropertyName = "UName";
            this.UName.HeaderText = "用户名称";
            this.UName.Name = "UName";
            this.UName.ReadOnly = true;
            // 
            // other
            // 
            this.other.DataPropertyName = "other";
            this.other.HeaderText = "其他信息";
            this.other.Name = "other";
            this.other.ReadOnly = true;
            this.other.Width = 750;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "聊天服务";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // lbUser
            // 
            this.AccessibleDescription = "5";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 555);
            this.Controls.Add(this.tabpage33);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "lbUser";
            this.Text = "SMS服务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.lbUser_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabpage33.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGType)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.infoGread)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabpage33;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DGType;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Times;
        private System.Windows.Forms.DataGridViewTextBoxColumn Point;
        private System.Windows.Forms.DataGridView infoGread;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfoTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn infoPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn UName;
        private System.Windows.Forms.DataGridViewTextBoxColumn other;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

