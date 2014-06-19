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
    /// 家教
    /// </summary>
    public class TutorController : ManageSystemUserController
    {
        //
        // GET: /Manager/M_Tutor/

        public ActionResult Index(int? id, string strCreatdDate, string Mname, string selEnumStatus)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            PagedList<M_Tutor> tutor = null;
            if (!string.IsNullOrEmpty(selEnumStatus))
            {
                if (selEnumStatus == "all")
                {
                    tutor = m_tutorModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
                else
                {
                    tutor = m_tutorModel.GetListByStatus(int.Parse(selEnumStatus), strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
            }
            else
            {
                tutor = m_tutorModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
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
            ViewBag.Title = "家教 - 评审 -" + SystemConst.PlatformName;
            return View(tutor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="Error">1:成功 2失败</param>
        /// <returns></returns>
        public ActionResult TutorInfo(int TID, int PageID, int? Error)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            var tutor = m_tutorModel.Get(TID);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = 0;
            }
            ViewBag.TID = TID;
            ViewBag.PageID = PageID;
            ViewBag.Title = tutor.Title + " - 家教 - 评审 -" + SystemConst.PlatformName;
            return View(tutor);
        }

        [HttpPost]
        public ActionResult TutorInfo(int TID, int Status, int PageID)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            var result = m_tutorModel.UpdateStatus(TID, Status);
            if (result.HasError)
            {
                return JavaScript("window.location.href='" + Url.Action("TutorInfo", "Tutor", new { TID = TID, PageID = PageID, Error = 2 }) + "'");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("TutorInfo", "Tutor", new { TID = TID, PageID = PageID, Error = 1 }) + "'");
            }
        }


    }
}
