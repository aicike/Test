﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Poco;
using Interface;
using System.Data;
using Poco.Enum;
using System.Threading.Tasks;
using agsXMPP;
using System.Threading;
using agsXMPP.protocol.client;

namespace Web.Controllers
{
    public class SingleChatController : ManageAccountController
    {
        //
        // GET: /SingleChat/

        private XmppClientConnection Connection;


        //聊天页面
        public ActionResult Index(int id, int userID)
        {
            //判断是否与当前用户有聊天权限
            var AccountUserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            bool isOk = AccountUserModel.ChickUserInAccount(LoginAccount.ID, userID);
            isOk.NotAuthorizedPage();
            //当前聊天人
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = userModel.Get(userID);
            ViewBag.UName = user.Name; //当前聊天人名称
            ViewBag.HostName = LoginAccount.HostName;

            ViewBag.UserID = userID;
            ViewBag.ID = id;
            return View();
        }

        [AllowCheckPermissions(false)]
        public ActionResult SendMessage(int id, int userID, string Content)
        {
            TreedCon();
            Thread.Sleep(1000);
            try
            {
                agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                msg.Type = MessageType.chat;
                msg.Body = Content;
                msg.From = new Jid("s" + LoginAccount.ID, "localhost", "resource");
                msg.To = new Jid("u" + userID, "localhost", "s" + LoginAccount.ID);
                NewsProtocol np = new NewsProtocol();
                np.MSD = "0"; //->购房-->售楼
                np.MT = "1";  //消息
                np.EID = "0"; //文本
                msg.AddChild(np);
                Connection.Send(msg);
                Presence p = new Presence();
                p.Type = PresenceType.unavailable;
                Connection.Send(p);
            }
            catch
            {
                //发送失败
            }
            Thread.Sleep(1000);

            return RedirectToAction("ChatPartialView", new { id = id, userID = userID });
        }

        [AllowCheckPermissions(false)]
        public ActionResult ChatPartialView(int id, int userID)
        {
            //获取聊天记录
            var MessageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            var message = MessageModel.GetList(LoginAccount.ID, userID).ToPagedList(id, 30);

            //修改记录状态 并 删除待处理数据
            MessageModel.UpAndDelData(LoginAccount.ID, userID);
            ViewBag.HostName = LoginAccount.HostName;
            return View(message);
        }

        [AllowCheckPermissions(false)]
        public ActionResult ReadMessage( int id,int userID)
        {
            return RedirectToAction("ChatPartialView", new { id = id, userID = userID });
        }

        [AllowCheckPermissions(false)]
        public String CheckMessage()
        {
            if (Session["IsHaveMessage"] != null)
            {
                if (Session["IsHaveMessage"].ToString() == "1")
                {
                    Session["IsHaveMessage"] = "0";
                    return "1";
                }
            }
            return "0";
        }

        //登陆XMPP
        public void TreedCon()
        {

            //连接xmpp
            if (Connection == null)
            {

                Task t = new Task(() =>
                {
                    Connection = new XmppClientConnection();
                    //登陆方法
                    Connection.OnLogin += new ObjectHandler((object sender) =>
                    {

                        Connection.Show = ShowType.chat;
                        Connection.SendMyPresence();
                    }
                    );
                    //Connection.OnMessage += new XmppClientConnection.MessageHandler(con_OnMessage);
                    //出息消息
                    Connection.Username = "s" + LoginAccount.ID;
                    Connection.Server = "127.0.0.1";
                    Connection.Port = 5222;
                    Connection.Resource = "Resource";

                    Thread mythread = new Thread(connect);
                    mythread.Start();
                    mythread.IsBackground = true;

                });
                t.Start();
            }
        }

        //xmpp连接
        void connect()
        {
            Connection.Open();
        }


    }
}