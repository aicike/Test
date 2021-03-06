﻿using System;
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
        public ActionResult Index(int? id,int? groupID)
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
            var userList = userModel.GetUserByAccountID(LoginAccount.ID, groupID.Value).ToPagedList(id ?? 1, 15);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "用户管理-分组", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(userList);
        }

        public string AddGroup(string groupName, int currentGroupID)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var result = groupModel.AddGroup(groupName, LoginAccount.ID, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return string.Format("location.href='{0}'", Url.Action("Index", "UserManage", new { HostName = LoginAccount.HostName, groupID = currentGroupID }));
        }

        public string EditGroup(int groupID, string groupName, int currentGroupID)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var result = groupModel.EditGroup(groupID, groupName, LoginAccount.ID, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }

            
            return string.Format("location.href='{0}'", Url.Action("Index", "UserManage", new { HostName = LoginAccount.HostName, groupID = currentGroupID }));
        }

        public string DeleteGroup(int groupID, int currentGroupID)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var result = groupModel.DeleteGroup(groupID, LoginAccount.ID, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return string.Format("location.href='{0}'", Url.Action("Index", "UserManage", new { HostName = LoginAccount.HostName }));
        }

        public string ChangeGroup(int userID, int groupID, int currentGroupID)
        {
            var accountUserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            var result = accountUserModel.ChangeGroup(userID, LoginAccount.ID, groupID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return string.Format("location.href='{0}'", Url.Action("Index", "UserManage", new { HostName = LoginAccount.HostName, groupID = currentGroupID }));
        }

        public ActionResult EditUser(int userID)
        {
            //检查是否有数据操作权限
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            accountModel.CheckHasPermissions_User(LoginAccount.ID, userID).NotAuthorizedPage();

            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var groupList = groupModel.GetGroupListByAccountID(LoginAccount.ID, null);
            ViewBag.GroupList = groupList;
            ViewBag.HostName = LoginAccount.HostName;

            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = userModel.Get(userID);

            var usertagModel = Factory.Get<IUserTagModel>(SystemConst.IOC_Model.UserTagModel);
            ViewBag.UserTag = usertagModel.List(true);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "用户管理-修改用户信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            //检查是否有数据操作权限
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            accountModel.CheckHasPermissions_User(LoginAccount.ID, user.ID).NotAuthorizedPage();

            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var result= userModel.UpdateUserInfo(user);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript(string.Format("location.href='{0}'", Url.Action("Index", "UserManage", new { HostName = LoginAccount.HostName })));
        }

        public ActionResult ViewUser(int userID)
        {
            //检查是否有数据操作权限
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            accountModel.CheckHasPermissions_User(LoginAccount.ID, userID).NotAuthorizedPage();

            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var groupList = groupModel.GetGroupListByAccountID(LoginAccount.ID, null);
            ViewBag.GroupList = groupList;
            ViewBag.HostName = LoginAccount.HostName;

            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = userModel.Get(userID);
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "用户管理-用户信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(user);
        }
    }
}
