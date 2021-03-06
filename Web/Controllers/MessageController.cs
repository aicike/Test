﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Injection;
using Poco;
using Controllers;
using Business;
using Poco.Enum;

namespace Web.Controllers
{
    public class MessageController : ManageAccountController
    {
        public ActionResult Index()
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var groupList = groupModel.GetGroupListByAccountID(LoginAccount.ID, null);
            ViewBag.GroupList = groupList;
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "消息管理-新建消息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        public string SendText(int libraryID, string url, string content, string type, string sendType, string uids)
        {
            PushModel pushModel = new PushModel();
            Result result = null;
            switch (type)
            {
                case "text":
                    result = pushModel.Push(EnumMessageType.Text, null, null, content, sendType, LoginAccount.ID, uids, LoginAccount.CurrentAccountMainID);
                    break;
                case "image":
                    result = pushModel.Push(EnumMessageType.Image, libraryID, url, content, sendType, LoginAccount.ID, uids, LoginAccount.CurrentAccountMainID);
                    break;
                case "voice":
                    result = pushModel.Push(EnumMessageType.Voice, libraryID, url, content, sendType, LoginAccount.ID, uids, LoginAccount.CurrentAccountMainID);
                    break;
                case "video":
                    result = pushModel.Push(EnumMessageType.Video, libraryID, url, content, sendType, LoginAccount.ID, uids, LoginAccount.CurrentAccountMainID);
                    break;
                case "imagetext":
                    result = pushModel.Push(EnumMessageType.ImageText, libraryID, url, content, sendType, LoginAccount.ID, uids, LoginAccount.CurrentAccountMainID);
                    break;
            }
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "Message", new { HostName = LoginAccount.HostName }) + "'";
        }

        public ActionResult History(int? id)
        {
            IPushMsgModel model = Factory.Get<IPushMsgModel>(SystemConst.IOC_Model.PushMsgModel);
            var result = model.GetList(LoginAccount.CurrentAccountMainID, LoginAccount.ID).ToPagedList(id ?? 1, 15);
            var list = GetMessageTitle(result.ToList());
            ViewBag.List = list;
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "消息管理-已发送消息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(result);
        }

        public ActionResult Delete(int id)
        {
            IPushMsgModel model = Factory.Get<IPushMsgModel>(SystemConst.IOC_Model.PushMsgModel);
            var result = model.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("History", "Message", new { HostName = LoginAccount.HostName }) + "'");
        }

        public List<Poco.PushMsg> GetMessageTitle(List<Poco.PushMsg> pmlist)
        {
            foreach (var pm in pmlist)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int length = 20;
                switch (pm.EnumMessageType)
                {
                    case (int)Poco.Enum.EnumMessageType.Text:
                        sb.AppendFormat("[文字] {0}", pm.Text.Length > length ? pm.Text.Substring(0, length) : pm.Text);
                        break;
                    case (int)Poco.Enum.EnumMessageType.Image:
                        var libraryImageModel = Injection.Factory.Get<Interface.ILibraryImageModel>(Poco.SystemConst.IOC_Model.LibraryImageModel);
                        var msg = libraryImageModel.Get(pm.LibraryID.Value);
                        if (msg != null)
                        {
                            sb.AppendFormat("[图片] <img src='{0}' style='width:80px' />", Url.Content(msg.FilePath ?? ""));
                        }
                        break;
                    case (int)Poco.Enum.EnumMessageType.Video:
                        var libraryVideoModel = Injection.Factory.Get<Interface.ILibraryVideoModel>(Poco.SystemConst.IOC_Model.LibraryVideoModel);
                        var vid = libraryVideoModel.Get(pm.LibraryID.Value);
                        if (vid != null)
                        {
                            sb.AppendFormat("[视频] {0}", vid.FileName);
                        }
                        break;
                    case (int)Poco.Enum.EnumMessageType.Voice:
                        var libraryVoiceModel = Injection.Factory.Get<Interface.ILibraryVoiceModel>(Poco.SystemConst.IOC_Model.LibraryVoiceModel);
                        var voi = libraryVoiceModel.Get(pm.LibraryID.Value);
                        if (voi != null)
                        {
                            sb.AppendFormat("[语音] {0}", voi.FileName);
                        }
                        break;
                    case (int)Poco.Enum.EnumMessageType.ImageText:
                        var libraryImageTextModel = Injection.Factory.Get<Interface.ILibraryImageTextModel>(Poco.SystemConst.IOC_Model.LibraryImageTextModel);
                        var it = libraryImageTextModel.Get(pm.LibraryID.Value);
                        if (it != null)
                        {
                            sb.AppendFormat("[图文] {0}", it.Title);
                        }
                        break;
                }
                pm.HtmlShow = sb.ToString();
            }
            return pmlist;
        }
    }
}
