using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP.Xml;
using System.Net.Sockets;
using agsXMPP;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.auth;
using agsXMPP.protocol.iq.roster;
using System.Collections;
using Poco;
using Poco.Enum;
using AcceptanceServer.DataOperate;
using AcceptanceServer.DataBllOperate;
using System.Data;
using System.Threading.Tasks;
using Business;
using System.Security.Policy;
using System.Configuration;



namespace AcceptanceServer
{
    public class XmppServerConnection
    {
        #region(变量)
        private StreamParser streamParser;
        private Socket m_Sock;
        delegate void dosomethings();
        private const int BUFFERSIZE = 1024;
        private byte[] buffer = new byte[BUFFERSIZE];
        private lbUser frm;
        public Jid jid;
        public string LandingApproach = "";
        public delegate void mydelegate(string str);

        #endregion


        #region(窗体构造函数)
        public XmppServerConnection()
        {
            streamParser = new StreamParser();
            streamParser.OnStreamStart += new StreamHandler(streamParser_OnStreamStart);
            streamParser.OnStreamEnd += new StreamHandler(streamParser_OnStreamEnd);
            streamParser.OnStreamElement += new StreamHandler(streamParser_OnStreamElement);
        }
        public XmppServerConnection(lbUser a)
            : this()
        {
            frm = a;
        }
        public XmppServerConnection(lbUser a, Socket sock)
            : this()
        {
            m_Sock = sock;
            frm = a;
            m_Sock.BeginReceive(buffer, 0, BUFFERSIZE, 0, new AsyncCallback(ReadCallback), null);
        }

        #endregion


        public void ReadCallback(IAsyncResult ar)
        {
            try
            {
                int bytesRead = m_Sock.EndReceive(ar);

                if (bytesRead > 0)
                {
                    streamParser.Push(buffer, 0, bytesRead);

                    m_Sock.BeginReceive(buffer, 0, BUFFERSIZE, 0, new AsyncCallback(ReadCallback), null);
                }
                else
                {
                    m_Sock.Shutdown(SocketShutdown.Both);
                    m_Sock.Close();
                }
            }
            catch (Exception ex)
            {
                //下线------------
                //frm.ShowErrorMessage(ex.Message);
                string ShowError = ConfigurationManager.AppSettings["ShowError"].ToString();
                if (ShowError == "true")
                {
                    frm.ShowErrorMessage(ex.Message);
                }

            }
        }




        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                int bytesSent = m_Sock.EndSend(ar);
            }
            catch (Exception ex)
            {

                frm.ShowErrorMessage(ex.Message);
            }
        }



        #region (sessionid)
        private string m_SessionId = null;

        public string SessionId
        {
            get
            {
                return m_SessionId;
            }
            set
            {
                m_SessionId = value;
            }
        }
        #endregion

        private void streamParser_OnStreamStart(object sender, Node e)
        {
            SendOpenStream();
        }


        private void streamParser_OnStreamEnd(object sender, Node e)
        {

        }

        /// <summary>
        /// 三个函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void streamParser_OnStreamElement(object sender, Node e)
        {
            frm.BeginInvoke(new mydelegate(frm.ShowRecvMessage), new Object[] { e.ToString() });

            if (e.GetType() == typeof(Presence))
            {
                Presence pres = e as Presence;
                //处理用户上线消息
                if (pres.Show == ShowType.chat && pres.Type == PresenceType.available)//pres.Show == ShowType.chat &&
                {
                    string showUP = ConfigurationManager.AppSettings["UPnotice"].ToString();
                    if (showUP == "true")
                    {
                        ////显示当前谁上线了
                        frm.Invoke(new dosomethings(delegate()
                        {
                            frm.ShowOlineUser(jid.User, jid.Bare + "||" + jid.Server);
                        }));
                    }


                }
                //处理好友离线消息
                else if (pres.Type == PresenceType.unavailable)
                {
                    pres.From = this.jid;

                    OnlineUser.onlinuser.Remove(this);

                }

            }
            else if (e.GetType() == typeof(agsXMPP.protocol.client.Message))
            {

                agsXMPP.protocol.client.Message msg = e as agsXMPP.protocol.client.Message;
                //点对点聊
                if (msg.Type == MessageType.chat)
                {
                    XMPP_Body_SMS Np = msg.SelectSingleElement(typeof(XMPP_Body_SMS)) as XMPP_Body_SMS;

                    //消息

                    //转发发送消息 所有设备IEnumerable
                    List<XmppServerConnection> cons = OnlineUser.onlinuser.Where(a => a.jid.User == msg.To.User).ToList();
                    foreach (XmppServerConnection xcon in cons)
                    {
                        xcon.Send(msg, 0);
                    }



                }


            }
            else if (e.GetType() == typeof(IQ))
            {
                ProcessIQ(e as IQ);
            }
        }

        private void ProcessIQ(IQ iq)
        {
            //用户认证
            if (iq.Query.GetType() == typeof(Auth))
            {
                Auth auth = iq.Query as Auth;
                switch (iq.Type)
                {
                    case IqType.get:
                        iq.SwitchDirection();
                        iq.Type = IqType.result;
                        auth.AddChild(new Element("Password"));
                        auth.AddChild(new Element("digest"));
                        Send(iq, 0);
                        break;
                    case IqType.set:
                        //上线
                        this.jid = new Jid(auth.Username, "localhost", "Resource");



                        List<XmppServerConnection> cons = OnlineUser.onlinuser.Where(a => a.jid.User == auth.Username).ToList();
                        if (cons.Count > 0)
                        {
                            for (int i = 0; i < OnlineUser.onlinuser.Count; i++)
                            {
                                if (OnlineUser.onlinuser[i].jid.User == auth.Username)
                                {
                                    OnlineUser.onlinuser.RemoveAt(i);
                                    i--;
                                }
                            }
                        }





                        //记录用户
                        OnlineUser.onlinuser.Add(this);

                        string[] subItem0 = { auth.Username, m_Sock.RemoteEndPoint.ToString() };


                        iq.SwitchDirection();
                        iq.Type = IqType.result;
                        iq.Query = null;
                        Send(iq, 0);
                        break;
                }

            }


        }


        private void SendOpenStream()
        {
            string ServerDomain = "localhost";

            this.SessionId = AcceptanceServer.SessionId.CreateNewId();

            StringBuilder sb = new StringBuilder();

            sb.Append("<stream:stream from='");
            sb.Append(ServerDomain);

            sb.Append("' xmlns='");
            sb.Append(agsXMPP.Uri.CLIENT);

            sb.Append("' xmlns:stream='");
            sb.Append(agsXMPP.Uri.STREAM);

            sb.Append("' id='");
            sb.Append(this.SessionId);

            sb.Append("'>");

            Send(sb.ToString(), null, 0);
        }

        private void Send(Element el, int ThisMessageID)
        {
            Send(el.ToString(), el, ThisMessageID);
        }

        private void Send(string data, Node n, int ThisMessageID)
        {
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(data);
                frm.BeginInvoke(new mydelegate(frm.ShowSendMessage), new Object[] { data });
                if (m_Sock.Connected)
                {
                    m_Sock.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), null);
                }
                else //不在线
                {

                    agsXMPP.protocol.client.Message msg = n as agsXMPP.protocol.client.Message;
                    NewsProtocol Np = msg.SelectSingleElement(typeof(NewsProtocol)) as NewsProtocol;
                    List<XmppServerConnection> cons = OnlineUser.onlinuser.Where(a => a.jid.User == msg.To.User).ToList();
                    if (cons.Count > 0)
                    {
                        for (int i = 0; i < OnlineUser.onlinuser.Count; i++)
                        {
                            if (OnlineUser.onlinuser[i].jid.User == msg.To.User)
                            {
                                OnlineUser.onlinuser.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ShowError = ConfigurationManager.AppSettings["ShowError"].ToString();
                if (ShowError == "true")
                {
                    frm.ShowErrorMessage(ex.Message);
                }
                throw;
            }
        }

        //-----------------------------------------------------------------------------------------------------


    }
}
