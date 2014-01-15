using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Interface;
using Injection;

namespace Web.Controllers
{
    public class CharacterController : ManageAccountController
    {
        //
        // GET: /Character/
        public ActionResult Index(int? id)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var list = roleModel.GetListNoAdmin(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);


            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
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
            return "window.location.href='" + Url.Action("Index", "Character", new { HostName = LoginAccount.HostName }) + "'";
        }

        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-添加角色", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }
        [HttpPost]
        public ActionResult Add(Role role)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            role.IsCanDelete = true;
            role.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = roleModel.Add(role);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Character", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Edit(int id)
        {

            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.Get(id);
            result.IsCanDelete.NotAuthorizedPage();


            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-修改角色", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
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
            return JavaScript("window.location.href='" + Url.Action("Index", "Character", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Delete(int id)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.DelteByIDAndAMID(id, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Character", new { HostName = LoginAccount.HostName }) + "'");
        }

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        public ActionResult Power(int id)
        {

            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            IMenuModel menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
            IMenuOptionModel menuOptionModel = Factory.Get<IMenuOptionModel>(SystemConst.IOC_Model.MenuOptionModel);
            //当前角色
            var role = roleModel.Get(id);
            ViewBag.Role = role;

            //获取当前管理员角色ID
            //int AdminPID = roleModel.GetRoleIscandelete(LoginAccount.CurrentAccountMainID);

            //管理员角色所操作的菜单
            //var AdminRoleMenuList = menuModel.GetAllMenuByRoleID(AdminPID).Select(a => a.ID).ToList();
            var AdminRoleMenuList = menuModel.MicroSite_GetAllMenuByRoleID().Select(a => a.ID).ToList();

            //管理员角色所操作的功能
            //var AdminRoleOptionList = menuOptionModel.GetAllOptionByRoleID(AdminPID).Select(a => a.ID).ToList();
            var AdminRoleOptionList = menuOptionModel.List_Cache().Select(a => a.ID).ToList();

            ViewBag.AdminMenyOptionIDS = AdminRoleOptionList;

            //可分配的菜单
            var menus = menuModel.List_Cache().Where(a => AdminRoleMenuList.Contains(a.ID) && a.ParentMenuID.HasValue == false).OrderBy(a => a.Order).ToList();
            //可分配的二级菜单ID
            //var menusOption = menuModel.List_Cache().Where(a => AdminRoleOptionList.Contains(a.ID) && a.ParentMenuID.HasValue).Select(a => a.ID).ToList();
            var menusOption = menuModel.List_Cache().Where(a => AdminRoleMenuList.Contains(a.ID) && a.ParentMenuID.HasValue).Select(a => a.ID).ToList();
            ViewBag.AdminMenyIDS = menusOption;


            //当前角色所操作的菜单
            var currentRoleMenuList = menuModel.GetAllMenuByRoleID(id).Select(a => a.ID).ToList();
            ViewBag.CurrentRoleMenuList = currentRoleMenuList;
            //当前角色所操作的功能
            var currentRoleOptionList = menuOptionModel.GetAllOptionByRoleID(id).Select(a => a.ID).ToList();
            ViewBag.CurrentRoleOptionList = currentRoleOptionList;



            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-权限控制", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(menus);
        }

        [HttpPost]
        public ActionResult Power()
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
            return Content(AlertJS(new Dialog("绑定成功。", Url.Action("Power", "Character", new { id = roleID }))));
        }
    }
}
