using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Poco.Enum;

namespace Web.Controllers
{
    public class RepairInfoController : ManageAccountController
    {
        //
        // GET: /RepairInfo/

        public ActionResult Index(int? id)
        {
            var repairInfoModel = Factory.Get<IRepairInfoModel>(SystemConst.IOC_Model.RepairInfoModel);
            var repairinfo = repairInfoModel.GetInfo(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 20);


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
        public ActionResult ShowInfo(int RID)
        {
            var repairInfoModel = Factory.Get<IRepairInfoModel>(SystemConst.IOC_Model.RepairInfoModel);
            var repairinfo = repairInfoModel.GetInfoByID(RID,LoginAccount.CurrentAccountMainID);

            var repairOperation = Factory.Get<IRepairOperationModel>(SystemConst.IOC_Model.RepairOperationModel);
            ViewBag.reairOperation = repairOperation.GetInfoByPID(RID);
            ViewBag.RID = RID;
            return View(repairinfo);
        }
        /// <summary>
        /// 关闭维修单
        /// </summary>
        /// <param name="RID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CloseRepairInfo(int RID)
        {
            var repairInfoModel = Factory.Get<IRepairInfoModel>(SystemConst.IOC_Model.RepairInfoModel);
            string Content = Request.Form["CloseContent"].ToString();
            Result result = new Result();
            result = repairInfoModel.UpdStatus(RID, (int)EnumRepairStatus.Close);
            if (!result.HasError)
            {
                repairInfoModel.AddRemark(RID,"报修单关闭。关闭原因："+Content); 
            }
            return RedirectToAction("ShowInfo", "RepairInfo", new { RID = RID });
        }

        /// <summary>
        /// Account列表
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountList()
        {
            var roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roleList = roleModel.GetRoleList(LoginAccount.CurrentAccountMainID);
            return View(roleList);
        }

        /// <summary>
        /// 修改负责人
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdAccount(int RID, int AccountID,string AName)
        {
            var repairInfoModel = Factory.Get<IRepairInfoModel>(SystemConst.IOC_Model.RepairInfoModel);
            Result result = repairInfoModel.UpdAccount(RID,AccountID);
            if (result.HasError)
            {
                return false;
            }
            else
            {
                repairInfoModel.UpdStatus(RID, (int)EnumRepairStatus.Allocated);
                repairInfoModel.AddRemark(RID, "更改/分配负责人：" + AName); 
                return true;
            }
        }
    }
}
