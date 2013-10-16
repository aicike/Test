using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Business;
using Poco.Enum;

namespace Web.Controllers
{
    public class HousesMangeController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var list = hounsesModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        public ActionResult Select(int id)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounses = hounsesModel.Get(id);
            //建筑类型
            var LookupBuilding = LookupFactory.GetLookupOptionList(typeof(EnumBuildingType));
            ViewBag.Building = LookupBuilding;
            //装修类型
            var LookupDecoration = LookupFactory.GetLookupOptionList(typeof(EnumDecoration));
            ViewBag.Decoration = LookupDecoration;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理-项目详细", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(Hounses);
        }

        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;
            //建筑类型
            var LookupBuilding = LookupFactory.GetLookupOptionList(typeof(EnumBuildingType));
            ViewBag.Building = LookupBuilding;
            //装修类型
            var LookupDecoration = LookupFactory.GetLookupOptionList(typeof(EnumDecoration));
            ViewBag.Decoration = LookupDecoration;
            ViewBag.AccountMainID = LoginAccount.CurrentAccountMainID;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理-添加项目", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        public ActionResult Add(AccountMainHouses Hounses)
        {
            Hounses.AccountMainID = LoginAccount.CurrentAccountMainID;
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            Hounses.BuildingType = Request.Form["BuildingType"];
            Hounses.Decoration = Request.Form["Decoration"];
            var result = hounsesModel.Add(Hounses);
            if (result.HasError)
            {
                return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HousesMange", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Edit(int id)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounses = hounsesModel.Get(id);
            ViewBag.HostName = LoginAccount.HostName;
            //建筑类型
            var LookupBuilding = LookupFactory.GetLookupOptionList(typeof(EnumBuildingType));
            ViewBag.Building = LookupBuilding;
            //装修类型
            var LookupDecoration = LookupFactory.GetLookupOptionList(typeof(EnumDecoration));
            ViewBag.Decoration = LookupDecoration;
            ViewBag.AccountMainID = LoginAccount.CurrentAccountMainID;
            ViewBag.zt = Hounses.SalesState;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理-修改项目", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            

            return View(Hounses);
        }

        [HttpPost]
        public ActionResult Edit(AccountMainHouses Hounses)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            Hounses.BuildingType = Request.Form["BuildingType"];
            Hounses.Decoration = Request.Form["Decoration"];
            var result = hounsesModel.Edit(Hounses);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HousesMange", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Delete(int id)
        {
            var AutoMessageModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            if (AutoMessageModel.GetKeyByHouseID(id))
            { 
                return   Alert(new Dialog("此项目已经设置 “关键词自动回复” 不能删除！"));
            }

            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var result = hounsesModel.DelteAll(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HousesMange", new { HostName = LoginAccount.HostName }) + "'");
        }
    }
}
