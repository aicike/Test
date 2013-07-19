using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Injection;
using Poco;
using Controllers;

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

    }
}
