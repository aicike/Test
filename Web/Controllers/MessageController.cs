using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Injection;
using Poco;
using Controllers;
using Business;

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
            return View();
        }

        public string SendText(string content, string type, string sendType, string uids)
        {
            PushModel pushModel = new PushModel();
            switch (type)
            {
                case "text":
                    pushModel.Push_Text(content, sendType, LoginAccount.ID, uids, LoginAccount.CurrentAccountMainID);
                    break;
                case "image":
                    break;
                case "voice":
                    break;
                case "video":
                    break;
                case "imagetext":
                    break;
            }

            return "window.location.href='" + Url.Action("Index", "Message", new { HostName = LoginAccount.HostName }) + "'";
        }

        public ActionResult History()
        {
            return View();
        }
    }
}
