using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Poco;
using Injection;
using Poco.MerchantPoco;
using Interface.MerchantInterface;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 家政服务
    /// </summary>
    public class DomesticController : ManageSystemUserController
    {
        //
        // GET: /Manager/Domestic/

        public ActionResult Index(int? id, string strCreatdDate, string Mname, string selEnumStatus)
        {
            var m_domesticModel = Factory.Get<IM_DomesticModel>(SystemConst.IOC_Model.M_DomesticModel);
            PagedList<M_Domestic> domestic = null;
            if (!string.IsNullOrEmpty(selEnumStatus))
            {
                if (selEnumStatus == "all")
                {
                    domestic = m_domesticModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
                else
                {
                    domestic = m_domesticModel.GetListByStatus(int.Parse(selEnumStatus), strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
            }
            else
            {
                domestic = m_domesticModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
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
            ViewBag.Title = "家政服务 - 评审 -" + SystemConst.PlatformName;
            return View(domestic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="Error">1:成功 2失败</param>
        /// <returns></returns>
        public ActionResult DomesticInfo(int DID, int PageID, int? Error)
        {
            var m_domesticModel = Factory.Get<IM_DomesticModel>(SystemConst.IOC_Model.M_DomesticModel);
            var domestic = m_domesticModel.Get(DID);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = 0;
            }
            ViewBag.UID = DID;
            ViewBag.PageID = PageID;
            ViewBag.Title = domestic.Title + " - 家政服务 - 评审 -" + SystemConst.PlatformName;
            return View(domestic);
        }

        [HttpPost]
        public ActionResult DomesticInfo(int DID, int Status, int PageID)
        {
            var m_domesticModel = Factory.Get<IM_DomesticModel>(SystemConst.IOC_Model.M_DomesticModel);
            var result = m_domesticModel.UpdateStatus(DID, Status);
            if (result.HasError)
            {
                return JavaScript("window.location.href='" + Url.Action("DomesticInfo", "Domestic", new { DID = DID, PageID = PageID, Error = 2 }) + "'");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("DomesticInfo", "Domestic", new { DID = DID, PageID = PageID, Error = 1 }) + "'");
            }
        }
    }
}
