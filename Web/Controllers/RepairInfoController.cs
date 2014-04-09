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
    public class RepairInfoController : ManageAccountController
    {
        //
        // GET: /RepairInfo/

        public ActionResult Index(int? id)
        {
            var repairInfoModel = Factory.Get<IRepairInfoModel>(SystemConst.IOC_Model.RepairInfoModel);
            var repairinfo = repairInfoModel.GetInfo(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);


            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-报修管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(repairinfo);
        }

        /// <summary>
        /// 查看详细内容
        /// </summary>
        /// <param name="RID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowInfo(int RID)
        {
            return View();
        }
    }
}
