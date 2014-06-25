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
    public class ComplaintController : ManageAccountController
    {
        //
        // GET: /Complaint/

        public ActionResult Index(int ?id)
        {
            var complaintModel = Factory.Get<IComplaintModel>(SystemConst.IOC_Model.ComplaintModel);
            var complaint = complaintModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 20);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-投诉管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            var webNoticeModel = Factory.Get<IWebNoticeModel>(SystemConst.IOC_Model.WebNoticeModel);
            webNoticeModel.ClearWebNotice(LoginAccount.CurrentAccountMainID, "Token_WUYE_Complain");

            return View(complaint);
        }

        /// <summary>
        /// 查看详细
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public ActionResult ShowInfo(int CID)
        {
            var complaintModel = Factory.Get<IComplaintModel>(SystemConst.IOC_Model.ComplaintModel);
            var complain = complaintModel.Get(CID);

            var complaintReplyModel = Factory.Get<IComplaintReplyModel>(SystemConst.IOC_Model.ComplaintReplyModel);
            ViewBag.complainreply = complaintReplyModel.GetList(CID);
            ViewBag.CID = CID;
            return View(complain);
        }

        /// <summary>
        /// 答复
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="ReplyContent"></param>
        /// <returns></returns>
        public ActionResult ReplyCom(int CID, string ReplyContent)
        {
            var complaintReplyModel = Factory.Get<IComplaintReplyModel>(SystemConst.IOC_Model.ComplaintReplyModel);
            Result reusult = complaintReplyModel.AddInfo(CID, ReplyContent, LoginAccount.ID);
            return RedirectToAction("ShowInfo", "Complaint", new { CID = CID });
        }

    }
}
