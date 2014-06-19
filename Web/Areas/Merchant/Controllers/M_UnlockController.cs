using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Poco;
using Interface.MerchantInterface;
using Poco.MerchantPoco;
using Poco.Enum;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 开锁换锁
    /// </summary>
    public class M_UnlockController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_Unlock/

        public ActionResult Index(int? id)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            var m_unlock = m_unlockModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "开锁换锁 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_unlock);
        }

        public ActionResult Add()
        {
            ViewBag.Title = "开锁换锁 - 添加信息- " + SystemConst.PlatformName;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_Unlock m_unlock)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            m_unlock.MerchantID = LoginMerchant.ID;
            m_unlock.IsPublish = false;

            m_unlock.EnumDataStatus = (int)EnumDataStatus.None;

            m_unlock.CreatDate = DateTime.Now;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_unlockModel.AddInfo(m_unlock, ids);

            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_Unlock", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Edit(int id)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            var item = m_unlockModel.GetInfoByID(LoginMerchant.ID, id);
            ViewBag.Community = item.M_CommunityMappings.Select(a => a.AccountMainID).ToArray();
            ViewBag.Title = "开锁换锁 - 修改信息- " + SystemConst.PlatformName;
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_Unlock m_unlock)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            m_unlock.MerchantID = LoginMerchant.ID;
            m_unlock.IsPublish = false;
            m_unlock.EnumDataStatus = (int)EnumDataStatus.None;


            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_unlockModel.EditInfo(m_unlock, ids);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_Unlock", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Delete(int id,int PageID)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);

            var result = m_unlockModel.DeleteInfo(id, LoginMerchant.ID);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Unlock", new { Area = "Merchant", id = PageID }) + "'");
        }

        /// <summary>
        /// 更是否发布信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ISPush"></param>
        /// <param name="PageID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpPush(int ID, bool ISPush,int PageID)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            m_unlockModel.UpdatePush(ID,ISPush);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Unlock", new { Area = "Merchant", id = PageID }) + "'");
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
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            m_unlockModel.UpdateStatus(ID,(int)EnumDataStatus.WaitPayMent);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Unlock", new { Area = "Merchant", id = PageID }) + "'");
        }
    }
}
