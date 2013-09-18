using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AcceptanceServer.Properties;

namespace AcceptanceServer
{
    public partial class CloseTipForm : Form
    {
        public CloseTipForm()
        {
            InitializeComponent();
            InitControl();
        }
        private void InitControl()
        {
            //设置窗口位置
            this.StartPosition = FormStartPosition.CenterParent;

            //设置上次选中

            rbtn1.Checked = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings r = new Settings();
            if (rbtn1.Checked)
            {
                r.CloseAction = ClickCloseButtonAction.Min;
            }
            else if (rbtn2.Checked)
            {
                r.CloseAction = ClickCloseButtonAction.Close;
            }

            r.Save();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings r = new Settings();
            r.CloseAction = ClickCloseButtonAction.Cancel;
            r.Save();
            this.Close();
        }
    }

    public class ClickCloseButtonAction
    {
        public static int Tip = 0;
        public static int Min = 1;
        public static int Close = 2;
        public static int Cancel = 3;
    }
}
