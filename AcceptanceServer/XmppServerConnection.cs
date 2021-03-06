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
using Business;
using System.Security.Policy;
using System.Configuration;
using Interface;
using Injection;
using Poco.WebAPI_Poco;



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
                DataBusiness.SetLog(ex);
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
            try
            {
                frm.BeginInvoke(new mydelegate(frm.ShowRecvMessage), new Object[] { e.ToString() });

                if (e.GetType() == typeof(Presence))
                {
                    Presence pres = e as Presence;
                    //处理用户上线消息
                    if (pres.Show == ShowType.chat && pres.Type == PresenceType.available)//pres.Show == ShowType.chat &&
                    {

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
                    string WebUrlIP = ConfigurationManager.AppSettings["WebUrlIP"].ToString();
                    agsXMPP.protocol.client.Message msg = e as agsXMPP.protocol.client.Message;
                    //点对点聊
                    if (msg.Type == MessageType.chat)
                    {
                        NewsProtocol Np = msg.SelectSingleElement(typeof(NewsProtocol)) as NewsProtocol;

                        //消息
                        if (Np.MT == "1")
                        {
                            DataTable dt;
                            //存储聊天记录
                            try
                            {
                                if (Np.FielUrl != null)
                                {
                                    Np.FielUrl = Np.FielUrl.Replace(WebUrlIP, "~");
                                }
                                dt = DataBusiness.InsertChatRecord(msg, Np).Tables[0];
                                //本条消息数据库ID
                                int ThisMessageID = 0;
                                if (dt == null) //数据存储失败 
                                {
                                    //return  6发送失败
                                    SendMessageStatus("6", msg.To.User);
                                }
                                else
                                {
                                    ThisMessageID = int.Parse(dt.Rows[0][0].ToString());
                                    dt.Dispose();
                                    if (!string.IsNullOrEmpty(Np.EID))
                                    {
                                        //处理多图文
                                        if (Np.EID == "4")
                                        {
                                            var ImgTextID = Np.ImgTextID;
                                            var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
                                            var itext = libraryImageTextModel.Get(int.Parse(ImgTextID));
                                            if (itext != null)
                                            {
                                                if (itext.LibraryImageTexts.Count > 0)
                                                {
                                                    List<App_AutoMessageReplyContent> subImageText = new List<App_AutoMessageReplyContent>();
                                                    foreach (var it in itext.LibraryImageTexts)
                                                    {
                                                        App_AutoMessageReplyContent rep_it = new App_AutoMessageReplyContent();
                                                        rep_it.ID = it.ID;
                                                        rep_it.Type = (int)EnumMessageType.ImageText;
                                                        rep_it.FileTitle = it.Title;
                                                        rep_it.FileUrl = SystemConst.WebUrlIP + it.ImagePath.Replace("~", "");
                                                        //rep_it.SendTime = sendTime;
                                                        subImageText.Add(rep_it);
                                                    }
                                                    Np.Subcontent = Newtonsoft.Json.JsonConvert.SerializeObject(subImageText);
                                                }
                                            }

                                        }
                                        //处理音频MP3
                                        else if (Np.EID == "3")
                                        {
                                            Np.FielUrlMP3 = Np.FielUrl.Substring(0, Np.FielUrl.LastIndexOf('.')) + ".mp3";
                                        }
                                    }


                                    //群聊
                                    if (Np.MSD == "4")
                                    {

                                        DataTable userDT = DataBusiness.GetSidUser(int.Parse(Np.SID));
                                        foreach (DataRow row in userDT.Rows)
                                        {
                                            if (row["juid"].ToString() != msg.From.User)
                                            {
                                                //在线
                                                if (OnlineUser.onlinuser.Any(a => a.jid.User == row["juid"].ToString()))
                                                {
                                                    msg.From = jid;
                                                    if (Np.FielUrl != null)
                                                    {
                                                        Np.FielUrl = WebUrlIP + Np.FielUrl.Remove(0, 1);
                                                    }
                                                    if (Np.FielUrlMP3 != null)
                                                    {
                                                        Np.FielUrlMP3 = WebUrlIP + Np.FielUrlMP3.Remove(0, 1);
                                                    }
                                                    //转发发送消息 所有设备IEnumerable
                                                    List<XmppServerConnection> cons = OnlineUser.onlinuser.Where(a => a.jid.User == row["juid"].ToString()).ToList();
                                                    foreach (XmppServerConnection xcon in cons)
                                                    {
                                                        xcon.Send(msg, ThisMessageID);
                                                    }

                                                }
                                                //不在线
                                                else
                                                {
                                                    int UserID = int.Parse(row["userID"].ToString());
                                                    int UserType = int.Parse(row["UserType"].ToString());
                                                    DataBusiness.InsertMessageGroupChat(ThisMessageID, UserID, UserType, int.Parse(Np.SID));
                                                    PushMessage(msg, Np);

                                                }


                                            }
                                        }


                                    }
                                    //单人聊天
                                    else
                                    {

                                        //本条消息数据库ID
                                        //在线
                                        if (OnlineUser.onlinuser.Any(a => a.jid.User == msg.To.User))
                                        {
                                            msg.From = jid;
                                            if (Np.FielUrl != null)
                                            {
                                                Np.FielUrl = WebUrlIP + Np.FielUrl.Remove(0, 1);
                                            }
                                            if (Np.FielUrlMP3 != null)
                                            {
                                                Np.FielUrlMP3 = WebUrlIP + Np.FielUrlMP3.Remove(0, 1);
                                            }
                                            //转发发送消息 所有设备IEnumerable
                                            List<XmppServerConnection> cons = OnlineUser.onlinuser.Where(a => a.jid.User == msg.To.User).ToList();
                                            foreach (XmppServerConnection xcon in cons)
                                            {
                                                xcon.Send(msg, ThisMessageID);
                                            }
                                            //发给一设备
                                            //XmppServerConnection con = OnlineUser.onlinuser.Where(a => a.jid.User == msg.To.User).ToList()[0];
                                            //con.Send(msg,ThisMessageID);

                                        }
                                        //不在线
                                        else
                                        {
                                            //推送
                                            PushMessage(msg, Np);
                                        }
                                    }

                                }
                            }
                            catch
                            {
                                SendMessageStatus("6", msg.To.User);

                            }
                        }
                        //已读状态
                        else if (Np.MT == "5")
                        {
                            //修改数据库状态
                            //售楼代表s；用户u
                            string AoU = jid.User.Substring(0, 1);
                            //当前用户类型

                            //ID
                            string AoUID = jid.User.Substring(1);
                            //不是多人聊天的
                            if (Np.MSD != "4")
                            {
                                DataBusiness.UpandDelMessType(int.Parse(Np.SID), AoU, int.Parse(AoUID));
                                //在线

                                List<XmppServerConnection> cons = OnlineUser.onlinuser.Where(a => a.jid.User == msg.To.User).ToList();
                                msg.From = jid;
                                foreach (XmppServerConnection xcon in cons)
                                {
                                    xcon.Send(msg, 0);
                                }

                            }
                            //多人聊天的处理
                            else
                            {

                                int utype = 0;
                                if (AoU == "s")
                                {
                                    utype = (int)EnumClientUserType.Account;
                                }
                                else if (AoU == "u")
                                {
                                    utype = (int)EnumClientUserType.User;
                                }
                                DataBusiness.DelMessGroupChat(int.Parse(AoUID), utype, int.Parse(Np.SID));

                            }
                        }


                    }


                }
                else if (e.GetType() == typeof(IQ))
                {
                    ProcessIQ(e as IQ);
                }
            }
            catch (Exception ex)
            {
                DataBusiness.SetLog(ex);
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
                        string AoU = auth.Username.Substring(0, 1);

                        if (auth.Resource != "web")
                        {
                            //if (AoU == "u")
                            //{
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
                            //}
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
                Send(iq, 0);

                //将其他人的信息发送
                Presence pre;
                pre = new Presence();
                pre.Show = ShowType.chat;
                foreach (XmppServerConnection con in OnlineUser.onlinuser)
                {
                    pre.From = con.jid;
                    pre.Value = con.m_Sock.RemoteEndPoint.ToString();
                    Send(pre, 0);
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
                    //存储离线记录
                    if (ThisMessageID != 0)
                    {
                        //推送
                        agsXMPP.protocol.client.Message msg = n as agsXMPP.protocol.client.Message;
                        NewsProtocol Np = msg.SelectSingleElement(typeof(NewsProtocol)) as NewsProtocol;

                        PushMessage(msg, Np);
                    }
                }
            }
            catch (Exception ex)
            {
                DataBusiness.SetLog(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 回发消息状态
        /// </summary>
        /// <param name="MT">消息状态 4发送成功 6失败</param>
        public void SendMessageStatus(string MT, string FromUser)
        {
            List<XmppServerConnection> cons = OnlineUser.onlinuser.Where(a => a.jid.User == jid.User).ToList();

            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
            msg.Type = MessageType.chat;
            msg.From = new Jid(FromUser, "localhost", "resource");
            msg.To = new Jid(jid.User, "localhost", jid.User);
            NewsProtocol np = new NewsProtocol();
            np.MT = MT;//消息状态
            msg.Body = "";
            msg.AddChild(np);
            for (int i = 0; i < cons.Count(); i++)
            {
                try
                {
                    cons[i].Send(msg, 0);
                }
                catch (Exception ex)
                {
                    DataBusiness.SetLog(ex);
                }
            }

        }



        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="Np"></param>
        public void PushMessage(agsXMPP.protocol.client.Message msg, NewsProtocol Np)
        {
            try
            {
                PushModel pm = new PushModel();
                //发送人类型
                int FromType = 0;
                //接收人类型
                int ToType = 0;
                //发送人ID
                int FromUID = int.Parse(msg.From.User.Substring(1));
                //接收人ID
                int ToUID = int.Parse(msg.To.User.Substring(1));
                //消息方向
                int Msd = int.Parse(Np.MSD);
                //售楼 - 售楼
                if (Msd == (int)EnumMessageSendDirection.Account_Account)
                {
                    FromType = (int)EnumClientUserType.Account;
                    ToType = (int)EnumClientUserType.Account;
                }
                //售楼 - 用户
                else if (Msd == (int)EnumMessageSendDirection.Account_User)
                {
                    FromType = (int)EnumClientUserType.Account;
                    ToType = (int)EnumClientUserType.User;
                }
                //用户 - 售楼
                else if (Msd == (int)EnumMessageSendDirection.User_Account)
                {
                    FromType = (int)EnumClientUserType.User;
                    ToType = (int)EnumClientUserType.Account;
                }
                //用户 - 用户
                else if (Msd == (int)EnumMessageSendDirection.User_User)
                {
                    FromType = (int)EnumClientUserType.User;
                    ToType = (int)EnumClientUserType.User;
                }

                pm.PushFromChat((EnumMessageType)int.Parse(Np.EID), msg.Body, (EnumClientUserType)ToType, ToUID, (EnumClientUserType)FromType, FromUID);
            }
            catch (Exception ex)
            {
                DataBusiness.SetLog(ex);
            }
        }



    }
}
