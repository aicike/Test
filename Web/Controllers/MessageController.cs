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
            return View();
        }

        public string SendText(string content, string text)
        {
            switch (text)
            {
                case "text":
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




            string CLIENTID = "03b2ac5b2c55619f7c29f87eabff771f";
            PushMessage message = new PushMessage();
            message.Title = "测试推送";
            message.Text = content;
            var result = Push_Getui.SendMessage(message, CLIENTID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "Message", new { HostName = LoginAccount.HostName }) + "'";
        }
    }
}
