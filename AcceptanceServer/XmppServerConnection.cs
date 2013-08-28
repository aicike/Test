﻿using System;
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

            }
        }

        private void Send(string data)
        {
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(data);
                frm.BeginInvoke(new mydelegate(frm.ShowSendMessage), new Object[] { data });
                m_Sock.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), null);
            }
            catch (Exception ex)
            {
                frm.ShowErrorMessage(ex.Message);
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
                    //显示当前谁上线了
                    frm.Invoke(new dosomethings(delegate()
                    {
                        frm.ShowOlineUser(jid.User, jid.Bare + "||" + jid.Server);
                    }));

                   
                    ////暂时屏蔽
                    //pres.From = this.jid;
                    //foreach (XmppServerConnection con in OnlineUser.onlinuser)
                    //{
                    //    if (con.jid.User != this.jid.User)
                    //    {
                    //        pres.To = con.jid;
                    //        con.Send(pres);
                    //    }
                    //}
                }
                //处理好友离线消息
                else if (pres.Type == PresenceType.unavailable)
                {
                    pres.From = this.jid;

                    OnlineUser.onlinuser.Remove(this);

                    //frm.listBox2.Items.Remove(this.jid.User);
                    //frm.listBox1.Items.Add(this.jid.User + "下线了");

                    foreach (XmppServerConnection con in OnlineUser.onlinuser)
                    {
                        if (con.jid.User != this.jid.User)
                        {
                            pres.To = con.jid;
                            con.Send(pres);
                        }
                    }

                }

            }
            else if (e.GetType() == typeof(agsXMPP.protocol.client.Message))
            {
                agsXMPP.protocol.client.Message msg = e as agsXMPP.protocol.client.Message;
                //点对点聊
                if (msg.Type == MessageType.chat)
                {
                    NewsProtocol Np = msg.SelectSingleElement(typeof(NewsProtocol)) as NewsProtocol;
                    //消息
                    if (Np.MT == "1")
                    {
                        //存储聊天记录
                        DataTable dt = DataBusiness.InsertChatRecord(msg, Np).Tables[0];
                        if (dt == null) //数据存储失败 
                        {
                            //return 发送失败 --------------------------------------------------------------------------------------------------
                        }
                        else
                        {
                            //在线
                            if (OnlineUser.onlinuser.Any(a => a.jid.User == msg.To.User))
                            {
                                //转发发送消息
                                IEnumerable<XmppServerConnection> cons =  OnlineUser.onlinuser.Where(a => a.jid.User == msg.To.User).ToList();
                                //XmppServerConnection con = OnlineUser.onlinuser.Where(a => a.jid.User == msg.To.User).ToList();

                                msg.From = jid;
                                foreach (XmppServerConnection con in cons)
                                {
                                    con.Send(msg);
                                }
                               
                                
                                //frm.ShowMesage(msg.From.User + " 对 " + msg.To.User + " 发送信息 \r" + msg.Body);
                            }
                            //不在线
                            else
                            {
                                //本条消息数据库ID
                                int ThisMessageID = int.Parse(dt.Rows[0][0].ToString());
                                //存储离线记录
                                int cnt = DataBusiness.InsertOffLineData(msg, Np, ThisMessageID);
                                if (cnt <= 0)
                                {
                                    //return 发送失败--------------------------------------------------------------------------------------------------
                                }

                            }
                        }
                    }
                    //状态
                    else if (Np.MT == "2")
                    {

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
                        Send(iq);
                        break;
                    case IqType.set:
                        //上线
                        this.jid = new Jid(auth.Username, "localhost", "Resource");


                        //记录用户
                        OnlineUser.onlinuser.Add(this);

                        string[] subItem0 = { auth.Username, m_Sock.RemoteEndPoint.ToString() };

                        //frm.Invoke(new dosomethings(delegate()
                        //{
                        //    //frm.listBox1.Items.Add(auth.Username + "上线");
                        //    //frm.listBox2.Items.Add(auth.Username);
                        //}));

                        //刚刚登陆获取未读消息
                        Task t = new Task(() =>
                        {
                            //售楼代表s；用户u
                            string AoU = jid.User.Substring(0, 1);
                            //ID
                            string AoUID = jid.User.Substring(1);
                            XmppServerConnection con = OnlineUser.onlinuser.Where(a => a.jid.User == jid.User).ToList()[0];

                            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                            msg.Type = MessageType.chat;
                            msg.From = jid;
                            msg.To = new Jid(jid.User, "localhost", jid.User);

                            NewsProtocol np = new NewsProtocol();
                            np.MT = "3";//未读消息

                            List<UnreadMessage> UMlist = DataBusiness.GetUnreadMessage(AoU, AoUID);

                            try
                            {
                                msg.Body = UMlist.ObjectToJson();

                                con.Send(msg);
                            }
                            catch { }


                        });
                        //t.Start();

                        iq.SwitchDirection();
                        iq.Type = IqType.result;
                        iq.Query = null;
                        Send(iq);
                        break;
                }

            }
            else if (iq.Query.GetType() == typeof(Roster))
            {
                //发送用户列表
                //ProcessRosterIQ(iq);

            }

        }

        // 发送用户列表
        private void ProcessRosterIQ(IQ iq)
        {
            if (iq.Type == IqType.get)
            {

                //获取用户列表
                for (int i = 0; i < OnlineUser.onlinuser.Count; i++)
                {
                    RosterItem ri = new RosterItem();
                    ri.Name = OnlineUser.onlinuser[i].ToString();
                    ri.Subscription = SubscriptionType.both;
                    ri.Jid = new Jid(ri.Name + "@localhost");
                    iq.Query.AddChild(ri);
                }
                Send(iq);

                //将其他人的信息发送
                Presence pre;
                pre = new Presence();
                pre.Show = ShowType.chat;
                foreach (XmppServerConnection con in OnlineUser.onlinuser)
                {
                    pre.From = con.jid;
                    pre.Value = con.m_Sock.RemoteEndPoint.ToString();
                    Send(pre);
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

            Send(sb.ToString());
        }

        private void Send(Element el)
        {
            Send(el.ToString());
        }

    }
}
