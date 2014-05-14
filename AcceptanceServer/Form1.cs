using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using Poco;
using System.Threading.Tasks;
using AcceptanceServer.Properties;
using Common;

namespace AcceptanceServer
{
    public partial class lbUser : Form
    {
        //错误信息
        //public static DataTable ErrorMessageDt;
        ////运行信息（开发测试）
        //public static DataTable MessageDt;



        public lbUser()
        {
            InitializeComponent();
            //初始化显示界面显示数据dt
            //initErrorMessageDT();
            //initMessageDT();

            agsXMPP.Factory.ElementFactory.AddElementType("NewsProtocol", "agsoftware:NewsProtocol", typeof(NewsProtocol));
        }
        private ManualResetEvent allDone = new ManualResetEvent(false);
        private Socket listener;
        private bool m_Listening;
        //开启服务
        private void Form1_Load(object sender, EventArgs e)
        {
            //System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
            //{
            //    Listen();
            //});

            //t.Start();

            ThreadStart myThreadDelegate = new ThreadStart(Listen);
            Thread myThread = new Thread(myThreadDelegate);
            myThread.Start();
            myThread.IsBackground = true;



            //ShowErrorMessage("服务开启.....端口号：5222");
        }
        private void Listen()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 5222);

            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(20);

                m_Listening = true;

                while (m_Listening)
                {
                    allDone.Reset();

                    //    this.Invoke(new dosomethings(delegate()
                    //{
                    //    //listBox1.Items.Add("等待客户端连接！");
                    //}));

                    listener.BeginAccept(new AsyncCallback(AcceptCallBack), null);

                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                //ShowErrorMessage(ex.Message);
            }
        }

        public delegate void dosomethings();

        public void AcceptCallBack(IAsyncResult ar)
        {
            allDone.Set();

            Socket newSock = listener.EndAccept(ar);
            try
            {
                AcceptanceServer.XmppServerConnection con = new AcceptanceServer.XmppServerConnection(this, newSock);
            }
            catch (Exception ex)
            {
                try
                {

                    EmailInfo emailInfo = new EmailInfo();
                    emailInfo.To = "shuangqi.wu@imtimely.com";
                    emailInfo.Subject = "ImTimely - 聊天服务出错！请速检查（5222）";
                    emailInfo.IsHtml = true;
                    emailInfo.UseSSL = false;


                    if (this == null)
                    {
                        emailInfo.Body = "聊天服务出错！<br/><br/>请速检查<br/><br/> Form窗口为NULL。<br/><br/>" + ex.Message + "<br/><br/>" + ex.StackTrace;
                    }
                    else
                    {
                        emailInfo.Body = "聊天服务出错！<br/><br/>请速检查。<br/><br/>" + ex.Message + "<br/><br/>" + ex.StackTrace;
                    }


                    SendEmail.SendMailAsync(emailInfo);
                }
                catch { }
            }
        }

        #region(源数据)
        public void ShowRecvMessage(string str)
        {
            //this.Invoke(new dosomethings(delegate()
            //   {
            //       //doc.LoadXml(str);
            //       //foreach (Node var in doc.ChildNodes)
            //       //{
            //       //    string s = var.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).ToString();
            //       //    listBox1.Items.Add(s);
            //       //}
            //       //listBox1.Items.Add("接收的" + str);
            //       //ShowMesage("接收的：" + str);

            //   }));
        }

        public void ShowSendMessage(string str)
        {
            //listBox1.Items.Add("发送的"+str);
            //ShowMesage("发送的：" + str);
        }

        #endregion

        #region 界面显示信息与日志

        //初始化显示异常消失dt
        //public void initErrorMessageDT()
        //{
        //    ErrorMessageDt = new DataTable();
        //    ErrorMessageDt.Columns.Add("Times");
        //    ErrorMessageDt.Columns.Add("Point");
        //}

        //显示异常信息（日志）
        //public void ShowErrorMessage(string message)
        //{
        //    DataRow row = ErrorMessageDt.NewRow();
        //    row["Times"] = DateTime.Now;
        //    row["Point"] = message;
        //    ErrorMessageDt.Rows.Add(row);

        //    DGType.DataSource = ErrorMessageDt;

        //    //日志


        //}

        //初始化运行信息dt
        //public void initMessageDT()
        //{
        //    MessageDt = new DataTable();
        //    MessageDt.Columns.Add("infoTime");
        //    MessageDt.Columns.Add("infoPoint");
        //}

        //显示运行信息
        //public void ShowMesage(string message)
        //{
        //    DataRow row = MessageDt.NewRow();
        //    row["infoTime"] = DateTime.Now;
        //    row["infoPoint"] = message;
        //    MessageDt.Rows.Add(row);

        //    infoGread.DataSource = MessageDt;
        //}

        //初始化在线用户dt






        #endregion





        private void lbUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings r = new Settings();

            CloseTipForm form = new CloseTipForm();
            form.ShowDialog();

            r.Reload();
            if (r.CloseAction == ClickCloseButtonAction.Cancel)//取消
            {
                e.Cancel = true;
            }
            else if (r.CloseAction == ClickCloseButtonAction.Min)//最小化
            {
                e.Cancel = true;
                //this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (r.CloseAction == ClickCloseButtonAction.Close)//关闭
            {
            }


            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;    //取消"关闭窗口"事件
            //    this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果
            //    notifyIcon1.Visible = true;
            //    this.Hide();
            //    return;
            //}

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }


        //查询当前在线用户
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtuser = new DataTable();
                dtuser.Columns.Add("unames");
                dtuser.Columns.Add("others");
                foreach (var item in OnlineUser.onlinuser)
                {
                    DataRow row = dtuser.NewRow();
                    row["unames"] = item.jid.User.ToString();
                    row["others"] = item.jid.Bare + "||" + item.jid.Resource + "||" + item.jid.Server;
                    dtuser.Rows.Add(row);
                }
                dataGridView1.DataSource = dtuser;

                label2.Text = OnlineUser.onlinuser.Count.ToString();
            }
            catch { }
        }



    }
}
