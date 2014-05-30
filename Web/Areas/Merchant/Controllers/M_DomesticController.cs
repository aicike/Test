using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Interface.MerchantInterface;
using Injection;
using Poco.MerchantPoco;
using Poco.Enum;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 家政服务
    /// </summary>
    public class M_DomesticController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_Domestic/

        public ActionResult Index(int ?id)
        {
            var m_domesticModel = Factory.Get<IM_DomesticModel>(SystemConst.IOC_Model.M_DomesticModel);
            var m_domestic = m_domesticModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "家政服务 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_domestic);
        }

        public ActionResult Add()
        {

            ViewBag.Title = "家政服务 - 添加信息- " + SystemConst.PlatformName;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_Domestic m_domestic)
        {
            var m_domesticModel = Factory.Get<IM_DomesticModel>(SystemConst.IOC_Model.M_DomesticModel);
            m_domestic.MerchantID = LoginMerchant.ID;
            m_domestic.IsPublish = false;

            m_domestic.EnumDataStatus = (int)EnumDataStatus.None;
            m_domestic.CreatDate = DateTime.Now;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_domesticModel.AddInfo(m_domestic, ids);

            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_Domestic", new { Area = "Merchant" }) + "'");
        }


    }
}
