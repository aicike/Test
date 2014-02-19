namespace ImTimelyService
{
    partial class Service
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Timer1Min = new System.Timers.Timer();
            this.Timer5Min = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.Timer1Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Timer5Min)).BeginInit();
            // 
            // Timer1Min
            // 
            this.Timer1Min.Enabled = true;
            this.Timer1Min.Interval = 60000D;
            this.Timer1Min.Elapsed += new System.Timers.ElapsedEventHandler(this.Timer1Min_Elapsed);
            // 
            // Timer5Min
            // 
            this.Timer5Min.Enabled = true;
            this.Timer5Min.Interval = 300000D;
            this.Timer5Min.Elapsed += new System.Timers.ElapsedEventHandler(this.Timer5Min_Elapsed);
            // 
            // Service
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.Timer1Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Timer5Min)).EndInit();

        }

        #endregion

        private System.Timers.Timer Timer1Min;
        private System.Timers.Timer Timer5Min;

    }
}
