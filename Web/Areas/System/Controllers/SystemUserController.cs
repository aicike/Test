using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.Enum;
using Web.Controllers;

namespace Web.Areas.System.Controllers
{
    public class SystemUserController : ManageSystemUserController
    {
        public ActionResult Index(int? index)
        {
            var systemUserModel = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
            var pageList = systemUserModel.List().Where(a => a.SystemUserRoleID != 1).OrderBy(a => a.ID).ToPagedList(index ?? 1, 15);
            return View(pageList);
        }

        public ActionResult SetStatus(int ID, string status)
        {
            var systemUserModel = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
            EnumAccountStatus systemUserStatus = (EnumAccountStatus)Enum.Parse(typeof(EnumAccountStatus), status);
            var result = systemUserModel.ChangeStatus(ID, systemUserStatus);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }

        public ActionResult Add()
        {
            var systemUserRoleModel = Factory.Get<ISystemUserRoleModel>(SystemConst.IOC_Model.SystemUserRoleModel);
            var roles = systemUserRoleModel.GetRoleWithoutSuperAdmin();
            var selectListRoles = new SelectList(roles, "ID", "Name");
            List<SelectListItem> newRolesList = new List<SelectListItem>();
            newRolesList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newRolesList.AddRange(selectListRoles);
            ViewData["Roles"] = newRolesList;
            return View();
        }

        [HttpPost]
        public ActionResult Add(SystemUser user, HttpPostedFileBase HeadImagePathFile)
        {
            var systemUserModel = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
            var result = systemUserModel.Add(user, HeadImagePathFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "SystemUser", new { Area = "System" });
        }

        public ActionResult Edit(int id)
        {
            var model = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
            var entity = model.Get(id);

            var systemUserRoleModel = Factory.Get<ISystemUserRoleModel>(SystemConst.IOC_Model.SystemUserRoleModel);
            var roles = systemUserRoleModel.GetRoleWithoutSuperAdmin();
            var selectListRoles = new SelectList(roles, "ID", "Name");
            List<SelectListItem> newRolesList = new List<SelectListItem>();
            newRolesList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newRolesList.AddRange(selectListRoles);
            ViewData["Roles"] = newRolesList;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(SystemUser user, HttpPostedFileBase HeadImagePathFile)
        {
            var model = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
            var result = model.Edit(user, HeadImagePathFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "SystemUser", new { Area = "System"});
        }

        public ActionResult Delete(int id)
        {
            var model = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
            var result = model.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }
    }
}
