using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Poco;
using Injection;
using Interface.MerchantInterface;
using Poco.Enum;
using Poco.MerchantPoco;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 开锁换锁
    /// </summary>
    public class UnlockController : ManageSystemUserController
    {
        //
        // GET: /Manager/Unlock/

        public ActionResult Index(int? id, string strCreatdDate, string Mname, string selEnumStatus)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            PagedList<M_Unlock> unlock = null;
            if (!string.IsNullOrEmpty(selEnumStatus))
            {
                if (selEnumStatus == "all")
                {
                    unlock = m_unlockModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
                else
                {
                    unlock = m_unlockModel.GetListByStatus(int.Parse(selEnumStatus), strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
            }
            else
            {
                unlock = m_unlockModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
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
            return View(unlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="Error">1:成功 2失败</param>
        /// <returns></returns>
        public ActionResult UnlockInfo(int UID, int PageID, int? Error)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            var unlock = m_unlockModel.Get(UID);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else {
                ViewBag.Error = 0;
            }
            ViewBag.UID = UID;
            ViewBag.PageID = PageID;
            ViewBag.Title = unlock.Title + " - 开锁换锁 - 评审 -" + SystemConst.PlatformName;
            return View(unlock);
        }
        [HttpPost]
        public ActionResult UnlockInfo(int UID, int Status,int PageID)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            var result = m_unlockModel.UpdateStatus(UID, Status);
            if (result.HasError)
            {
                return JavaScript("window.location.href='" + Url.Action("UnlockInfo", "Unlock", new { UID = UID, PageID = PageID, Error = 2 }) + "'");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("UnlockInfo", "Unlock", new { UID = UID, PageID = PageID, Error = 1 }) + "'");
            }

        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpStatus(int ID,int PageID)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            m_unlockModel.UpdateStatus(ID, (int)EnumDataStatus.Disabled);
            return JavaScript("window.location.href='" + Url.Action("Index", "Unlock", new { Area = "Manager", id = PageID }) + "'");  
        }


      
    }
}
