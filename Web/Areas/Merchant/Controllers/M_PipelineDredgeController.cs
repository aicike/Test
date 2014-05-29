﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface.MerchantInterface;
using Poco;
using Poco.MerchantPoco;
using Poco.Enum;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 管道疏通
    /// </summary>
    public class M_PipelineDredgeController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_PipelineDredge/

        public ActionResult Index(int ?id)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var m_pipelinedredg = m_pipelineModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "管道疏通 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_pipelinedredg);
        }

        public ActionResult Add()
        {
            ViewBag.Title = "管道疏通 - 添加信息 - " + SystemConst.PlatformName;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_PipelineDredge m_pipelinedredg)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            m_pipelinedredg.MerchantID = LoginMerchant.ID;
            m_pipelinedredg.IsPublish = false;

            m_pipelinedredg.EnumDataStatus = (int)EnumDataStatus.None;
            m_pipelinedredg.CreatDate = DateTime.Now;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_pipelineModel.AddInfo(m_pipelinedredg, ids);

            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_PipelineDredge", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Edit(int id)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var item = m_pipelineModel.GetInfoByID(LoginMerchant.ID, id);
            ViewBag.Community = item.M_CommunityMappings.Select(a => a.AccountMainID).ToArray();
            ViewBag.Title = "管道疏通 - 修改信息- " + SystemConst.PlatformName;
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_PipelineDredge m_pipelinedredg)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            m_pipelinedredg.MerchantID = LoginMerchant.ID;
            m_pipelinedredg.IsPublish = false;
            m_pipelinedredg.EnumDataStatus = (int)EnumDataStatus.None;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_pipelineModel.EditInfo(m_pipelinedredg, ids);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_PipelineDredge", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Delete(int id, int PageID)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var result = m_pipelineModel.DeleteInfo(id, LoginMerchant.ID);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_PipelineDredge", new { Area = "Merchant", id = PageID }) + "'");
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
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            m_pipelineModel.UpdatePush(ID, ISPush);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_PipelineDredge", new { Area = "Merchant", id = PageID }) + "'");
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
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            m_pipelineModel.UpdateStatus(ID, (int)EnumDataStatus.WaitPayMent);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_PipelineDredge", new { Area = "Merchant", id = PageID }) + "'");
        }
    }
}
