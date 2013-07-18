using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;

namespace Web.Controllers
{
    public class UserManageController : ManageAccountController
    {
        public ActionResult Index(int? groupID, int? index)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var groupList = groupModel.GetGroupListByAccountID(LoginAccount.ID, null);
            ViewBag.GroupList = groupList;
            int defaultGroupID = groupList.Where(a => a.IsDefaultGroup).FirstOrDefault().ID;
            groupID = groupID ?? defaultGroupID;
            ViewBag.DefaultGroupID = defaultGroupID;
            ViewBag.GroupID = groupID;
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.LoginAccountID = LoginAccount.ID;
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var userList = userModel.GetUserByAccountID(LoginAccount.ID, groupID.Value).ToPagedList(index ?? 1, 15);
            return View(userList);
        }

        public string AddGroup(string groupName)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var result = groupModel.AddGroup(groupName, LoginAccount.ID, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return string.Format("location.href='{0}'", Url.Action("Index", "UserManage", new { HostName = LoginAccount.HostName }));
        }

        public string EditGroup(int groupID, string groupName)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var result = groupModel.EditGroup(groupID, groupName, LoginAccount.ID, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return string.Format("location.href='{0}'", Url.Action("Index", "UserManage", new { HostName = LoginAccount.HostName }));
        }

        public string DeleteGroup(int groupID)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var result= groupModel.DeleteGroup(groupID, LoginAccount.ID, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return string.Format("location.href='{0}'", Url.Action("Index", "UserManage", new { HostName = LoginAccount.HostName }));
        }
    }
}
