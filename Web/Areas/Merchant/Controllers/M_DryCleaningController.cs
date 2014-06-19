using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface.MerchantInterface;
using Poco;
using Poco.Enum;
using Poco.MerchantPoco;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 干洗服务
    /// </summary>
    public class M_DryCleaningController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_DryCleaning/

        public ActionResult Index(int ?id)
        {
            var m_dryCleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);
            var m_dryCleaning = m_dryCleaningModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "干洗服务 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_dryCleaning);
        }


        public ActionResult Add()
        {

            ViewBag.Title = "干洗服务 - 添加信息- " + SystemConst.PlatformName;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_DryCleaning m_drycleaning, int x1, int y1, int w, int h, int tw, int th)
        {
            var m_dryCleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);
            m_drycleaning.MerchantID = LoginMerchant.ID;
            m_drycleaning.IsPublish = false;

            m_drycleaning.EnumDataStatus = (int)EnumDataStatus.None;
            m_drycleaning.CreatDate = DateTime.Now;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_dryCleaningModel.AddInfo(m_drycleaning, ids, x1, y1, w, h, tw, th);

            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_DryCleaning", new { Area = "Merchant" }) + "'");
        }


        public ActionResult Edit(int ID)
        {
            var m_dryCleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);
            var dryCleaning = m_dryCleaningModel.GetInfoByID(LoginMerchant.ID, ID);
            ViewBag.Community = dryCleaning.M_CommunityMappings.Select(a => a.AccountMainID).ToArray();
            ViewBag.Title = "干洗服务 - 修改信息- " + SystemConst.PlatformName;
            return View(dryCleaning);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_DryCleaning m_drycleaning, int x1, int y1, int w, int h, int tw, int th)
        {
            var m_dryCleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);

            m_drycleaning.MerchantID = LoginMerchant.ID;
            m_drycleaning.IsPublish = false;
            m_drycleaning.EnumDataStatus = (int)EnumDataStatus.None;


            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_dryCleaningModel.EditInfo(m_drycleaning, ids, x1, y1, w, h, tw, th);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }



            return JavaScript("window.location.href='" + Url.Action("Index", "M_DryCleaning", new { Area = "Merchant" }) + "'");

        }


        public ActionResult Delete(int id, int PageID)
        {
            var m_dryCleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);
            var result = m_dryCleaningModel.DeleteInfo(id, LoginMerchant.ID);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_DryCleaning", new { Area = "Merchant", id = PageID }) + "'");
        }


        /// <summary>
        /// 更改是否发布信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ISPush"></param>
        /// <param name="PageID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpPush(int ID, bool ISPush, int PageID)
        {
            var m_dryCleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);
            m_dryCleaningModel.UpdatePush(ID, ISPush);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_DryCleaning", new { Area = "Merchant", id = PageID }) + "'");
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
            var m_dryCleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);
            m_dryCleaningModel.UpdateStatus(ID, (int)EnumDataStatus.WaitPayMent);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_DryCleaning", new { Area = "Merchant", id = PageID }) + "'");
        }


    }
}
