using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class RepairchargesoController : ManageAccountController
    {
        //
        // GET: /Repairchargeso/

        public ActionResult Index(int? id)
        {
            var repairchargesoModel = Factory.Get<IRepairchargesoModel>(SystemConst.IOC_Model.RepairchargesoModel);
            var repairchargeso = repairchargesoModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 20);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-收费维修", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(repairchargeso);
        }

        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-收费维修-添加信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Repairchargeso repairchargeso)
        {
            var repairchargesoModel = Factory.Get<IRepairchargesoModel>(SystemConst.IOC_Model.RepairchargesoModel);
            repairchargeso.GetDate = DateTime.Now;
            repairchargeso.AccountMainID = LoginAccount.CurrentAccountMainID;
            repairchargesoModel.Add(repairchargeso);
            return RedirectToAction("Index", "Repairchargeso");
        }

        public ActionResult Edit(int ID)
        {
            var repairchargesoModel = Factory.Get<IRepairchargesoModel>(SystemConst.IOC_Model.RepairchargesoModel);
            var repairchargeso = repairchargesoModel.Getinfo(ID,LoginAccount.CurrentAccountMainID);
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-收费维修-修改信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(repairchargeso);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Repairchargeso repairchargeso)
        {
            var repairchargesoModel = Factory.Get<IRepairchargesoModel>(SystemConst.IOC_Model.RepairchargesoModel);
            var result = repairchargesoModel.Edit(repairchargeso);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return RedirectToAction("Index", "Repairchargeso");
        }

        public ActionResult Delete(int ID)
        {
            var repairchargesoModel = Factory.Get<IRepairchargesoModel>(SystemConst.IOC_Model.RepairchargesoModel);
            var result = repairchargesoModel.CompleteDelete(ID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "Repairchargeso", new { HostName = LoginAccount.HostName }) + "'");
        }
    }
}
