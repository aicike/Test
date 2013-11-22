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
        public ActionResult Index(int ?id)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var list = roleModel.GetListByAMID(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);


            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-添加角色", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }
        public ActionResult Edit()
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-修改角色", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }
        public ActionResult Delete()
        {
            return JavaScript("window.location.href='" + Url.Action("Index", "Character", new { HostName = LoginAccount.HostName }) + "'");
        }
        public ActionResult Power()
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-权限控制", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }
    }
}
