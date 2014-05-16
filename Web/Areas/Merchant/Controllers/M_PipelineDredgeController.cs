using System;
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
    public class M_PipelineDredgeController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_PipelineDredge/

        public ActionResult Index(int ?id)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var m_pipelinedredg = m_pipelineModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            return View(m_pipelinedredg);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_PipelineDredge m_pipelinedredg)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            m_pipelinedredg.MerchantID = LoginMerchant.ID;
            if (m_pipelinedredg.IsPublish)
            {
                m_pipelinedredg.EnumDataStatus = (int)EnumDataStatus.WaitPayMent;
            }
            else
            {
                m_pipelinedredg.EnumDataStatus = (int)EnumDataStatus.None;
            }
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
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_PipelineDredge m_pipelinedredg)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            m_pipelinedredg.MerchantID = LoginMerchant.ID;
            if (m_pipelinedredg.IsPublish)
            {
                m_pipelinedredg.EnumDataStatus = (int)EnumDataStatus.WaitPayMent;
            }
            else
            {
                m_pipelinedredg.EnumDataStatus = (int)EnumDataStatus.None;
                m_pipelinedredg.PublishDate = null;
            }

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

        public ActionResult Delete(int id)
        {
            var m_pipelineModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var result = m_pipelineModel.DeleteInfo(id, LoginMerchant.ID);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_PipelineDredge", new { Area = "Merchant" }) + "'");
        }
    }
}
