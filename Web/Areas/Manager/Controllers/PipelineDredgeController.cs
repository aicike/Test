using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Injection;
using Interface.MerchantInterface;
using Poco;
using Poco.MerchantPoco;
using Poco.Enum;

namespace Web.Areas.Manager.Controllers
{
    public class PipelineDredgeController : ManageSystemUserController
    {
        //
        // GET: /Manager/PipelineDredge/

        public ActionResult Index(int? id, string strCreatdDate, string Mname, string selEnumStatus)
        {
            var m_pipelinedredgeModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            PagedList<M_PipelineDredge> pipelinedredge = null;
            if (!string.IsNullOrEmpty(selEnumStatus))
            {
                if (selEnumStatus == "all")
                {
                    pipelinedredge = m_pipelinedredgeModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
                else
                {
                    pipelinedredge = m_pipelinedredgeModel.GetListByStatus(int.Parse(selEnumStatus), strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
            }
            else
            {
                pipelinedredge = m_pipelinedredgeModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
            }
            if (string.IsNullOrEmpty(strCreatdDate))
            {
                ViewBag.strCreatdDate = "";
            }
            else
            {
                ViewBag.strCreatdDate = strCreatdDate;
            }
            if (string.IsNullOrEmpty(Mname))
            {
                ViewBag.Mname = "";
            }
            else
            {
                ViewBag.Mname = Mname;
            }
            if (string.IsNullOrEmpty(selEnumStatus))
            {
                ViewBag.selEnumStatus = "";
            }
            else
            {
                ViewBag.selEnumStatus = selEnumStatus;
            }
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            ViewBag.Title = "开锁换锁 - 评审 -" + SystemConst.PlatformName;
            return View(pipelinedredge);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="Error">1:成功 2失败</param>
        /// <returns></returns>
        public ActionResult UnlockInfo(int UID, int PageID, int? Error)
        {
            var m_pipelinedredgeModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var unlock = m_pipelinedredgeModel.Get(UID);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = 0;
            }
            ViewBag.UID = UID;
            ViewBag.PageID = PageID;
            ViewBag.Title = unlock.Title + " - 开锁换锁 - 评审 -" + SystemConst.PlatformName;
            return View(unlock);
        }
        [HttpPost]
        public ActionResult UnlockInfo(int UID, int Status, int PageID)
        {
            var m_pipelinedredgeModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var result = m_pipelinedredgeModel.UpdateStatus(UID, Status);
            if (result.HasError)
            {
                return JavaScript("window.location.href='" + Url.Action("UnlockInfo", "PipelineDredge", new { UID = UID, PageID = PageID, Error = 2 }) + "'");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("UnlockInfo", "PipelineDredge", new { UID = UID, PageID = PageID, Error = 1 }) + "'");
            }

        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpStatus(int ID, int PageID)
        {
            var m_pipelinedredgeModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            m_pipelinedredgeModel.UpdateStatus(ID, (int)EnumDataStatus.Disabled);
            return JavaScript("window.location.href='" + Url.Action("Index", "PipelineDredge", new { Area = "Manager", id = PageID }) + "'");
        }

    }
}
