using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using Poco;
using System.Threading.Tasks;
using AcceptanceServer.Properties;

namespace AcceptanceServer
{
    public partial class lbUser : Form
    {
       


        public lbUser()
        {
            InitializeComponent();
            //初始化显示界面显示数据dt

            agsXMPP.Factory.ElementFactory.AddElementType("NewsProtocol", "agsoftware:NewsProtocol", typeof(NewsProtocol));
        }
        private ManualResetEvent allDone = new ManualResetEvent(false);
        private Socket listener;
        private bool m_Listening;
        //开启服务
        private void Form1_Load(object sender, EventArgs e)
        {
            ThreadStart myThreadDelegate = new ThreadStart(Listen);
            Thread myThread = new Thread(myThreadDelegate);
            myThread.Start();
            myThread.IsBackground = true;
           
            //System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
            //{
            //    Listen();
            //});
            //t.Start();
        }
        private void Listen()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 5333);

            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(20);

                m_Listening = true;

                while (m_Listening)
                {
                    allDone.Reset();

                    listener.BeginAccept(new AsyncCallback(AcceptCallBack), null);

                    allDone.WaitOne();
                }
            }

            catch (Exception ex)
            {

            }
        }
        public delegate void dosomethings();
        public void AcceptCallBack(IAsyncResult ar)
        {
            allDone.Set();

            Socket newSock = listener.EndAccept(ar);

            AcceptanceServer.XmppServerConnection con = new AcceptanceServer.XmppServerConnection(this, newSock);
        }

        #region(源数据)
        public void ShowRecvMessage(string str)
        {
            this.Invoke(new dosomethings(delegate()
               {
                   //doc.LoadXml(str);
                   //foreach (Node var in doc.ChildNodes)
                   //{
                   //    string s = var.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).ToString();
                   //    listBox1.Items.Add(s);
                   //}
                   //listBox1.Items.Add("接收的" + str);
                   //ShowMesage("接收的：" + str);

               }));
        }

        public void ShowSendMessage(string str)
        {
            //listBox1.Items.Add("发送的"+str);
            //ShowMesage("发送的：" + str);
        }

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



        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }


      
    }
}
