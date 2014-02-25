using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Poco.Enum;
using Poco;
using Interface;
using Injection;
using System.Data;
using System.Collections;

namespace Web.Areas.System.Controllers
{
    public class ReportController : ManageSystemUserController
    {
        //
        // GET: /System/Report/

        public ActionResult Index(int accountMainId)
        {
            ViewBag.AccountMainID = accountMainId;
            //展示列表
            DataTable dicDT = new DataTable();
            dicDT.Columns.Add("id");
            dicDT.Columns.Add("name");
            DataRow row = dicDT.NewRow();
           
            row["id"] = (int)EnumReportForm.DayAddNews;
            row["name"] = "每日接收消息数";
            dicDT.Rows.Add(row);

            row = dicDT.NewRow();
            row["id"] = (int)EnumReportForm.DayAddPeople;
            row["name"] = "每日净增人数";
            dicDT.Rows.Add(row);

            ViewBag.dicDT = dicDT;

            IReportFormPowerModel ReportModel = Factory.Get<IReportFormPowerModel>(SystemConst.IOC_Model.ReportFormPowerModel);
            ViewBag.PowerIDs = ReportModel.GetReportByAMID(accountMainId).Select(a => a.EnumReportID).ToList();

            return View();
        }

        [HttpPost]
        [AllowCheckPermissions(false)]
        public ActionResult Edit(int accountMainID)
        {
            var checkboxMenu = Request["checkboxMenu"];

            int[] menuIDArray = null;
            if (!string.IsNullOrEmpty(checkboxMenu))
            {
                menuIDArray = checkboxMenu.Split(',').ConvertToIntArray();
            }

            try
            {
                IReportFormPowerModel ReportModel = Factory.Get<IReportFormPowerModel>(SystemConst.IOC_Model.ReportFormPowerModel);
                ReportModel.Edit(accountMainID,menuIDArray);
            }
            catch (Exception ex)
            {
                return Alert(new Dialog(ex.Message));
            }
            return Content(AlertJS(new Dialog("绑定成功。", Url.Action("Index", "Report", new { Area = "System", accountMainId = accountMainID }))));

        }

    }
}
