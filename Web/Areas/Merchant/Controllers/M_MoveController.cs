using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface.MerchantInterface;
using Poco;
using Controllers;
using Poco.MerchantPoco;
using Poco.Enum;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 搬家
    /// </summary>
    public class M_MoveController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_Move/

        public ActionResult Index(int? id)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            var m_move = m_moveModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "搬家 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_move);
        }

        public ActionResult Add()
        {

            ViewBag.Title = "搬家 - 添加信息- " + SystemConst.PlatformName;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_Move m_move)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            m_move.MerchantID = LoginMerchant.ID;
            m_move.IsPublish = false;

            m_move.EnumDataStatus = (int)EnumDataStatus.None;
            m_move.CreatDate = DateTime.Now;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_moveModel.AddInfo(m_move, ids);

            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_Move", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Edit(int id)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            var item = m_moveModel.GetInfoByID(LoginMerchant.ID, id);
            ViewBag.Community = item.M_CommunityMappings.Select(a => a.AccountMainID).ToArray();
            ViewBag.Title = "搬家 - 修改信息- " + SystemConst.PlatformName; ;
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_Move m_move)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            m_move.MerchantID = LoginMerchant.ID;
            m_move.IsPublish = false;
            m_move.EnumDataStatus = (int)EnumDataStatus.None;


            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_moveModel.EditInfo(m_move, ids);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_Move", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Delete(int id, int PageID)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);

            var result = m_moveModel.DeleteInfo(id, LoginMerchant.ID);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Move", new { Area = "Merchant", id = PageID }) + "'");
        }


        /// <summary>
        /// 更是否发布信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ISPush"></param>
        /// <param name="PageID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpPush(int ID, bool ISPush, int PageID)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            m_moveModel.UpdatePush(ID, ISPush);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Move", new { Area = "Merchant", id = PageID }) + "'");
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ISPush"></param>
        /// <param name="PageID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpStatus(int ID, int PageID)
        {
            var m_moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            m_moveModel.UpdateStatus(ID, (int)EnumDataStatus.WaitPayMent);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Move", new { Area = "Merchant", id = PageID }) + "'");
        }
    }
}
