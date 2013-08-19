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
    public class RoleController : ManageSystemUserController
    {
        public ActionResult Index(int? index)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var list = roleModel.List().ToPagedList(index ?? 1, 15);
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Role role)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.Add(role);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Role", new { Area = "System" }) + "'");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.Get(id);
            result.IsCanDelete.NotAuthorizedPage();
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.Edit(role);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Role", new { Area = "System" }) + "'");
        }

        [AllowCheckPermissions(false)]
        public string IsCanFindByUser(int id, bool value)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.IsCanFindByUser(id, value);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "Role", new { Area = "System" }) + "'";
        }

        public ActionResult Delete(int id)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Role", new { Area = "System" }) + "'");
        }

        public ActionResult Permission(int id)
        {
            IMenuModel menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
            var menus = menuModel.List_Cache().Where(a => a.ParentMenuID.HasValue == false).OrderBy(a => a.Order).ToList();
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var role = roleModel.Get(id);
            ViewBag.Role = role;
            //当前角色所操作的菜单
            var currentRoleMenuList = menuModel.GetAllMenuByRoleID(id).Select(a => a.ID).ToList();
            ViewBag.CurrentRoleMenuList = currentRoleMenuList;
            //当前角色所操作的功能
            IMenuOptionModel menuOptionModel = Factory.Get<IMenuOptionModel>(SystemConst.IOC_Model.MenuOptionModel);
            var currentRoleOptionList = menuOptionModel.GetAllOptionByRoleID(id).Select(a => a.ID).ToList();
            ViewBag.CurrentRoleOptionList = currentRoleOptionList;
            ViewBag.RawUrl = Url.Action("Index", "Role", new { Area = "System" });
            return View(menus);
        }

        [HttpPost]
        public ActionResult Permission()
        {
            var roleID = Convert.ToInt32(Request["hidRoleID"]);

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
                IRoleMenuModel roleMenuModel = Factory.Get<IRoleMenuModel>(SystemConst.IOC_Model.RoleMenuModel);
                roleMenuModel.BindPermission(roleID, menuIDArray, optionIDArray);
            }
            catch (Exception ex)
            {
                return Alert(new Dialog(ex.Message));
            }
            return Content(AlertJS(new Dialog("绑定成功。", Url.Action("Permission", "Role", new { Area = "System", id = roleID }))));
        }
    }
}
