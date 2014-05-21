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
    public class MoveController : ManageSystemUserController
    {
        //
        // GET: /Manager/Move/
        public ActionResult Index(int? id, string strCreatdDate, string Mname, string selEnumStatus)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            PagedList<M_Move> move = null;
            if (!string.IsNullOrEmpty(selEnumStatus))
            {
                if (selEnumStatus == "all")
                {
                    move = m_moveModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
                else
                {
                    move = m_moveModel.GetListByStatus(int.Parse(selEnumStatus), strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
            }
            else
            {
                move = m_moveModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
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
            ViewBag.Title = "搬家 - 评审 -" + SystemConst.PlatformName;
            return View(move);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="Error">1:成功 2失败</param>
        /// <returns></returns>
        public ActionResult UnlockInfo(int UID,int PageID, int? Error)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            var move = m_moveModel.Get(UID);
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
            ViewBag.Title = move.Title + " - 搬家 - 评审 -" + SystemConst.PlatformName;
            return View(move);
        }
        [HttpPost]
        public ActionResult UnlockInfo(int UID, int Status)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            var result = m_moveModel.UpdateStatus(UID, Status);
            if (result.HasError)
            {
                return JavaScript("window.location.href='" + Url.Action("UnlockInfo", "Move", new {UID = UID, Error = 2 }) + "'");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("UnlockInfo", "Move", new {UID = UID, Error = 1 }) + "'");
            }

        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpStatus(int ID, int PageID)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            m_moveModel.UpdateStatus(ID, (int)EnumDataStatus.Disabled);
            return JavaScript("window.location.href='" + Url.Action("Index", "Move", new { Area = "Manager", id = PageID }) + "'");
        }


    }
}
