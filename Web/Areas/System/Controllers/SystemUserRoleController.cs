using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Interface;
using Injection;
using Poco;

namespace Web.Areas.System.Controllers
{
    public class SystemUserRoleController : ManageSystemUserController
    {
        public ActionResult Index(int? index)
        {
            ISystemUserRoleModel roleModel = Factory.Get<ISystemUserRoleModel>(SystemConst.IOC_Model.SystemUserRoleModel);
            var list = roleModel.List().Where(a => a.ID != 1).ToPagedList(index ?? 1, 15);
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(SystemUserRole role)
        {
            (role.ID != 1).NotAuthorizedPage();
            ISystemUserRoleModel roleModel = Factory.Get<ISystemUserRoleModel>(SystemConst.IOC_Model.SystemUserRoleModel);
            var result = roleModel.Add(role);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "SystemUserRole", new { Area = "System" }) + "'");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            (id != 1).NotAuthorizedPage();
            ISystemUserRoleModel roleModel = Factory.Get<ISystemUserRoleModel>(SystemConst.IOC_Model.SystemUserRoleModel);
            var result = roleModel.Get(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(SystemUserRole role)
        {
            (role.ID != 1).NotAuthorizedPage();
            ISystemUserRoleModel roleModel = Factory.Get<ISystemUserRoleModel>(SystemConst.IOC_Model.SystemUserRoleModel);
            var result = roleModel.Edit(role);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "SystemUserRole", new { Area = "System" }) + "'");
        }

        public ActionResult Delete(int id)
        {
            (id != 1).NotAuthorizedPage();
            ISystemUserRoleModel roleModel = Factory.Get<ISystemUserRoleModel>(SystemConst.IOC_Model.SystemUserRoleModel);
            var result = roleModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "SystemUserRole", new { Area = "System" }) + "'");
        }

        public ActionResult Permission(int id)
        {
            (id != 1).NotAuthorizedPage();
            ISystemUserMenuModel systemUserMenuModel = Factory.Get<ISystemUserMenuModel>(SystemConst.IOC_Model.SystemUserMenuModel);
            var menus = systemUserMenuModel.GetMenuByRoleID(1);
            ISystemUserRoleModel roleModel = Factory.Get<ISystemUserRoleModel>(SystemConst.IOC_Model.SystemUserRoleModel);
            var role = roleModel.Get(id);
            ViewBag.Role = role;
            //当前角色所操作的菜单
            var currentRoleMenuList = systemUserMenuModel.GetAllMenuByRoleID(id).Select(a => a.ID).ToList();
            ViewBag.CurrentRoleMenuList = currentRoleMenuList;
            //当前角色所操作的功能
            ISystemUserMenuOptionModel systemUserMenuOptionModel = Factory.Get<ISystemUserMenuOptionModel>(SystemConst.IOC_Model.SystemUserMenuOptionModel);
            var currentRoleOptionList = systemUserMenuOptionModel.GetAllOptionByRoleID(id).Select(a => a.ID).ToList();
            ViewBag.CurrentRoleOptionList = currentRoleOptionList;
            ViewBag.RawUrl = Url.Action("Index", "SystemUserRole", new { Area = "System" });
            return View(menus);
        }

        [HttpPost]
        public ActionResult Permission()
        {
            var roleID = Convert.ToInt32(Request["hidRoleID"]);
            (roleID != 1).NotAuthorizedPage();

            var checkboxMenu = Request["checkboxMenu"];
            var checkboxOption = Request["checkboxOption"];

            int[] menuIDArray = null;
            if (!string.IsNullOrEmpty(checkboxMenu))
            {
                menuIDArray = checkboxMenu.Split(',').ConvertToIntArray();
            }

            int[] optionIDArray = null;
            if (!string.IsNullOrEmpty(checkboxOption))
            {
                optionIDArray = checkboxOption.Split(',').ConvertToIntArray();
            }
            try
            {
                ISystemUserRole_SystemUserMenuModel systemUserRole_SystemUserMenuModel = Factory.Get<ISystemUserRole_SystemUserMenuModel>(SystemConst.IOC_Model.SystemUserRole_SystemUserMenuModel);
                systemUserRole_SystemUserMenuModel.BindPermission(roleID, menuIDArray, optionIDArray);
            }
            catch (Exception ex)
            {
                return Alert(new Dialog(ex.Message));
            }
            return Content(AlertJS(new Dialog("绑定成功。", Url.Action("Permission", "SystemUserRole", new { Area = "System", id = roleID }))));
        }

    }
}
