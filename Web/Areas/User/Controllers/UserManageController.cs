using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;

namespace Web.Areas.User.Controllers
{
    public class UserManageController : ManageAccountController
    {
        public ActionResult Index(int? groupID, int? index)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var groupList = groupModel.GetGroupListByAccountID(LoginAccount.ID, null);
            ViewBag.GroupList = groupList;
            int defaultGroupID = groupList.FirstOrDefault().ID;
            ViewBag.GroupID = defaultGroupID;
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            groupID = groupID ?? defaultGroupID;
            var userList = userModel.GetUserByAccountID(LoginAccount.ID, groupID.Value).ToPagedList(index ?? 1, 15);
            return View(userList);
        }

    }
}
