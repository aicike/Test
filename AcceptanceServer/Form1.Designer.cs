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
            this.btnUpOrDown = new System.Windows.Forms.Button();
            this.tabpage33 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DGType = new System.Windows.Forms.DataGridView();
            this.Times = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.infoGread = new System.Windows.Forms.DataGridView();
            this.InfoTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infoPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.tabpage33.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGType)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoGread)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            // btnUpOrDown
            // 
            this.btnUpOrDown.BackgroundImage = global::AcceptanceServer.Properties.Resources.Start;
            this.btnUpOrDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpOrDown.Location = new System.Drawing.Point(12, 34);
            this.btnUpOrDown.Name = "btnUpOrDown";
            this.btnUpOrDown.Size = new System.Drawing.Size(58, 59);
            this.btnUpOrDown.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnUpOrDown, "启动");
            this.btnUpOrDown.UseVisualStyleBackColor = true;
            // 
            // tabpage33
            // 
            this.tabpage33.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabpage33.Controls.Add(this.tabPage1);
            this.tabpage33.Controls.Add(this.tabPage2);
            this.tabpage33.Controls.Add(this.tabPage4);
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
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.dataGridView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(925, 388);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "查找在线用户";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "暂无";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "当前用户总数：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询在线用户";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.MistyRose;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(3, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(919, 344);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "UNames";
            this.dataGridViewTextBoxColumn1.HeaderText = "用户名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "others";
            this.dataGridViewTextBoxColumn2.HeaderText = "其他信息";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 750;
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
            this.Controls.Add(this.btnUpOrDown);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "lbUser";
            this.Text = "数据接收服务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.lbUser_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabpage33.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGType)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.infoGread)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.Button btnUpOrDown;
        private System.Windows.Forms.TabControl tabpage33;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DGType;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Times;
        private System.Windows.Forms.DataGridViewTextBoxColumn Point;
        private System.Windows.Forms.DataGridView infoGread;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfoTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn infoPoint;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}

