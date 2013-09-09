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
using System.Configuration;

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
            //获取会话ID
            var ConversationModel = Factory.Get<IConversationModel>(SystemConst.IOC_Model.ConversationModel);
            var Conver = ConversationModel.GetCID(LoginAccount.CurrentAccountMainID.ToString(), LoginAccount.ID.ToString(), userID.ToString(), "0");
            ViewBag.ConverID = Conver;
            //当前聊天人
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = userModel.Get(userID);
            ViewBag.UName = user.Name; //当前聊天人名称
            ViewBag.HostName = LoginAccount.HostName;

            ViewBag.UserID = userID;
            ViewBag.ID = id;
            return View();
        }

        //历史聊天页面
        [AllowCheckPermissions(false)]
        public ActionResult HistoryMes(int? id, int userID)
        {
            //判断是否与当前用户有聊天权限
            var AccountUserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            bool isOk = AccountUserModel.ChickUserInAccount(LoginAccount.ID, userID);
            isOk.NotAuthorizedPage();

            //客户姓名
            var UserModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var User = UserModel.Get(userID);
            if (User.Name == User.UserLoginInfo.Name)
            {
                ViewBag.UserName = User.Name;
            }
            else
            {
                ViewBag.UserName = User.UserLoginInfo.Name + "(" + User.Name + ")";
            }
            //获取聊天记录
            var MessageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            var message = MessageModel.GetHistoryList(LoginAccount.ID, userID).ToPagedList(id ?? 1, 30);

            return View(message);
        }


        [AllowCheckPermissions(false)]
        public ActionResult SendMessage(int id, int userID, string Content, string MesType, string TypePath, string MesAddress, HttpPostedFileBase TypeImagePathFile, string imgtextID,string SID)
        {
            TreedCon();
            Thread.Sleep(1000);
            try
            {
                if (Connection != null)
                {
                    agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                    msg.Type = MessageType.chat;
                    msg.From = new Jid("s" + LoginAccount.ID, "localhost", "resource");
                    msg.To = new Jid("u" + userID, "localhost", "s" + LoginAccount.ID);

                    NewsProtocol np = new NewsProtocol();
                    np.MSD = ((int)EnumMessageSendDirection.Account_User).ToString(); //售楼->购房
                    np.MT = "1";  //消息
                    np.EID = MesType; //消息类型
                    np.SID = SID;//会话ID
                    np.Sendtime = DateTime.Now.ToString(SystemConst.Business.TimeFomatFull);//发送时间
                    //文本
                    if (MesType == ((int)EnumMessageType.Text).ToString())
                    {
                        msg.Body = Content;
                    }
                    //图片
                    else if (MesType == ((int)EnumMessageType.Image).ToString())
                    {
                        //本地
                        if (MesAddress == "1")
                        {
                            string UpFilePath = ConfigurationManager.AppSettings["UpLodeFile"].ToString();
                            var path = string.Format(string.Format("{0}{1}.Message/Account/{2}/Image", UpFilePath, LoginAccount.CurrentAccountMainID, LoginAccount.ID));
                            if (!System.IO.File.Exists(Server.MapPath(path)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath(path));
                            }
                            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                            var imageName = string.Format("{0}_{1}", token, TypeImagePathFile.FileName);
                            var imagePath = string.Format("{0}\\{1}", path, imageName);
                            var imagePath2 = Server.MapPath(imagePath);
                            TypeImagePathFile.SaveAs(imagePath2);
                            np.FielUrl = imagePath;
                        }
                        else
                        {
                            np.FielUrl = TypePath;
                        }
                    }
                    //语音
                    else if (MesType == ((int)EnumMessageType.Voice).ToString())
                    {
                        np.FielUrl = TypePath;
                    }
                    //视频
                    else if (MesType == ((int)EnumMessageType.Video).ToString())
                    {
                        np.FielUrl = TypePath;
                    }
                    //图文
                    else if (MesType == ((int)EnumMessageType.ImageText).ToString())
                    {
                        np.ImgTextID = imgtextID;
                    }


                    msg.AddChild(np);
                    Connection.Send(msg);
                    Presence p = new Presence();
                    p.Type = PresenceType.unavailable;
                    Connection.Send(p);
                }
                else
                {
                    //发送失败
                }
            }
            catch
            {
                //发送失败
            }
            if (MesType != ((int)EnumMessageType.Text).ToString())
            {
                return RedirectToAction("index", new { id = id, userID = userID });
            }
            else
            {
                return RedirectToAction("ChatPartialView", new { id = id, userID = userID, SID = SID });
            }
        }

        [AllowCheckPermissions(false)]
        public ActionResult ChatPartialView(int id, int userID,int SID)
        {
            //获取聊天记录
            var MessageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            var message = MessageModel.GetList(SID).ToPagedList(id, 30);

            //修改记录状态 并 删除待处理数据
            MessageModel.UpAndDelData(LoginAccount.ID, userID);
            ViewBag.HostName = LoginAccount.HostName;
            return View(message);
        }

        [AllowCheckPermissions(false)]
        public ActionResult ReadMessage(int id, int userID,int SID)
        {
            return RedirectToAction("ChatPartialView", new { id = id, userID = userID, SID = SID });
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

        [AllowCheckPermissions(false)]
        public JsonResult GetImgTextMessage(int ImgTextID)
        {
            var ImgTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var imgText = ImgTextModel.Get(ImgTextID);

            return Json(new LibraryImageText()
            {
                ID=imgText.ID,
                Title = imgText.Title,
                Content = imgText.Content,
                ImagePath = Url.Content(imgText.ImagePath)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
